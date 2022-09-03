using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Tests.MyLogger;

namespace Tests.Pages;

public class WebPage
{
    private static IWebDriver _driver;
    private static WebDriverWait _wait;
    protected readonly Logger MyLogger = new();

    public WebPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public static IWebDriver Driver
    {
        get => _driver;
    }

    public static WebDriverWait Wait
    {
        get => _wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
    }

    public void OpenWebsite(string url)
    {
        _driver.Navigate().GoToUrl(url);
        _driver.Manage().Window.Maximize();
    }

    protected IWebElement FindElement(By selector)
    {
        return _driver.FindElement(selector);
    }

    protected IList<IWebElement> FindElements(By selector)
    {
        return _driver.FindElements(selector);
    }

    protected void ClickButton(By selector)
    {
        FindElement(selector).Click();
    }

    protected void InputDataToField(By selector, string textToType)
    {
        FindElement(selector).SendKeys(textToType);
    }

    protected bool IsElementFound(By selector)
    {
        return FindElement(selector).Displayed;
    }
}