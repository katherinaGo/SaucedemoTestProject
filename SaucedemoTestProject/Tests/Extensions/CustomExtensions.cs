using OpenQA.Selenium;

namespace Tests.Extensions;

public static class CustomExtensions
{
    public static string GetCurrentUrlOfPage(this IWebDriver driver)
    {
        return driver.Url;
    }

    public static void ClearCookies(this IWebDriver driver)
    {
        driver.Manage().Cookies.DeleteAllCookies();
    }

    public static void OpenBrowserNewTab(this IWebDriver driver)
    {
        driver.SwitchTo().NewWindow(WindowType.Tab);
        driver.SwitchTo().NewWindow(WindowType.Window);
    }
}