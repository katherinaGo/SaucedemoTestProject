using System.Reflection;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using Tests.Exceptions;
using Tests.MyLogger;
using Tests.Pages;

namespace Tests.Driver;

public static class DriverInstance
{
    private static IWebDriver? _driver;

    public static IWebDriver Driver => InitializeDriver();

    public static string? GetDefaultBrowserName()
    {
        var json =
            File.ReadAllText(
                "/Users/kate/RiderProjects/SaucedemoTestProject/SaucedemoTestProject/Tests/ProjectInfo/configuration.json");
        var page = JsonConvert.DeserializeObject<WebPage>(json);
        var defaultBrowserName = page!.BrowserName;
        return defaultBrowserName;
    }

    public static void CloseBrowser()
    {
        _driver?.Close();
        _driver = null;
    }

    private static IWebDriver InitializeDriver()
    {
        try
        {
            var browser = GetDefaultBrowserName().ToLower();
            if (_driver == null)
            {
                switch (browser)
                {
                    case "chrome":
                        _driver = new ChromeDriver();
                        break;
                    case "google chrome":
                        _driver = new ChromeDriver();
                        break;
                    case "safari":
                        _driver = new SafariDriver();
                        break;
                    case "firefox":
                        _driver = new FirefoxDriver();
                        break;
                    case "mazila":
                        _driver = new FirefoxDriver();
                        break;
                    case "mazila firefox":
                        _driver = new FirefoxDriver();
                        break;
                    default:
                        Logger.ErrorLogger("Browser name is not found, can't open any browser.",
                            "Tests.Driver",
                            "DriverInstance",
                            MethodBase.GetCurrentMethod()?.Name!);

                        throw new NoSuchDriverException("Can't open such driver.");
                }
            }
        }
        catch (NoSuchDriverException e)
        {
            Logger.DebugLogger($"Exception: {e.Message}, \n{e.StackTrace}",
                "Tests.Driver",
                "DriverInstance",
                MethodBase.GetCurrentMethod()?.Name!);
        }

        return _driver!;
    }
}