using OpenQA.Selenium;

namespace LoginTests.Pages;

public class LoginPage : WebPage
{
    private readonly By _userNameFieldId = By.Id("user-name");
    private readonly By _passwordFieldXpath = By.XPath("//*[@id=\"password\"]");
    private readonly By _logInButtonCss = By.CssSelector("#login-button");
    private readonly By _productsTitleInHeaderCss = By.CssSelector(".header_secondary_container");
    private readonly By _lockedOutErrorCss = By.CssSelector(".error-message-container");

    public LoginPage(IWebDriver driver) : base(driver)
    {
    }

    public void LogInToAccount(string login, string password)
    {
        InputDataToField(_userNameFieldId, login);
        InputDataToField(_passwordFieldXpath, password);
        ClickButton(_logInButtonCss);
    }

    public bool CheckIfLoggedIn()
    {
        return IsElementFound(_productsTitleInHeaderCss);
    }

    public bool CheckIfLockedOutErrorDisplayed()
    {
        return IsElementFound(_lockedOutErrorCss);
    }
}