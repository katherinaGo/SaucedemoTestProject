using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Tests.Pages;

public class ProductsPage : WebPage
{
    private readonly string _productsUrl = "https://www.saucedemo.com/inventory.html";

    private readonly By _errorMessageWhenOpenProductsBeingNotLogInXPath =
        By.XPath("//h3[contains(text(), 'You can only access ')]");

    private readonly By _menuCss = By.CssSelector("#react-burger-menu-btn");
    private readonly By _logOutBtnCss = By.CssSelector("#logout_sidebar_link");
    private readonly By _logInButtonCss = By.CssSelector("#login-button");
    private readonly By _productsNameCss = By.CssSelector(".inventory_item_name");
    private readonly By _pricesOfItemsCss = By.CssSelector(".inventory_item_price");
    private readonly By _sortedMenuCss = By.CssSelector(".product_sort_container");

    public ProductsPage(IWebDriver driver) : base(driver)
    {
    }

    public ProductsPage OpenProductsPage()
    {
        OpenWebsite(_productsUrl);
        return new ProductsPage(Driver);
    }

    public bool CheckIfErrorDisplayedWhenNotLoggedIn() =>
        IsElementFound(_errorMessageWhenOpenProductsBeingNotLogInXPath);

    public ProductsPage OpenMenu()
    {
        ClickButton(_menuCss);
        return new ProductsPage(Driver);
    }

    public LoginPage ClickLogOutButton()
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
        catch (NoSuchElementException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }

        return new LoginPage(Driver);
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

    public bool CheckIfItemsDisplayed()
    {
        IList<IWebElement> productsList = GetAllProducts();
        return productsList.Count != 0;
    }

    public bool CheckIfTShirtsInTheList()
    {
        IList<IWebElement> productsList = GetAllProducts();
        var foundTshirts = productsList.Where(item => item.Text.Contains("T-Shirt"));
        if (foundTshirts.Any())
        {
            MyLogger.InfoLogger($"It's found {foundTshirts.Count()} t-shirt(s) on the products page.",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
            return true;
        }

        return false;
    }

    public bool CheckIfSortedByPriceFromLowToHighCorrectly()
    {
        double[] sortedByComp = GetPricesOfItemsOnTheProductsPageAndSortFromLowToHigh();
        double[] sortedByWebSite = GetSortedPricesOfItemsOnTheProductsPageFromLowToHigh();
        int count = 0;

        for (int i = 0; i < sortedByComp.Length; i++)
        {
            if (sortedByComp[i].Equals(sortedByWebSite[i]))
            {
                count++;
            }
        }

        if (count.Equals(sortedByComp.Length))
        {
            return true;
        }

        return false;
    }

    private bool IsElementVisibleExplicitWait(By selector) => Wait.Until(d => FindElement(selector)).Displayed;

    private IList<IWebElement> GetAllProducts()
    {
        IList<IWebElement> listOfProducts = FindElements(_productsNameCss);
        Console.WriteLine("List of found products:");
        foreach (var item in listOfProducts)
        {
            Console.WriteLine(item.Text);
        }

        return listOfProducts;
    }

    private double[] GetPricesOfItemsOnTheProductsPageAndSortFromLowToHigh()
    {
        IList<IWebElement> pricesOfItems = FindElements(_pricesOfItemsCss);
        double[] prices = ParseStringToDouble(pricesOfItems);
        Array.Sort(prices);
        return prices;
    }

    private double[] GetSortedPricesOfItemsOnTheProductsPageFromLowToHigh()
    {
        SortByPriceFromLowToHigh();
        IList<IWebElement> pricesOfItems = FindElements(_pricesOfItemsCss);
        double[] prices = ParseStringToDouble(pricesOfItems);
        return prices;
    }

    private void SortByPriceFromLowToHigh()
    {
        SelectElementInDropdown(_sortedMenuCss, "Price (low to high)");
    }

    private double[] ParseStringToDouble(IList<IWebElement> pricesOfItems)
    {
        string[] pricesStrings = new string[pricesOfItems.Count];
        double[] prices = new Double[pricesOfItems.Count];
        for (int i = 0; i < pricesOfItems.Count; i++)
        {
            pricesStrings[i] = pricesOfItems[i].Text.Trim('$').Replace('.', ',');
            prices[i] = Convert.ToDouble(pricesStrings[i]);
        }

        return prices;
    }
}