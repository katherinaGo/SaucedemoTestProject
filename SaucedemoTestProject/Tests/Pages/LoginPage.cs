using System.Reflection;
using OpenQA.Selenium;
using Tests.Exceptions;
using Tests.Models;
using Tests.MyLogger;
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

    public void LogInToAccount(User user)
    {
        try
        {
            if (user.UserName.Equals(UsersCredentials.invalidCredsUser.ToString()))
            {
                InputDataToField(_userNameFieldId, user.UserName);
                InputDataToField(_passwordFieldXpath, user.Password);
                // Example of reflection:
                // InputDataToField(_passwordFieldXpath, UserReflection.SetCustomValueToFieldPassword());
                ClickButton(_logInButtonCss);

                Logger.ErrorLogger("Can't log in to the account with invalid credentials",
                    GetType().Namespace!,
                    GetType().Name,
                    MethodBase.GetCurrentMethod()?.Name!);
                throw new SuchUserDoesntExistException(
                    "Login or/and password are invalid or such user doesn't exist.");
            }

            InputDataToField(_userNameFieldId, user.UserName);
            InputDataToField(_passwordFieldXpath, user.Password);
            ClickButton(_logInButtonCss);
        }
        catch (SuchUserDoesntExistException e)
        {
            Logger.DebugLogger($"Exception: {e.Message}, \n{e.StackTrace}",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
        }
    }

    public bool CheckIfLoggedIn()
    {
        var result = false;
        try
        {
            result = IsElementFound(_productsTitleInHeaderCss);
            Logger.InfoLogger("User logged in successfully.",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
        }
        catch (NoSuchElementException e)
        {
            Logger.ErrorLogger("User is not logged in successfully.",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
            Logger.DebugLogger($"Exception: {e.Message}, \n{e.StackTrace}",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
        }

        return result;
    }

    public bool CheckIfLockedOutErrorDisplayed()
    {
        var result = false;
        try
        {
            result = IsElementFound(_lockedOutErrorXPath);
            Logger.InfoLogger("Error for locked user is displayed.",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
        }
        catch (NoSuchElementException e)
        {
            Logger.ErrorLogger("Error is not displayed or not found.",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
            Logger.DebugLogger($"Exception: {e.Message}, \n{e.StackTrace}",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
        }

        return result;
    }

    public bool CheckIfErrorDisplayedForNonExistingUser()
    {
        var result = false;
        try
        {
            result = IsElementFound(_errorInvalidCredsXPath);
            Logger.InfoLogger("Error is displayed when non-existing user tried to log in.",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
        }
        catch (NoSuchElementException e)
        {
            Logger.ErrorLogger("Error is not displayed for non-existing user.",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
            Logger.DebugLogger($"Exception: {e.Message}, \n{e.StackTrace}",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
        }

        return result;
    }
}