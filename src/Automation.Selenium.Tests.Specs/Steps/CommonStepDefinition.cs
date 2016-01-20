using Insight.Web.BddTests.Common.Utils;
using Insight.Web.BddTests.PageObjects.Pages;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Insight.Web.BddTests.Specs.Steps
{
    [Binding]
    public class CommonStepDefinition
    {
        [Then(@"I should stay on the Insight ""(.*)"" page")]
        [Then(@"I should be on the Insight ""(.*)"" page")]
        [Given(@"I should be on the Insight ""(.*)"" page")]
        public void ThenIShouldBeOnTheInsightPage(Constants.Page pageType)
        {
            string expectedPageTitle;

            switch (pageType)
            {
                case Constants.Page.Main:
                {
                    expectedPageTitle = Constants.PageTitle.Main;
                }
                    break;
                case Constants.Page.Login:
                {
                    expectedPageTitle = Constants.PageTitle.Login;
                }
                    break;
                case Constants.Page.ForgotPassword:
                {
                    expectedPageTitle = Constants.PageTitle.ForgotPassword;
                }
                    break;

                default:
                {
                    throw new AssertionException(pageType + " not defined in the test framework!");
                }
            }

            Assert.IsTrue(BasePage.GetCurrentPageTitle().StartsWith(expectedPageTitle), "Page title not as expected.");
        }
    }
}
