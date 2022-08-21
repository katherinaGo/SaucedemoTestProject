using OpenQA.Selenium;
using Tests.Driver;
using Tests.MyLogger;
using Tests.Pages;

namespace Tests.Tests;

public class BaseTest
{
    protected const string Url = "https://www.saucedemo.com/";
    private LoginPage _loginPage;
    private ProductsPage _productsPage;
    private static WebPage _page;
    private static IWebDriver _driver;
    private DateTime _startTime;
    private DateTime _endTime;
    private Logger _myLogger;

    protected LoginPage LoginPage
    {
        get => _loginPage;
        private set => _loginPage = value;
    }

    protected ProductsPage ProductsPage
    {
        get => _productsPage;
        private set => _productsPage = value;
    }

    protected static WebPage Page
    {
        get => _page;
        private set => _page = value;
    }

    private static IWebDriver Driver
    {
        get => DriverInstance.Driver;
        set => _driver = value;
    }

    [OneTimeSetUp]
    public void LoggerSetUp()
    {
        _myLogger = new Logger();
        _myLogger.CreateLogger();
        _startTime = _myLogger.StartProgramLogging();
    }

    [SetUp]
    public void SetUp()
    {
        Driver = DriverInstance.Driver;
        Page = new WebPage(_driver);
        LoginPage = new LoginPage(_driver);
        ProductsPage = new ProductsPage(_driver);
    }


    [OneTimeTearDown]
    public void LoggerTearDown()
    {
        _endTime = _myLogger.FinishProgramLogging();
        _myLogger.InfoLogger($"Tests time execution: {_endTime - _startTime}, hh:mm:ss:ms");
    }

    [TearDown]
    public void TearDown()
    {
        DriverInstance.CloseBrowser();
    }
}