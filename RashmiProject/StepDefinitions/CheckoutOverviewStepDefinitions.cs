using System;
using RashmiProject.Pages;
using TechTalk.SpecFlow;

namespace RashmiProject.StepDefinitions
{
    [Binding]


    public class CheckoutOverviewStepDefinitions
    {
        CheckOverviewPage checkover;

        public CheckoutOverviewStepDefinitions()
        {
            this.checkover = new CheckOverviewPage();
        }

        [Given(@"User is on the Checkout Overview page")]
        public void GivenUserIsOnTheCheckoutOverviewPage()
        {
            checkover.CheckOutOverview();
            Thread.Sleep(1000);
        }

        [When(@"User clicks on Finish")]
        public void WhenUserClicksOnFinish()
        {
            checkover.Finish();
            Thread.Sleep(1000);
        }

        [Then(@"Order status should be visible")]
        public void ThenOrderStatusShouldBeVisible()
        {
            checkover.OrderConfirm();
            
        }

    }
}
