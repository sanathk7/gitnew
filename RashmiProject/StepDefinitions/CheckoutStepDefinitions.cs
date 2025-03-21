using System;
using RashmiProject.Pages;
using TechTalk.SpecFlow;

namespace RashmiProject.StepDefinitions
{
    [Binding]
    public class CheckoutStepDefinitions
    {

        CheckOutPage checkout;

        public CheckoutStepDefinitions()
        {
            this.checkout = new CheckOutPage();
        }


        [Given(@"User is on the Checkout page")]
        public void GivenUserIsOnTheCheckoutPage()
        {
            checkout.CheckOut();
        }

        [When(@"User enters First Name ""([^""]*)"", Last Name ""([^""]*)"", and Zip Code ""([^""]*)""")]
        public void WhenUserEntersFirstNameLastNameAndZipCode(string sanath, string kumar, string p2)
        {
            checkout.BuyerDetail(sanath, kumar, p2);
            Thread.Sleep(1000);
        }

        [Then(@"Clicks on Continue")]
        public void ThenClicksOnContinue()
        {
            checkout.Countniue();
            Thread.Sleep(1000);
        }

    }
}