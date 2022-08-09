using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LoginTests.Pages;

public class ProductsPage : WebPage
{
    private readonly string _productsUrl = "https://www.saucedemo.com/inventory.html";

    private readonly By _errorMessageWhenOpenProductsBeingNotLogInXPath =
        By.XPath("//*[@id=\"login_button_container\"]/div/form/div[3]/h3");

    private readonly By _menuXPath = By.XPath("//*[@id=\"menu_button_container\"]/div/div[1]/div");
    private readonly By _logOutBtnXPath = By.XPath("//*[@id=\"logout_sidebar_link\"]");
    private readonly By _userNameFieldId = By.Id("user-name");
    private static WebDriverWait _wait;

    public ProductsPage(IWebDriver driver) : base(driver)
    {
    }

    public void OpenProductsPage()
    {
        OpenWebsite(_productsUrl);
    }

    public bool CheckIfErrorDisplayedWhenNotLoggedIn()
    {
        return IsElementFound(_errorMessageWhenOpenProductsBeingNotLogInXPath);
    }

    public void OpenMenu()
    {
        ClickButton(_menuXPath);
    }

    public void ClickLogOutButton()
    {
        try
        {
            if (IsElementVisibleExplicitWait(_logOutBtnXPath).Equals(true))
            {
                ClickButton(_logOutBtnXPath);
            }
        }
        catch (WebDriverTimeoutException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }
    }

    public void GetAllMenuItems()
    {
        IList<IWebElement> menuSections = FindElements(_menuXPath);
    }

    public bool CheckIfUserLoggedOut()
    {
        try
        {
            if (FindElement(_userNameFieldId).Displayed)
            {
                return true;
            }
        }
        catch (NoSuchElementException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }

        return false;
    }

    private bool IsElementVisibleExplicitWait(By selector)
    {
        _wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
        return _wait.Until(d => FindElement(selector)).Displayed;
    }
}