namespace FormApp.API.Attributes;

internal class RateLimitInfo
{
    public DateTime WindowStart { get; set; } = DateTime.UtcNow;
    public int RequestCount { get; set; } = 0;
}
