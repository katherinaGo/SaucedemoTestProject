using System.Reflection;
using Tests.EmailService;
using Tests.Models;

namespace Tests.Tests;

public class ProductsPageTests : BaseTest
{
    [Test]
    public void CheckIfProductsDisplayedWhenLoggedIn()
    {
        LoginPage?.OpenWebsite(Url);
        LoginPage!.LogInToAccount(new User(UsersCredentials.standard_user));
        bool actualResult = ProductsPage!.CheckIfItemsDisplayed();
        TestResults.GetTestResults(MethodBase.GetCurrentMethod()?.Name!, actualResult);
        Assert.True(actualResult, "No items displayed");
    }

    [Test]
    public void CheckIfProductsTShirtsInTheList()
    {
        LoginPage?.OpenWebsite(Url);
        LoginPage!.LogInToAccount(new User(UsersCredentials.standard_user));
        bool actualResult = ProductsPage!.CheckIfTShirtsInTheList();
        TestResults.GetTestResults(MethodBase.GetCurrentMethod()?.Name!, actualResult);
        Assert.True(actualResult, "No t-shirts found");
    }

    [Test]
    public void CheckIfPossibleToSortByPriceFromLowToHigh()
    {
        LoginPage?.OpenWebsite(Url);
        LoginPage!.LogInToAccount(new User(UsersCredentials.problem_user));
        bool actualResult = ProductsPage!.CheckIfSortedByPriceFromLowToHighCorrectly();
        TestResults.GetTestResults(MethodBase.GetCurrentMethod()?.Name!, actualResult);
        Assert.True(actualResult, "Items not sorted.");
    }
}