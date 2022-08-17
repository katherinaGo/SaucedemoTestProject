using OpenQA.Selenium;

namespace LoginTests.Pages;

public class LoginPage : WebPage
{
    private readonly By _userNameFieldId = By.Id("user-name");
    private readonly By _passwordFieldXpath = By.XPath("//*[@id=\"password\"]");
    private readonly By _logInButtonXpath = By.XPath("//*[@id=\"login-button\"]");
    private readonly By _productsTitleInHeaderXpath = By.XPath("//*[@id=\"header_container\"]/div[2]/span");

    public LoginPage(IWebDriver driver) : base(driver)
    {
    }

    public void LogInToAccount(string login, string password)
    {
        InputDataToField(_userNameFieldId, login);
        InputDataToField(_passwordFieldXpath, password);
        ClickButton(_logInButtonXpath);
    }

    public bool CheckIfLoggedIn()
    {
        return IsElementFound(_productsTitleInHeaderXpath);
    }
}