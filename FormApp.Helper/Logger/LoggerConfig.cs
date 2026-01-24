using Serilog;

namespace FormApp.Helper.Logger;

public static class LoggerConfig
{
    public static void ConfigureLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logs/app-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
}
