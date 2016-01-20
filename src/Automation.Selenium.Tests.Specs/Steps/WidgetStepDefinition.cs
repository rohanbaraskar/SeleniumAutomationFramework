using Insight.Web.BddTests.PageObjects.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Insight.Web.BddTests.Specs.Steps
{
    [Binding]
    public class WidgetStepDefinition
    {
        [When(@"I select ""(.*)"" comparison inside ""(.*)"" workbook")]
        public void WhenISelectComparisonInsideWorkbook(string comparisonName, string workbookName)
        {
            BasePage.WaitForElementLoad(By.CssSelector("#SearchGrid"), 30);
            Assert.IsTrue(MainPage.FindComparisonPage(workbookName, comparisonName));
            ScenarioContext.Current["comparisonName"] = comparisonName;
        }

        [When(@"I edit ""(.*)"" analytics")]
        public void WhenIEditAnalytics(string analytics)
        {
            var comparison = ScenarioContext.Current["comparisonName"];
            MainPage.OpenDashboardAndAnalytics(comparison.ToString(), analytics);
        }

        [When(@"I switch the new browser and should be on ""(.*)"" analysis page")]
        public void WhenISwitchTheNewBrowserAndShouldBeOnAnalysisPage(string analysisNumber)
        {
            Assert.IsTrue(WidgetPage.ChangeWindowAndVeryifyAnalysisPage(analysisNumber));
        }

        [Then(@"I should see the following values on the widget")]
        public void ThenIShouldSeeTheFollowingValuesOnTheWidget(Table expectedValues)
        {
            var s =
                WidgetPage.GenerateNephilaStatsTable(
                    WidgetPage.ChangeIframeAndreturnDataFromWidget(
                        ".section-content .widget:nth-of-type(2) .chartContainer"));

            expectedValues.CompareToSet(s);
        }

        [Then(@"I should see the following values on the widget with (.*) decimal places")]
        public void ThenIShouldSeeTheFollowingValuesOnTheWidgetWithDecimalPlaces(int decimalPlaces, Table expectedValues)
        {
            var s =
                WidgetPage.GenerateNephilaStatsTable(
                    WidgetPage.ChangeIframeAndreturnDataFromWidget(
                        ".section-content .widget:nth-of-type(3) .chartContainer"));
            expectedValues.CompareToSet(s);
        }
    }
}