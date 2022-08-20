using OpenQA.Selenium;
using Tests.Driver;
using Tests.Pages;

namespace Tests.Tests;

public class BaseTest
{
    protected const string Url = "https://www.saucedemo.com/";
    protected readonly string _password = "secret_sauce";
    protected LoginPage _loginPage;
    protected ProductsPage _productsPage;
    protected static WebPage _page;
    protected static IWebDriver _driver;

    [SetUp]
    public void SetUp()
    {
        _driver = DriverSingletone.Driver;
        _page = new WebPage(_driver);
        _loginPage = new LoginPage(_driver);
        _productsPage = new ProductsPage(_driver);
    }

    [TearDown]
    public void TearDown()
    {
        DriverSingletone.CloseBrowser();
    }
}