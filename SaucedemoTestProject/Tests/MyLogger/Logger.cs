using Serilog;
using Log = Serilog.Log;

namespace Tests.MyLogger;

public class Logger
{
    private readonly string _pathToLogFile =
        "/Users/kate/RiderProjects/SaucedemoTestProject/SaucedemoTestProject/Tests/Logs/";

    public void CreateLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File(
                _pathToLogFile +
                $"log_"
                + $"{DateTime.Today.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.txt",
                rollOnFileSizeLimit: true,
                fileSizeLimitBytes: 30000
            )
            .CreateLogger();
    }

    public static void InfoLogger(string logMessage, string nameSpace, string className, string methodName)
    {
        var threadName = Thread.CurrentThread.Name;
        Log.Logger.Information("Namespace: {Namespace}, Class: {Class}, MethodName: {MethodName}, Thread: {ThreadName}",
            nameSpace,
            className,
            methodName,
            threadName);
        Log.Information(logMessage);
    }

    public static void DebugLogger(string logMessage, string nameSpace, string className, string methodName)
    {
        var threadName = Thread.CurrentThread.Name;
        Log.Logger.Debug("Namespace: {Namespace}, Class: {Class}, MethodName: {MethodName}, Thread: {ThreadName}",
            nameSpace,
            className,
            methodName,
            threadName);
        Log.Debug(logMessage);
    }

    public static void ErrorLogger(string logMessage, string nameSpace, string className, string methodName)
    {
        var threadName = Thread.CurrentThread.Name;
        Log.Logger.Error("Namespace: {Namespace}, Class: {Class}, MethodName: {MethodName}, Thread: {ThreadName}",
            nameSpace,
            className,
            methodName,
            threadName);
        Log.Error(logMessage);
    }

    public static DateTime StartProgramLogging()
    {
        Log.Information("{LocalTime}; Test execution starts", DateTime.Now.ToLocalTime());
        return DateTime.Now;
    }

    public static DateTime FinishProgramLogging()
    {
        Log.Information("{LocalTime}; Test execution finished", DateTime.Now.ToLocalTime());
        return DateTime.Now;
    }
}