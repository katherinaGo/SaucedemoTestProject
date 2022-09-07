using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Tests.MyLogger;

namespace Tests.Pages;

public class WebPage
{
    private static IWebDriver _driver;
    protected readonly Logger MyLogger = new();

    public WebPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public static IWebDriver Driver => _driver;

    public static WebDriverWait WaitImplicit => new(Driver, TimeSpan.FromSeconds(15));

    public static void WaitExplicit(By locator) => WaitImplicit.Until(ExpectedConditions.ElementIsVisible(locator));


    public void OpenWebsite(string url)
    {
        _driver.Navigate().GoToUrl(url);
        _driver.Manage().Window.Maximize();
    }

    protected IWebElement FindElement(By selector) => _driver.FindElement(selector);

    protected IList<IWebElement> FindElements(By selector) => _driver.FindElements(selector);

    protected void ClickButton(By selector) => FindElement(selector).Click();

    protected void InputDataToField(By selector, string textToType) => FindElement(selector).SendKeys(textToType);

    protected bool IsElementFound(By selector) => FindElement(selector).Displayed;

    protected void SelectElementInDropdown(By selector, string textOfItemToClick)
    {
        SelectElement select = new SelectElement(FindElement(selector));
        select.SelectByText(textOfItemToClick);
    }
}