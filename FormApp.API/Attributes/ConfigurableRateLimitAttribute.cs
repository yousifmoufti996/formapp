using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using FormApp.API.Configuration;
using FormApp.Core.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace FormApp.API.Attributes;

public abstract class ConfigurableRateLimitAttribute : ActionFilterAttribute
{
    protected abstract string GetCacheKeyPrefix();
    protected abstract (int requests, int windowMinutes, string message) GetLimitSettings(RateLimitSettings settings);

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();
        var settings = context.HttpContext.RequestServices.GetRequiredService<IOptions<RateLimitSettings>>().Value;
        
        var (requests, windowMinutes, message) = GetLimitSettings(settings);
        
        var clientId = GetClientIdentifier(context.HttpContext);
        var actionKey = GetActionKey(context);
        var cacheKey = $"rate_limit_{GetCacheKeyPrefix()}_{actionKey}_{clientId}";

        var requestInfo = cache.Get<RateLimitInfo>(cacheKey) ?? new RateLimitInfo();
        var now = DateTime.UtcNow;

        // Reset if window has passed
        if (now - requestInfo.WindowStart > TimeSpan.FromMinutes(windowMinutes))
        {
            requestInfo = new RateLimitInfo { WindowStart = now, RequestCount = 0 };
        }

        // Check if rate limit exceeded
        if (requestInfo.RequestCount >= requests)
        {
            var retryAfter = requestInfo.WindowStart.AddMinutes(windowMinutes) - now;
            context.HttpContext.Response.Headers["Retry-After"] = ((int)retryAfter.TotalSeconds).ToString();
            
            context.Result = new JsonResult(new ExceptionModel
            {
                HttpStatusCode = (int)HttpStatusCode.TooManyRequests,
                StatusCode = (int)HttpStatusCode.TooManyRequests,
                Message = message.Replace("{windowMinutes}", windowMinutes.ToString())
            })
            {
                StatusCode = (int)HttpStatusCode.TooManyRequests
            };
            return;
        }

        // Increment request count
        requestInfo.RequestCount++;
        cache.Set(cacheKey, requestInfo, TimeSpan.FromMinutes(windowMinutes));

        // Add rate limit headers
        context.HttpContext.Response.Headers["X-RateLimit-Limit"] = requests.ToString();
        context.HttpContext.Response.Headers["X-RateLimit-Remaining"] = (requests - requestInfo.RequestCount).ToString();
        context.HttpContext.Response.Headers["X-RateLimit-Reset"] = requestInfo.WindowStart.AddMinutes(windowMinutes).ToString("O");

        await next();
    }

    private string GetClientIdentifier(HttpContext context)
    {
        var userId = context.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            return $"user_{userId}";
        }

        var ipAddress = context.Connection.RemoteIpAddress?.ToString() 
                       ?? context.Request.Headers["X-Forwarded-For"].FirstOrDefault() 
                       ?? "unknown";
        
        return $"ip_{ipAddress}";
    }

    private string GetActionKey(ActionExecutingContext context)
    {
        var controller = context.ActionDescriptor.RouteValues["controller"];
        var action = context.ActionDescriptor.RouteValues["action"];
        var method = context.HttpContext.Request.Method;
        return $"{controller}_{action}_{method}";
    }
}
