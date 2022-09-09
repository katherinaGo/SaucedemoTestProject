using System.Reflection;
using OpenQA.Selenium;

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

    public bool CheckIfErrorDisplayedWhenNotLoggedIn()
    {
        if (IsElementFound(_errorMessageWhenOpenProductsBeingNotLogInXPath))
        {
            MyLogger.InfoLogger("Error message is displayed - impossible to open products page not being logged in.",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
            return true;
        }

        MyLogger.ErrorLogger($"Error message '{_errorMessageWhenOpenProductsBeingNotLogInXPath}' is not displayed.",
            GetType().Namespace!,
            GetType().Name,
            MethodBase.GetCurrentMethod()?.Name!);
        return false;
    }


    public ProductsPage OpenMenu()
    {
        ClickButton(_menuCss);
        return new ProductsPage(Driver);
    }

    public LoginPage ClickLogOutButton()
    {
        try
        {
            WaitExplicit(_logOutBtnCss);
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
                MyLogger.InfoLogger("User successfully logged out from the account.",
                    GetType().Namespace!,
                    GetType().Name,
                    MethodBase.GetCurrentMethod()?.Name!);
            }
            else
            {
                MyLogger.ErrorLogger($"User is not logged out or element '{_logInButtonCss}' not found.",
                    GetType().Namespace!,
                    GetType().Name,
                    MethodBase.GetCurrentMethod()?.Name!);
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
        if (productsList.Count != 0)
        {
            MyLogger.InfoLogger($"Products found on the products page.",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
            return true;
        }

        return false;
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

        MyLogger.ErrorLogger("No products are displayed on the products page",
            GetType().Namespace!,
            GetType().Name,
            MethodBase.GetCurrentMethod()?.Name!);
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
            MyLogger.InfoLogger("Prices were sorted correctly from low to high",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
            return true;
        }

        MyLogger.ErrorLogger("Prices were sorted incorrect from low to high",
            GetType().Namespace!,
            GetType().Name,
            MethodBase.GetCurrentMethod()?.Name!);

        return false;
    }

    private bool IsElementVisibleExplicitWait(By selector) => WaitImplicit.Until(_ => FindElement(selector)).Displayed;

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
        try
        {
            for (int i = 0; i < pricesOfItems.Count; i++)
            {
                pricesStrings[i] = pricesOfItems[i].Text.Trim('$').Replace('.', ',');
                prices[i] = Convert.ToDouble(pricesStrings[i]);
            }

            return prices;
        }
        catch (FormatException exception)
        {
            Console.WriteLine(exception.StackTrace);
            Console.WriteLine(exception.Message);
            MyLogger.ErrorLogger("FormatException, incorrect parsing of string to double.",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
            return null!;
        }
    }
}