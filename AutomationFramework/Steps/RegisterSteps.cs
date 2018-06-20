using AutomationFramework.Helpers;
using AutomationFramework.Models;
using AutomationFramework.Pages;
using Shouldly;
using System.Threading;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace AutomationFramework.Steps
{
    [Binding]
    public class RegisterSteps
    {
        Register Register = new Register();

        [When(@"the user fills in the user details form")]
        public void WhenTheUserFillsInTheUserDetailsForm(Table table)
        {
            var userDetails = table.CreateInstance<RegistrationModel>();
            Register.UserNameInput.SendKeys(Utils.ReplaceText(userDetails.Username));
            Register.FirstNameInput.SendKeys(Utils.ReplaceText(userDetails.FirstName));
            Register.LastNameInput.SendKeys(Utils.ReplaceText(userDetails.LastName));
            Register.PasswordInput.SendKeys(Utils.ReplaceText(userDetails.Password));
        }

        [When(@"the user clicks on Submit button")]
        public void WhenTheUserClicksOnSubmitButton()
        {
            Register.SubmitButton.Click();
        }

        [Then(@"the user is redirected to login page")]
        public void ThenTheUserIsRedirectedToLoginPage()
        {
            Browser.driver.Url.ShouldBe("http://ads.licenta.local/");
            Thread.Sleep(2000);
        }

    }
}
