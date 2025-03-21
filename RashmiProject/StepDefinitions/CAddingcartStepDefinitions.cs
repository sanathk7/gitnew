using System;
using RashmiProject.Pages;
using TechTalk.SpecFlow;

namespace RashmiProject.StepDefinitions
{
    [Binding]
    public class CAddingcartStepDefinitions
    {


        AddCart addcart;

        public CAddingcartStepDefinitions()
        {
            addcart = new AddCart();
        }

        [Given(@"User is logged in")]
        public void GivenUserIsLoggedIn()
        {
            //loginpage.AddToCart();
            Console.WriteLine("Usre Logid in");
        }

        [When(@"User clicks on Add to Cart")]
        public void WhenUserClicksOnAddToCart()
        {
            addcart.AddToCart();
        }

        [When(@"User clicks on Cart")]
        public void WhenUserClicksOnCart()
        {
            addcart.Cart();
            Thread.Sleep(1000);
        }

        [Then(@"User Cart should open")]
        public void ThenUserCartShouldOpen()
        {
            addcart.UserCart();
            Thread.Sleep(1000);
        }

    }
}
