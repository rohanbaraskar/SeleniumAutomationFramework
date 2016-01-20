using Automation.Selenium.Tests.Common.Helpers;
using Automation.Selenium.Tests.Common.Selenium.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.Selenium.Tests.PageObjects.Pages
{
    public class SearchResultPage : BasePage
    {
        #region Properties

        protected override string PageName
        {
            get { return "Search Result Page"; }
        }

        protected override string RelativeUrl
        {
            get { return ""; }
        }

        protected override string Title
        {
            get { return ""; }
        }

        #endregion

        #region Elements

        [FindsBy(How = How.CssSelector, Using = ".listing__results .product-spotlight:nth-of-type(2) .products-listing li:first-of-type img")]
        private IWebElement FirstItem { get; set; }

        #endregion

        #region Methods

        public void GoToFirstItem()
        {
            SeleniumHelper.Click(FirstItem);
        }

        #endregion
    }
}


