using FormApp.API.Configuration;

namespace FormApp.API.Attributes;

// Report generation rate limiting
public class ReportRateLimitAttribute : ConfigurableRateLimitAttribute
{
    protected override string GetCacheKeyPrefix() => "report";

    protected override (int requests, int windowMinutes, string message) GetLimitSettings(RateLimitSettings settings)
    {
        return (
            settings.Report.Requests,
            settings.Report.WindowMinutes,
            "Too many report requests. Please try again after {windowMinutes} minutes."
        );
    }
}
