using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace Tests.Driver;

public static class DriverInstance
{
    private static IWebDriver _driver;

    public static IWebDriver Driver
    {
        get
        {
            //TODO get this browser value from DB 
            string _browser = "chrome";
            if (_driver == null)
            {
                switch (_browser)
                {
                    case "safari":
                        _driver = new SafariDriver();
                        break;
                    case "firefox":
                        _driver = new FirefoxDriver();
                        break;
                    default:
                        _driver = new ChromeDriver();
                        break;
                }
            }

            return _driver;
        }
    }

    public static void CloseBrowser()
    {
        _driver.Quit();
        _driver = null;
    }
}