using Tests.Models;

namespace Tests.Tests;

public class LoginTests : BaseTest
{
    [Test]
    public void LogInAsStandardUserTest()
    {
        Page.OpenWebsite(Url);
        LoginPage.LogInToAccount(new User(UsersCredentials.standard_user));
        bool actualResult = LoginPage.CheckIfLoggedIn();
        Assert.True(actualResult, "Not logged in");
    }

    [Test]
    public void LogInAsLockedUserTest()
    {
        LoginPage.OpenWebsite(Url);
        LoginPage.LogInToAccount(new User(UsersCredentials.locked_out_user));
        bool actualResult = LoginPage.CheckIfLockedOutErrorDisplayed();
        Assert.True(actualResult, "Error is not displayed or element not found.");
    }

    [Test]
    public void LogInAsProblemUserTest()
    {
        Page.OpenWebsite(Url);
        LoginPage.LogInToAccount(new User(UsersCredentials.problem_user));
        bool actualResult = LoginPage.CheckIfLoggedIn();
        Assert.True(actualResult, "Error is not displayed or element not found.");
    }

    [Test]
    public void LogInWithInvalidCredentials()
    {
        Page.OpenWebsite(Url);
        LoginPage.LogInToAccount(new User(UsersCredentials.invalidCredsUser));
        bool actualResult = LoginPage.CheckIfErrorDisplayedForNonExistingUser();
        Assert.That(actualResult, Is.EqualTo(true));
    }

    [Test]
    public void OpenProductsPageWhenNotLoggedInTest()
    {
        bool actualResult = ProductsPage
            .OpenProductsPage()
            .CheckIfErrorDisplayedWhenNotLoggedIn();
        Assert.That(actualResult, Is.EqualTo(true),
            "Error is not displayed if user tries to open products not being logged in.");
    }

    [Test]
    public void LogOutTest()
    {
        Page.OpenWebsite(Url);
        LoginPage.LogInToAccount(new User(UsersCredentials.standard_user))
            .OpenMenu()
            .ClickLogOutButton();
        bool actualResult = ProductsPage.CheckIfUserLoggedOut();
        Assert.That(actualResult, Is.EqualTo(true), "User is not logged out or element was not found.");
    }
}