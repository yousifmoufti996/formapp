namespace FormApp.API.Configuration;

public class RateLimitSettings
{
    public const string SectionName = "RateLimit";

    public AuthRateLimit Auth { get; set; } = new();
    public FileRateLimit File { get; set; } = new();
    public StandardRateLimit Standard { get; set; } = new();
    public LenientRateLimit Lenient { get; set; } = new();
    public AdminRateLimit Admin { get; set; } = new();
    public ReportRateLimit Report { get; set; } = new();
    public DualRateLimit Dual { get; set; } = new();
}

public class AuthRateLimit
{
    public int Requests { get; set; } = 5;
    public int WindowMinutes { get; set; } = 5;
}

public class FileRateLimit
{
    public int Requests { get; set; } = 3;
    public int WindowMinutes { get; set; } = 10;
}

public class StandardRateLimit
{
    public int Requests { get; set; } = 100;
    public int WindowMinutes { get; set; } = 1;
}

public class LenientRateLimit
{
    public int Requests { get; set; } = 200;
    public int WindowMinutes { get; set; } = 1;
}

public class AdminRateLimit
{
    public int Requests { get; set; } = 20;
    public int WindowMinutes { get; set; } = 10;
}

public class ReportRateLimit
{
    public int Requests { get; set; } = 10;
    public int WindowMinutes { get; set; } = 5;
}

public class DualRateLimit
{
    public int RegularRequests { get; set; } = 50;
    public int RegularWindowMinutes { get; set; } = 1;
    public int FileRequests { get; set; } = 3;
    public int FileWindowMinutes { get; set; } = 10;
}
