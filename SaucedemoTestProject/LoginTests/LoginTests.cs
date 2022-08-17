using LoginTests.Pages;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LoginTests;

public class LoginTests
{
    private const string Url = "https://www.saucedemo.com/";
    private readonly string _userNameStandard = "standard_user"; // TODO check where to keep secret info as creds
    private readonly string _password = "secret_sauce";
    private LoginPage _loginPage;
    private ProductsPage _productsPage;
    private static WebPage _page;
    private static IWebDriver _driver;

    [SetUp]
    public void SetUp()
    {
        _driver = new ChromeDriver();
        _page = new WebPage(_driver);
        _loginPage = new LoginPage(_driver);
        _productsPage = new ProductsPage(_driver);
    }

    [Test]
    public void LogInAsStandardUserTest()
    {
        _page.OpenWebsite(Url);
        _loginPage.LogInToAccount(_userNameStandard, _password);
        bool actualResult = _loginPage.CheckIfLoggedIn();
        Assert.True(actualResult, "Not logged in");
    }

    [Test]
    public void OpenProductsPageWhenNotLoggedInTest()
    {
        _productsPage.OpenProductsPage();
        bool actualResult = _productsPage.CheckIfErrorDisplayedWhenNotLoggedIn();
        Assert.That(actualResult, Is.EqualTo(true),
            "Error is not displayed if user tries to open products not being logged in.");
    }

    [Test]
    public void LogOutTest()
    {
        _page.OpenWebsite(Url);
        _loginPage.LogInToAccount(Users.standard_user.ToString(), _password);
        _productsPage.OpenMenu();
        _productsPage.ClickLogOutButton();
        bool actualResult = _productsPage.CheckIfUserLoggedOut();
        Assert.That(actualResult, Is.EqualTo(true), "User is not logged out or element was not found.");
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Close();
        _driver.Quit();
    }
}