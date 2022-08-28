using Serilog;

namespace Tests.MyLogger;

public class Logger
{
    private string _programStarts;
    private string _programEnds;

    public void CreateLogger()
    {
        int counter = 1;
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console().WriteTo.File(
                $"/Users/kate/RiderProjects/SaucedemoTestProject/SaucedemoTestProject/Tests/Logs/log_"
                + $"{DateTime.Today.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.txt",
                rollOnFileSizeLimit: true, fileSizeLimitBytes: 2020).CreateLogger();
        counter++;
    }

    public void InfoLogger(string logMessage)
    {
        Log.Information(logMessage);
    }

    public void DebugLogger(string logMessage)
    {
        Log.Debug(logMessage);
    }

    public void ErrorLogger(string logMessage)
    {
        Log.Error(logMessage);
    }

    public DateTime StartProgramLogging()
    {
        _programStarts = DateTime.UtcNow + " UTC; Program starts";
        Log.Information(_programStarts);
        return DateTime.Now;
    }

    public DateTime FinishProgramLogging()
    {
        _programEnds = DateTime.UtcNow + " UTC; Program finished";
        Log.Information(_programEnds);
        return DateTime.Now;
    }
}