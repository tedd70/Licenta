using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AutomationFramework.Pages
{
    public class Register
    {
        public Register()
        {
            PageFactory.InitElements(Browser.Driver, this);
        }

        [FindsBy(How = How.Id, Using = "UserName")]
        public IWebElement UserNameInput;

        [FindsBy(How = How.Id, Using = "FirstName")]
        public IWebElement FirstNameInput;

        [FindsBy(How = How.Id, Using = "LastName")]
        public IWebElement LastNameInput;

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement PasswordInput;

        [FindsBy(How = How.CssSelector, Using = ".log-in")]
        public IWebElement SubmitButton;
        
    }
}
