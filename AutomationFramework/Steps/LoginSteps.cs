using AutomationFramework.Helpers;
using AutomationFramework.Pages;
using Shouldly;
using System.Threading;
using TechTalk.SpecFlow;

namespace AutomationFramework.Steps
{
    [Binding]
    public class LoginSteps
    {
        Login Login = new Login();

        [Given(@"the user navigates to login page")]
        public void GivenTheUserNavigatesToLoginPage()
        {
            Browser.NavigateTo("http://ads.licenta.local/");
        }

        [Given(@"the user types in the following username: '(.*)'")]
        [When(@"the user types in the following username: '(.*)'")]
        public void WhenTheUserTypesInTheFollowingUsername(string userName)
        {
            Login.UserNameInput.SendKeys(Utils.ReplaceText(userName));
            Thread.Sleep(2000);
        }

        [Given(@"the user types in the following password: '(.*)'")]
        [When(@"the user types in the following password: '(.*)'")]
        public void WhenTheUserTypesInTheFollowingPassword(string password)
        {
            Login.PasswordInput.SendKeys(Utils.ReplaceText(password));
        }

        [When(@"the user clicks on the login button")]
        public void WhenTheUserClicksOnTheLoginButton()
        {
            Thread.Sleep(2000);
            Login.LoginButton.Click();
        }

        [Then(@"invalid user details error message is displayed: '(.*)'")]
        public void ThenInvalidUserDetailsErrorMessageIsDisplayed(string errorMessage)
        {
            Login.ErrorMessageInvalidData.Text.ShouldBe(errorMessage);
            Thread.Sleep(2500);
        }

        [Given(@"the user clicks on Register here link")]
        public void GivenTheUserClicksOnRegisterHereLink()
        {
            Login.RegisterHereLink.Click();
        }

        [Then(@"the user is succesfully logged in into the application")]
        public void ThenTheUserIsSuccesfullyLoggedInIntoTheApplication()
        {
            Browser.driver.Url.ShouldBe("http://ads.licenta.local/Configuration");
            Thread.Sleep(2500);
        }

    }
}
