using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace FormApp.API.Attributes;

/// <summary>
/// Rate limit specifically for image upload endpoints - 3 requests per minute
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
public class ImageUploadRateLimitAttribute : ActionFilterAttribute
{
    private readonly int _maxRequests = 3;
    private readonly TimeSpan _timeWindow = TimeSpan.FromMinutes(1);

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();
        
        // Get user identifier (IP + UserId if authenticated)
        var userIdentifier = GetUserIdentifier(context.HttpContext);
        var cacheKey = $"ImageUploadRateLimit_{userIdentifier}";

        if (!cache.TryGetValue(cacheKey, out List<DateTime> requestTimestamps))
        {
            requestTimestamps = new List<DateTime>();
        }

        // Remove old timestamps outside the time window
        var now = DateTime.UtcNow;
        requestTimestamps = requestTimestamps
            .Where(t => now - t < _timeWindow)
            .ToList();

        if (requestTimestamps.Count >= _maxRequests)
        {
            context.Result = new ContentResult
            {
                StatusCode = (int)HttpStatusCode.TooManyRequests,
                Content = $"Rate limit exceeded. Maximum {_maxRequests} image upload requests per minute allowed.",
                ContentType = "application/json"
            };
            return;
        }

        // Add current timestamp
        requestTimestamps.Add(now);
        
        // Cache for the time window duration
        cache.Set(cacheKey, requestTimestamps, _timeWindow);

        await next();
    }

    private string GetUserIdentifier(HttpContext context)
    {
        var userId = context.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        
        return string.IsNullOrEmpty(userId) ? $"ip_{ipAddress}" : $"user_{userId}";
    }
}
