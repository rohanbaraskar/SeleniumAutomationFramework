using System;
using Insight.Web.BddTests.PageObjects.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Insight.Web.BddTests.PageObjects.Pages.Command
{
    public class CreateComparisonCommand
    {
        public CreateComparisonCommand(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }

        public CreateComparisonCommand WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public CreateComparisonCommand WithComments(string comments)
        {
            Comments = comments;
            return this;
        }

        public CreateComparisonCommand WithWorkbookPath(string workbookPath)
        {
            BasePage.WaitForElementLoad(By.CssSelector("#workbookText"), 30);
            var workbookField = Driver.Instance.FindElement(By.Id("workbookText"));

            var watchprogress =
                new WebDriverWait(Driver.Instance, new TimeSpan(30)).Until(e => e.FindElement(By.CssSelector("#workbookText"))).Text.Trim().Equals(workbookPath);

            Assert.IsTrue(watchprogress);

            Assert.AreEqual(workbookField.Text, workbookPath);
            return this;
        }

        public void Next()
        {
            #region Textbox
            BasePage.WaitForElementLoad(By.CssSelector("#Name"), 30);
            BasePage.WaitForElementLoad(By.CssSelector("#Description"), 30);
            BasePage.WaitForElementLoad(By.CssSelector("#Comments"), 30);

            

            // Name
            var nameField = Driver.Instance.FindElement(By.Id("Name"));
            nameField.SendKeys(Name);

            // Description
            var descriptionField = Driver.Instance.FindElement(By.Id("Description"));
            descriptionField.SendKeys(Description);

            // Comments
            var commentsField = Driver.Instance.FindElement(By.Id("Comments"));
            commentsField.SendKeys(Comments);

            #endregion

            #region Button
            BasePage.WaitForElementLoad(By.CssSelector("#stepNext"), 30);
            // Next
            var nextButton = Driver.Instance.FindElement(By.Id("stepNext"));
            nextButton.Click();

            #endregion
        }
    }
}