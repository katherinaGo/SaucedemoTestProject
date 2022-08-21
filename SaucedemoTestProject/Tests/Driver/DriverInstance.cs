using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using Tests.Exceptions;

namespace Tests.Driver;

public static class DriverInstance
{
    private static IWebDriver _driver;

    public static IWebDriver Driver
    {
        get
        {
            //TODO get this browser value from DB 
            string browser = "chrome";
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
                        throw new NoSuchDriverException("Can't open such driver.");
                }
            }

            return _driver;
        }
    }

    // private static string GetDefaultBrowserName()
    // {
    //     return DbCommunication.GetDefaultBrowserToRunTests();
    // }

    public static void CloseBrowser()
    {
        _driver.Quit();
        _driver = null;
    }
}