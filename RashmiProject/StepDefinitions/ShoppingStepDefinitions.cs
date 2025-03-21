using System;
using RashmiProject.Pages;
using TechTalk.SpecFlow;

namespace RashmiProject.StepDefinitions
{
    [Binding]
    public class ShoppingStepDefinitions
    {
        ItemPage itempage;

        public ShoppingStepDefinitions()
        {
            itempage = new ItemPage();
        }
        
        
        [When(@"User clicks on an Item")]
        public void WhenUserClicksOnAnItem()
        {
            itempage.Item();
        }

        [Then(@"Item details should be open")]
        public void ThenItemDetailsShouldBeOpen()
        {
            itempage.ItemDetail();
        }

    }
}
