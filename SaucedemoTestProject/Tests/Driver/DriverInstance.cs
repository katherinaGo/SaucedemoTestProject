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
    private static readonly Logger MyLogger = new();

    public static IWebDriver Driver
    {
        get
        {
            string browser = GetDefaultBrowserName();
            if (_driver == null)
            {
                switch (browser)
                {
                    case "chrome":
                        _driver = new ChromeDriver();
                        break;
                    case "safari":
                        _driver = new SafariDriver();
                        break;
                    case "firefox":
                        _driver = new FirefoxDriver();
                        break;
                    default:
                        MyLogger.ErrorLogger("Browser name is not found, can't open any browser.",
                            "Tests.Driver",
                            "DriverInstance",
                            MethodBase.GetCurrentMethod()?.Name!);

                        throw new NoSuchDriverException("Can't open such driver.");
                }
            }

            return _driver;
        }
    }

    private static string GetDefaultBrowserName()
    {
        string json =
            File.ReadAllText(
                "/Users/kate/RiderProjects/SaucedemoTestProject/SaucedemoTestProject/Tests/configuration.json");
        WebPage? page = JsonConvert.DeserializeObject<WebPage>(json);
        string defaultBrowserName = page!.BrowserName;
        return defaultBrowserName;
    }

    public static void CloseBrowser()
    {
        _driver?.Quit();
        _driver = null;
    }
}