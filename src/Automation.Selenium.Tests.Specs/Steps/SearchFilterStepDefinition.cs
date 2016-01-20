using Insight.Web.BddTests.Common.Utils;
using Insight.Web.BddTests.PageObjects.Pages;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Insight.Web.BddTests.Specs.Steps
{
    [Binding]
    public class SearchFilterStepDefinition
    {
        [When(@"I search for an analysis with the filter ""(.*)""")]
        public void WhenISearchForAnAnalysisWithTheFilter(string searchFilterText)
        {
            CreateComparisonPage.AddSearchFilter(searchFilterText);
        }

        [Then(@"I should only see analysis with Id ""(.*)"" in the list")]
        public void ThenIShouldOnlySeeAnalysisWithIdInTheList(string analysis)
        {
            BasePage.WaitForAjax(10, true);
            Assert.IsTrue(CreateComparisonPage.IsContainsAnalysisId("90499"), "Analysis not found on the list");
        }

        [Then(@"I should see a list of analysis ""(.*)"" starting with ""(.*)""")]
        public void ThenIShouldSeeAListOfAnalysisWithStarting(Constants.SearchResultColoumnName coloumnName, string searchFilterText)
        {
            BasePage.WaitForAjax(10, true);

            switch (coloumnName)
            {
                case Constants.SearchResultColoumnName.Id:
                    {
                        Assert.IsTrue(CreateComparisonPage.IsAnalysisIdStartsWith(searchFilterText),
                            "All analysis Id on list does not begin with " + searchFilterText);
                    }
                    break;
                case Constants.SearchResultColoumnName.Name:
                    {

                        Assert.IsTrue(CreateComparisonPage.IsAnalysisNameStartsWith(searchFilterText),
                            "All analysis Name on list does not begin with " + searchFilterText);
                    }
                    break;
                case Constants.SearchResultColoumnName.Definition:
                    {
                        Assert.IsTrue(CreateComparisonPage.IsAnalysisDefinitionStartsWith(searchFilterText),
                            "All analysis Definition on list does not begin with " + searchFilterText);
                    }
                    break;
                default:
                    {
                        throw new AssertionException("Field not defined in test framework.");
                    }
            }
        }

        [Then(@"I should see the message ""(.*)""")]
        public void ThenIShouldSeeTheMessage(string expectedMessage)
        {
            BasePage.WaitForAjax(10, true);
            Assert.IsTrue(CreateComparisonPage.IsNotFoundMessageExist(expectedMessage), "Message " + expectedMessage + " not found");
        }
    }
}