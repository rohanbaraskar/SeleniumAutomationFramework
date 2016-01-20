using System.Linq;
using System.Text.RegularExpressions;
using Insight.Web.BddTests.Common.Model;
using Insight.Web.BddTests.Common.Utils;
using Insight.Web.BddTests.PageObjects.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using StringAssert = Microsoft.VisualStudio.TestTools.UnitTesting.StringAssert;

namespace Insight.Web.BddTests.Specs.Steps
{
    [Binding]
    internal class ComparisonPageStepDefinition
    {
        [Given(@"I create an Empty Comparison on the ""(.*)"" folder")]
        [Then(@"I create an Empty Comparison on the ""(.*)"" folder")]
        public void WhenICreateAnEmptyComparisonOnTheFolder(string folderName)
        {
            BasePage.WaitForElementLoad(By.CssSelector("#SearchGrid"), 30);
            MainPage.GoToCreateComparison(folderName);
        }

        [Given(@"I complete the ""(.*)"" wizard with the following details")]
        [When(@"I complete the ""(.*)"" wizard with the following details")]
        public void WhenICompleteTheWizardWithTheFollowingDetails(string pageSubHeading, ComparisonInformation comparisonInformation)
        {
            CreateComparisonPage.IsLoaded(pageSubHeading);

            BasePage.WaitForElementLoad(By.CssSelector("#FormContainer h3"), 30);
            BasePage.WaitForElementLoad(By.CssSelector("#stepNext"), 30);

            CreateComparisonPage.SubmitWithName(comparisonInformation.Name)
                .WithDescription(comparisonInformation.Description)
                .WithComments(comparisonInformation.Comments)
                .WithWorkbookPath(comparisonInformation.Workbook)
                .Next();
        }

        [When(@"I add the first analyis from the list and save the comparison")]
        public void WhenIAddTheFirstAnalyisFromTheListAndSaveTheComparison()
        {
            CreateComparisonPage.IsLoaded("Select Analysis");

            CreateComparisonPage.SelectFirstAnalysis();
            CreateComparisonPage.Add();
            CreateComparisonPage.Save();
            CreateComparisonPage.Done();
        }

        [When(@"I click on Add without selecting an analysis from the list")]
        public void WhenIClickOnAddWithoutSelectingAnAnalysisFromTheList()
        {
            CreateComparisonPage.IsLoaded("Select Analysis");
            CreateComparisonPage.Add();
        }

        [Then(@"I should see a comparison with the name ""(.*)"" under workbook ""(.*)""")]
        public void ThenIShouldSeeAComparisonWithTheNameUnderWorkbook(string comparisonName, string folderName)
        {
            Assert.IsTrue(MainPage.IsComparisonExist(folderName, comparisonName), "Comparison does not exist.");
        }

        [Then(@"I should see the error message ""(.*)"" appear")]
        public void ThenIShouldSeeTheErrorMessageAppear(string errorMessage)
        {
            Assert.IsTrue(CreateComparisonPage.IsErrorExist(errorMessage),
                Constants.MessageNotFound.ErrorMessageNotFound);
        }

        [Then(@"the ""(.*)"" field should be highlighted in red")]
        public void ThenTheFieldShouldBeHighlightedInRed(string fieldName)
        {
            string actualColour;
            switch (fieldName)
            {
                case Constants.ComparisonPageField.SelectedAnalysis:
                    {
                        actualColour = CreateComparisonPage.SelectedAnalysisValidationColor;
                    }
                    break;
                case Constants.ComparisonPageField.Name:
                    {
                        actualColour = CreateComparisonPage.NameValidationColor;
                    }
                    break;
                default:
                    {
                        throw new AssertionException("No field name defined in test framework");
                    }
            }

            Assert.IsNotNull(actualColour);

            Assert.AreEqual(Constants.RgbaColour.RedTwo, actualColour);
        }

        [Then(@"I select the following ""(.*)"" tag on the ""(.*)"" page")]
        [When(@"I select the following ""(.*)"" tag on the ""(.*)"" page")]
        [Given(@"I select the following ""(.*)"" tag on the ""(.*)"" page")]
        public void WhenISelectTheFollowingTagOnThePage(string selectedTag, string formTitle)
        {
            var tags = selectedTag.Split();

            CreateComparisonPage.OpenCloseTagWindow();

            foreach (var tag in tags)
            {
                CreateComparisonPage.AddTagToAnalysis(tag);
            }
            CreateComparisonPage.OpenCloseTagWindow();
        }

        [Then(@"all the shown analysis should contain the following tags ""(.*)""")]
        public void ThenAllTheShownAnalysisShouldContainTheFollowingTags(string selectedTag)
        {
            BasePage.WaitForAjax(30, true);

            var actualTagsFromAnalysisAsList = CreateComparisonPage.ComparisonTagsList();

            try
            {
                foreach (
                    var analysisTag in
                        actualTagsFromAnalysisAsList.Select(tag => Regex.Replace(tag.Text, "\r\n?|\n", " ")))
                {
                    StringAssert.Contains(analysisTag, selectedTag);
                }
            }
            catch (AssertFailedException e)
            {
                throw new AssertFailedException(e.Message);
            }
        }

        [Then(@"all analysis should be shown regardless of selected tags")]
        public void ThenAllAnalysisShouldBeShownRegardlessOfSelectedTags()
        {
            ThenAllTheShownAnalysisShouldContainTheFollowingTags("");
        }
    }
}