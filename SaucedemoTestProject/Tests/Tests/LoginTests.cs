namespace Tests.Tests;

public class LoginTests : BaseTest
{
    [Test]
    public void LogInAsStandardUserTest()
    {
        _page.OpenWebsite(Url);
        _loginPage.LogInToAccount(Users.standard_user.ToString(), _password);
        bool actualResult = _loginPage.CheckIfLoggedIn();
        Assert.True(actualResult, "Not logged in");
    }

    [Test]
    public void LogInAsLockedUserTest()
    {
        _page.OpenWebsite(Url);
        _loginPage.LogInToAccount(Users.locked_out_user.ToString(), _password);
        bool actualResult = _loginPage.CheckIfLockedOutErrorDisplayed();
        Assert.True(actualResult, "Error is not displayed or element not found.");
    }

    [Test]
    public void LogInAsProblemUserTest()
    {
        _page.OpenWebsite(Url);
        _loginPage.LogInToAccount(Users.problem_user.ToString(), _password);
        bool actualResult = _loginPage.CheckIfLoggedIn();
        Assert.True(actualResult, "Error is not displayed or element not found.");
    }

    [Test]
    public void OpenProductsPageWhenNotLoggedInTest()
    {
        bool actualResult = _productsPage
            .OpenProductsPage()
            .CheckIfErrorDisplayedWhenNotLoggedIn();
        Assert.That(actualResult, Is.EqualTo(true),
            "Error is not displayed if user tries to open products not being logged in.");
    }

    [Test]
    public void LogOutTest()
    {
        _page.OpenWebsite(Url);
        _loginPage.LogInToAccount(Users.standard_user.ToString(), _password)
            .OpenMenu()
            .ClickLogOutButton();
        bool actualResult = _productsPage.CheckIfUserLoggedOut();
        Assert.That(actualResult, Is.EqualTo(true), "User is not logged out or element was not found.");
    }
}