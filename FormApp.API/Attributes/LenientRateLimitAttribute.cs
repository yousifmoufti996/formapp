using FormApp.API.Configuration;

namespace FormApp.API.Attributes;

// Lenient rate limiting for non-critical endpoints
public class LenientRateLimitAttribute : ConfigurableRateLimitAttribute
{
    protected override string GetCacheKeyPrefix() => "lenient";

    protected override (int requests, int windowMinutes, string message) GetLimitSettings(RateLimitSettings settings)
    {
        return (
            settings.Lenient.Requests,
            settings.Lenient.WindowMinutes,
            "Too many requests. Please try again after {windowMinutes} minutes."
        );
    }
}
