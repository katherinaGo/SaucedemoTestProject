using Tests.Models;

namespace Tests.Tests;

public class ProductsPageTests : BaseTest
{
    [Test]
    public void CheckIfProductsDisplayedWhenLoggedIn()
    {
        LoginPage.OpenWebsite(Url);
        LoginPage.LogInToAccount(new User(UsersCredentials.standard_user));
        bool actualResult = ProductsPage.CheckIfItemsDisplayed();
        Assert.True(actualResult, "No items displayed");
    }

    [Test]
    public void CheckIfProductsTShirtsInTheList()
    {
        LoginPage.OpenWebsite(Url);
        LoginPage.LogInToAccount(new User(UsersCredentials.standard_user));
        bool actualResult = ProductsPage.CheckIfTShirtsInTheList();
        Assert.True(actualResult, "No t-shirts found");
    }

    [Test]
    public void CheckIfPossibleToSortByPriceFromLowToHigh()
    {
        LoginPage.OpenWebsite(Url);
        LoginPage.LogInToAccount(new User(UsersCredentials.standard_user));
        bool actualResult = ProductsPage.CheckIfSortedByPriceFromLowToHighCorrectly();
        Assert.True(actualResult, "Items sorted not in correct order.");
    }
}