using FormApp.API.Configuration;

namespace FormApp.API.Attributes;

// File upload rate limiting
public class FileRateLimitAttribute : ConfigurableRateLimitAttribute
{
    protected override string GetCacheKeyPrefix() => "file";

    protected override (int requests, int windowMinutes, string message) GetLimitSettings(RateLimitSettings settings)
    {
        return (
            settings.File.Requests,
            settings.File.WindowMinutes,
            "Too many file upload requests. Please try again after {windowMinutes} minutes."
        );
    }
}
