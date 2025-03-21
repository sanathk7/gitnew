using System;
using RashmiProject.Pages;
using TechTalk.SpecFlow;

namespace RashmiProject.StepDefinitions
{
    [Binding]
    public class LoginPagesStepDefinitions
    {
        LoginPage loginpage;

        public LoginPagesStepDefinitions()
        {

            loginpage = new LoginPage();
        }

        [Given(@"User is on the Login Page")]
        public void GivenUserIsOnTheLoginPage()
        {
            loginpage.LaunchBrowser();
            Thread.Sleep(1000);
        }

        [When(@"User enters the Username ""([^""]*)""")]
        public void WhenUserEntersTheUsername(string p0)
        {
            loginpage.EnterUserNames(p0);
        }

        [When(@"User enters the Password ""([^""]*)""")]
        public void WhenUserEntersThePassword(string p0)
        {
            loginpage.EnterPassowrd(p0);
            Thread.Sleep(1000);
        }

        [When(@"Clicks the Login button")]
        public void WhenClicksTheLoginButton()
        {
            loginpage.Submit();

        }

        [Then(@"Homepage should open")]
        public void ThenHomepageShouldOpen()
        {
            loginpage.HomePageDisplay();
            Thread.Sleep(1000);
        }

    }
}
