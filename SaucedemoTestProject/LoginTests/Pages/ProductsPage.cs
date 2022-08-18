using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LoginTests.Pages;

public class ProductsPage : WebPage
{
    private readonly string _productsUrl = "https://www.saucedemo.com/inventory.html";
    private readonly By _errorMessageWhenOpenProductsBeingNotLogInCss = By.CssSelector(".error-message-container");
    private readonly By _menuCss = By.CssSelector("#react-burger-menu-btn");
    private readonly By _logOutBtnCss = By.CssSelector("#logout_sidebar_link");
    private readonly By _logInButtonCss = By.CssSelector("#login-button");
    private readonly By _productsCss = By.CssSelector(".inventory_item");
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
        return IsElementFound(_errorMessageWhenOpenProductsBeingNotLogInCss);
    }

    public void OpenMenu()
    {
        ClickButton(_menuCss);
        _wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
    }

    public void ClickLogOutButton()
    {
        try
        {
            if (IsElementVisibleExplicitWait(_logOutBtnCss).Equals(true))
            {
                ClickButton(_logOutBtnCss);
            }
        }
        catch (WebDriverTimeoutException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }
    }

    public IList<IWebElement> GetAllMenuItems()
    {
        IList<IWebElement> menuSections = FindElements(_menuCss);
        return menuSections;
    }

    public bool CheckIfUserLoggedOut()
    {
        bool result;
        try
        {
            if (IsElementFound(_logInButtonCss))
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }
        catch (NoSuchElementException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            result = false;
        }

        return result;
    }

    private bool IsElementVisibleExplicitWait(By selector)
    {
        _wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
        return _wait.Until(d => FindElement(selector)).Displayed;
    }

    private IList<IWebElement> GetAllProducts()
    {
        return FindElements(_productsCss);
    }

    public bool CheckIfItemsDisplayed()
    {
        IList<IWebElement> productsList = GetAllProducts();
        return productsList.Count != 0;
    }
}