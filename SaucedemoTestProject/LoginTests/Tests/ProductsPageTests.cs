namespace LoginTests.Tests;

public class ProductsPageTests : BaseTest
{
    [Test]
    public void CheckIfProductsDisplayedWhenLoggedIn()
    {
        _loginPage.OpenWebsite(Url);
        _loginPage.LogInToAccount(Users.problem_user.ToString(), _password);
        bool actualResult = _productsPage.CheckIfItemsDisplayed();
        Assert.True(actualResult, "No items displayed");
    }
}