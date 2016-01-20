using Insight.Web.BddTests.PageObjects.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Insight.Web.BddTests.Specs.Steps
{
    [Binding]
    public class HeaderPageStepDefiniton
    {
        [Then(@"I should see the user logged in as ""(.*)""")]
        public void ThenIShouldSeeTheUserLoggedInAs(string expectedUserLoggedAs)
        {
            Assert.AreEqual(expectedUserLoggedAs, TopMenu.UserLoggedAs);
        }
    }
}