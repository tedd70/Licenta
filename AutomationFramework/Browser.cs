using AutomationFramework.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationFramework
{
    public class Browser
    {
        public static IWebDriver driver;

        public static IWebDriver Driver
        {
            get {
                if (driver == null)
                {
                    switch (Settings.BrowserType)
                    {
                        case BrowserType.Chrome:
                            InitializeChromeDesktop();
                            break;
                    }
                }
                return driver;
            }

            set { }
        }
        public static void InitializeChromeDesktop()
        {
            var options = new ChromeOptions();
            options.AddArgument(@"--incognito");
            options.AddArgument("--start-maximized");

            driver = new ChromeDriver(options);
        }

        public static void NavigateTo(string url)
        {
            if(Driver != null)
            {
                driver.Navigate().GoToUrl(url);
            }
        }

        public static void Quit()
        {
            if (driver == null )
            {
                return;
            }

            driver.Quit();
            driver = null; 
        }
        
    }
}
