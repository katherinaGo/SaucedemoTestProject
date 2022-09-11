using System.Reflection;
using OpenQA.Selenium;
using Tests.Driver;
using Tests.EmailService;
using Tests.MyLogger;
using Tests.Pages;

namespace Tests.Tests;

public class BaseTest
{
    protected const string Url = "https://www.saucedemo.com/";
    private static IWebDriver? _driver;
    private DateTime _startTime;
    private DateTime _endTime;
    private Logger? _myLogger;
    private EmailSender? _emailSender;

    protected LoginPage? LoginPage { get; private set; }

    protected ProductsPage? ProductsPage { get; private set; }

    protected static WebPage? Page { get; private set; }

    private static IWebDriver Driver
    {
        get => DriverInstance.Driver;
        set => _driver = value;
    }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _myLogger = new Logger();
        _myLogger.CreateLogger();
        _startTime = _myLogger.StartProgramLogging();
        _emailSender = new EmailSender();
        TestResults.ClearFileBeforeTestExecution();
    }

    [SetUp]
    public void SetUp()
    {
        Driver = DriverInstance.Driver;
        Page = new WebPage(Driver);
        LoginPage = new LoginPage(Driver);
        ProductsPage = new ProductsPage(Driver);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _endTime = _myLogger!.FinishProgramLogging();
        _myLogger.InfoLogger($"Tests time execution: {_endTime - _startTime}, hh:mm:ss:ms",
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