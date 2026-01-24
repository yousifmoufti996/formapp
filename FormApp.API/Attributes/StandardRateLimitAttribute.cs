using FormApp.API.Configuration;

namespace FormApp.API.Attributes;

// Standard rate limiting for general API endpoints
public class StandardRateLimitAttribute : ConfigurableRateLimitAttribute
{
    protected override string GetCacheKeyPrefix() => "standard";

    protected override (int requests, int windowMinutes, string message) GetLimitSettings(RateLimitSettings settings)
    {
        return (
            settings.Standard.Requests,
            settings.Standard.WindowMinutes,
            "Too many requests. Please try again after {windowMinutes} minutes."
        );
    }
}
