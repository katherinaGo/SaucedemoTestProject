using System.Reflection;
using Tests.EmailService;
using Tests.Models;

namespace Tests.Tests;

public class LoginTests : BaseTest
{
    [Test]
    public void LogInAsStandardUserTest()
    {
        Page?.OpenWebsite(Url);
        LoginPage?.LogInToAccount(new User(UsersCredentials.standard_user));
        var actualResult = LoginPage!.CheckIfLoggedIn();
        TestResults.GetTestResults(MethodBase.GetCurrentMethod()?.Name!, actualResult);
        Assert.That(actualResult, Is.True, "Not logged in");
    }

    [Test]
    public void LogInAsLockedUserTest()
    {
        LoginPage?.OpenWebsite(Url);
        LoginPage!.LogInToAccount(new User(UsersCredentials.locked_out_user));
        bool actualResult = LoginPage.CheckIfLockedOutErrorDisplayed();
        TestResults.GetTestResults(MethodBase.GetCurrentMethod()?.Name!, actualResult);
        Assert.That(actualResult, Is.True, "Error is not displayed or element not found.");
    }

    [Test]
    public void LogInAsProblemUserTest()
    {
        Page?.OpenWebsite(Url);
        LoginPage?.LogInToAccount(new User(UsersCredentials.problem_user));
        var actualResult = LoginPage!.CheckIfLoggedIn();
        TestResults.GetTestResults(MethodBase.GetCurrentMethod()?.Name!, actualResult);
        Assert.That(actualResult, Is.True, "Error is not displayed or element not found.");
    }

    [Test]
    public void LogInWithInvalidCredentials()
    {
        Page?.OpenWebsite(Url);
        LoginPage?.LogInToAccount(new User(UsersCredentials.invalidCredsUser));
        var actualResult = LoginPage!.CheckIfErrorDisplayedForNonExistingUser();
        TestResults.GetTestResults(MethodBase.GetCurrentMethod()?.Name!, actualResult);
        Assert.That(actualResult, Is.EqualTo(true));
    }

    [Test]
    public void OpenProductsPageWhenNotLoggedInTest()
    {
        var actualResult = ProductsPage!
            .OpenProductsPage()
            .CheckIfErrorDisplayedWhenNotLoggedIn();
        TestResults.GetTestResults(MethodBase.GetCurrentMethod()?.Name!, actualResult);
        Assert.That(actualResult, Is.EqualTo(true),
            "Error is not displayed if user tries to open products not being logged in.");
    }

    [Test]
    public void LogOutTest()
    {
        Page?.OpenWebsite(Url);
        LoginPage?.LogInToAccount(new User(UsersCredentials.standard_user));
        ProductsPage?
            .OpenMenu()
            .ClickLogOutButton();
        var actualResult = ProductsPage!.CheckIfUserLoggedOut();
        TestResults.GetTestResults(MethodBase.GetCurrentMethod()?.Name!, actualResult);
        Assert.That(actualResult, Is.EqualTo(true), "User is not logged out or element was not found.");
    }
}