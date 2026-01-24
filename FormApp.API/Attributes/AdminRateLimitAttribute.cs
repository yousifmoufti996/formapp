using FormApp.API.Configuration;

namespace FormApp.API.Attributes;

// Admin rate limiting
public class AdminRateLimitAttribute : ConfigurableRateLimitAttribute
{
    protected override string GetCacheKeyPrefix() => "admin";

    protected override (int requests, int windowMinutes, string message) GetLimitSettings(RateLimitSettings settings)
    {
        return (
            settings.Admin.Requests,
            settings.Admin.WindowMinutes,
            "Too many admin requests. Please try again after {windowMinutes} minutes."
        );
    }
}
