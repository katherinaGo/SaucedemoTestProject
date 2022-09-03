using System.Reflection;
using OpenQA.Selenium;
using Tests.Exceptions;
using Tests.Models;
using Tests.Reflection;

namespace Tests.Pages;

public class LoginPage : WebPage
{
    private readonly By _userNameFieldId = By.Id("user-name");
    private readonly By _passwordFieldXpath = By.XPath("//*[@id=\"password\"]");
    private readonly By _logInButtonCss = By.CssSelector("#login-button");
    private readonly By _productsTitleInHeaderCss = By.CssSelector(".header_secondary_container");
    private readonly By _lockedOutErrorXPath = By.XPath("//h3[contains(text(), 'has been locked out.')]");

    private readonly By _errorInvalidCredsXPath =
        By.XPath("//h3[contains(text(), 'Username and password do not match')]");

    public LoginPage(IWebDriver driver) : base(driver)
    {
    }

    public ProductsPage LogInToAccount(User user)
    {
        try
        {
            if (user.UserName.Equals(UsersCredentials.invalidCredsUser.ToString()))
            {
                MyLogger.ErrorLogger("Can't log in to the account with invalid credentials",
                    GetType().Namespace!,
                    GetType().Name,
                    MethodBase.GetCurrentMethod()?.Name!);
                throw new SuchUserDoesntExistException(
                    "Login or/and password are invalid or such user doesn't exist.");
            }
        }
        catch (SuchUserDoesntExistException e)
        {
            Console.WriteLine(e.StackTrace);
            Console.WriteLine(e.Message);
        }

        InputDataToField(_userNameFieldId, user.UserName);
        InputDataToField(_passwordFieldXpath, user.Password);
        ClickButton(_logInButtonCss);
        return new ProductsPage(Driver);
    }

    public bool CheckIfLoggedIn()
    {
        bool result = false;
        try
        {
            result = IsElementFound(_productsTitleInHeaderCss);
        }
        catch (NoSuchElementException e)
        {
            Console.WriteLine(e.StackTrace);
            Console.WriteLine(e.Message);
        }

        return result;
    }

    public bool CheckIfLockedOutErrorDisplayed()
    {
        bool result = false;
        try
        {
            result = IsElementFound(_lockedOutErrorXPath);
        }
        catch (NoSuchElementException e)
        {
            Console.WriteLine(e.StackTrace);
            Console.WriteLine(e.Message);
        }

        return result;
    }

    public bool CheckIfErrorDisplayedForNonExistingUser()
    {
        bool result = false;
        try
        {
            result = IsElementFound(_errorInvalidCredsXPath);
        }
        catch (NoSuchElementException e)
        {
            Console.WriteLine(e.StackTrace);
            Console.WriteLine(e.Message);
        }

        return result;
    }
}