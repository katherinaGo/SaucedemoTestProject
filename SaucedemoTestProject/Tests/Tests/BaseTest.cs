using System.Reflection;
using Tests.Driver;
using Tests.EmailService;
using Tests.MyLogger;
using Tests.Pages;

namespace Tests.Tests;

public class BaseTest
{
    protected const string Url = "https://www.saucedemo.com/";
    private DateTime _startTime;
    private DateTime _endTime;
    private Logger? _myLogger;
    private EmailSender? _emailSender;

    protected LoginPage? LoginPage { get; private set; }

    protected ProductsPage? ProductsPage { get; private set; }

    protected static WebPage? Page { get; private set; }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _myLogger = new Logger();
        _myLogger.CreateLogger();
        _startTime = Logger.StartProgramLogging();
        _emailSender = new EmailSender();
        TestResults.ClearFileBeforeTestExecution();
    }

    [SetUp]
    public void SetUp()
    {
        Page = new WebPage();
        LoginPage = new LoginPage();
        ProductsPage = new ProductsPage();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _endTime = Logger.FinishProgramLogging();
        Logger.InfoLogger($"Tests time execution: {_endTime - _startTime}, hh:mm:ss:ms",
            GetType().Namespace!,
            GetType().Name,
            MethodBase.GetCurrentMethod()?.Name!);
        _emailSender?.SendEmailWithResults();
    }

    [TearDown]
    public void TearDown()
    {
        DriverInstance.CloseBrowser();
    }
}