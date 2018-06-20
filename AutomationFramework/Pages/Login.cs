using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AutomationFramework.Pages
{
    public class Login
    {
        public Login()
        {
            PageFactory.InitElements(Browser.Driver, this);
        }

        [FindsBy(How = How.Id, Using = "UserName")]
        public IWebElement UserNameInput;

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement PasswordInput;

        [FindsBy(How = How.CssSelector, Using = ".log-in")]
        public IWebElement LoginButton;

        [FindsBy(How = How.CssSelector, Using = ".form-login h3")]
        public IWebElement ErrorMessageInvalidData;

        [FindsBy(How = How.CssSelector, Using = ".register")]
        public IWebElement RegisterHereLink;
    }
}
