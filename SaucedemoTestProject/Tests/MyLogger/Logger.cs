using System.Reflection;
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

    public void InfoLogger(string logMessage, string nameSpace, string className, string methodName)
    {
        string? threadName = Thread.CurrentThread.Name;
        Log.Logger.Information("Namespace: {Namespace}, Class: {Class}, MethodName: {MethodName}, Thread: {ThreadName}",
            nameSpace,
            className,
            methodName,
            threadName);
        Log.Information(logMessage);
    }

    public void DebugLogger(string logMessage, string nameSpace, string className, string methodName)
    {
        string? threadName = Thread.CurrentThread.Name;
        Log.Logger.Information("Namespace: {Namespace}, Class: {Class}, MethodName: {MethodName}, Thread: {ThreadName}",
            nameSpace,
            className,
            methodName,
            threadName);
        Log.Debug(logMessage);
    }

    public void ErrorLogger(string logMessage, string nameSpace, string className, string methodName)
    {
        string? threadName = Thread.CurrentThread.Name;
        Log.Logger.Information("Namespace: {Namespace}, Class: {Class}, MethodName: {MethodName}, Thread: {ThreadName}",
            nameSpace,
            className,
            methodName,
            threadName);
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