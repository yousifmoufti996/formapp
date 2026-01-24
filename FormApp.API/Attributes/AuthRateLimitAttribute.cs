using FormApp.API.Configuration;

namespace FormApp.API.Attributes;

// Strict rate limiting for authentication endpoints
public class AuthRateLimitAttribute : ConfigurableRateLimitAttribute
{
    protected override string GetCacheKeyPrefix() => "auth";

    protected override (int requests, int windowMinutes, string message) GetLimitSettings(RateLimitSettings settings)
    {
        return (
            settings.Auth.Requests,
            settings.Auth.WindowMinutes,
            "Too many authentication attempts. Please try again after {windowMinutes} minutes."
        );
    }
}
