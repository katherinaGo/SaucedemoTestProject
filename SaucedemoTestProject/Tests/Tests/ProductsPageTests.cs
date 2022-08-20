using Tests.Model;

namespace Tests.Tests;

public class ProductsPageTests : BaseTest
{
    [Test]
    public void CheckIfProductsDisplayedWhenLoggedIn()
    {
        LoginPage.OpenWebsite(Url);
        LoginPage.LogInToAccount(new User(UsersCredentials.problem_user));
        bool actualResult = ProductsPage.CheckIfItemsDisplayed();
        Assert.True(actualResult, "No items displayed");
    }
}