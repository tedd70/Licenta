using OpenQA.Selenium.Support.Extensions;
using System;
using TechTalk.SpecFlow;

namespace AutomationFramework.Helpers
{
    [Binding]
    public class StepBase
    {
        [AfterFeature]
        public static void AfterFeature()
        {
            if (Browser.driver != null)
            {
                try
                {
                    if (ScenarioContext.Current.TestError != null)
                    {
                        TakeScreenShot();
                    }
                }
                finally
                {
                    Browser.Quit();
                }
            }
        }

        public static void TakeScreenShot()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                TakeScreenshotOnException(ScenarioContext.Current.ScenarioInfo.Title);
            }
        }

        private static void TakeScreenshotOnException(string title)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");
            string scenario = ScenarioContext.Current.ScenarioInfo.Title;
            Browser.Driver.TakeScreenshot().SaveAsFile("Exception-" + timestamp + "-" + scenario + ".png");
        }
    }
}
