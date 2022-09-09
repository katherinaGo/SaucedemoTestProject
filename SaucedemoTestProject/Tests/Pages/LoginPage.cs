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
        // InputDataToField(_passwordFieldXpath, UserReflection.SetCustomValueToFieldPassword());
        ClickButton(_logInButtonCss);
        return new ProductsPage(Driver);
    }

    public bool CheckIfLoggedIn()
    {
        bool result = false;
        try
        {
            result = IsElementFound(_productsTitleInHeaderCss);
            MyLogger.InfoLogger("User logged in successfully.",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
        }
        catch (NoSuchElementException e)
        {
            MyLogger.ErrorLogger("User is not logged in for some reason.",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
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
            MyLogger.InfoLogger("Error for locked user is displayed.",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
        }
        catch (NoSuchElementException e)
        {
            MyLogger.ErrorLogger("Error is not displayed or not found.",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
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
            MyLogger.InfoLogger("Error is displayed when non-existing user tried to log in.",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
        }
        catch (NoSuchElementException e)
        {
            MyLogger.ErrorLogger("Error is not displayed for non-existing user.",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
            Console.WriteLine(e.StackTrace);
            Console.WriteLine(e.Message);
        }

        return result;
    }
}