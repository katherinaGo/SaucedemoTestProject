using OpenQA.Selenium;
using Tests.Driver;
using Tests.Pages;

namespace Tests.Tests;

public class BaseTest
{
    protected const string Url = "https://www.saucedemo.com/";
    private LoginPage _loginPage;
    private ProductsPage _productsPage;
    private static WebPage _page;
    private static IWebDriver _driver;

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

    protected static IWebDriver Driver
    {
        get => DriverInstance.Driver;
        set => _driver = value;
    }

    [SetUp]
    public void SetUp()
    {
        Driver = DriverInstance.Driver;
        Page = new WebPage(_driver);
        LoginPage = new LoginPage(_driver);
        ProductsPage = new ProductsPage(_driver);
    }

    [TearDown]
    public void TearDown()
    {
        DriverInstance.CloseBrowser();
    }
}