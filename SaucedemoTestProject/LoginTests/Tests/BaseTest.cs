using LoginTests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LoginTests.Tests;

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
        _driver = new ChromeDriver();
        _page = new WebPage(_driver);
        _loginPage = new LoginPage(_driver);
        _productsPage = new ProductsPage(_driver);
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Close();
        _driver.Quit();
    }
}