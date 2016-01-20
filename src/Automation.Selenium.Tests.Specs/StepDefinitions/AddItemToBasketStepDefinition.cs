using Automation.Selenium.Tests.PageObjects.PageFactory;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Automation.Selenium.Tests.Specs.StepDefinitions
{
    [Binding]
    public sealed class AddItemToBasketStepDefinition
    {
        [Given(@"I am on an item page")]
        public void GivenIAmOnAnItemPage()
        {
            Pages.DefaultItemPage.Goto();
        }

        [Given(@"I add the item to the basket")]
        [When(@"I add the item to the basket")]
        public void WhenIAddTheItemToTheBasket()
        {
            Pages.DefaultItemPage.AddItemToBasket();
        }

        [Then(@"I should see the message ""(.*)""")]
        public void ThenIShouldSeeTheMessage(string expectedMessage)
        {
            Assert.IsTrue(Pages.Header.AddedToBasketMessageAppears(expectedMessage));
        }

        [Then(@"I should see the following")]
        public void ThenIShouldSeeTheFollowing(Table expectedValues)
        {
            expectedValues.CompareToSet(Pages.Header.ChangesAfterAddingItemToBasket());
        }
    }
}


