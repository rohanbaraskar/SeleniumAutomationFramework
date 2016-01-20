using Automation.Selenium.Tests.Common.Helpers;
using Automation.Selenium.Tests.Common.Selenium.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.Selenium.Tests.PageObjects.Pages
{
    public class HomePage : BasePage
    {
        #region Properties

        protected override string PageName
        {
            get { return "Home Page"; }
        }

        protected override string RelativeUrl
        {
            get { return "/"; }
        }

        protected override string Title
        {
            get { return "ASOS | Shop the Latest Clothes and Fashion Online"; }
        }

        #endregion

        #region Elements

        [FindsBy(How = How.CssSelector, Using = ".search-dropdown #search-query")]
        private IWebElement SearchQuery { get; set; }

        [FindsBy(How = How.Id, Using = "search-submit")]
        private IWebElement SearchSubmit { get; set; }

        #endregion

        #region Methods

        public void SearchForItem()
        {
            SeleniumHelper.SendKeys(SearchQuery, "Jeans");
            SeleniumHelper.Click(SearchSubmit);
        }
        #endregion
    }
}


