using Automation.Selenium.Tests.Common.Helpers;
using Automation.Selenium.Tests.Common.Selenium.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.Selenium.Tests.PageObjects.Pages
{
    public class ItemPage : BasePage
    {
        #region Properties

        protected override string PageName
        {
            get { return "Item Page"; }
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

        [FindsBy(How = How.CssSelector, Using = "#SizeKey option:nth-of-type(2)")]
        private IWebElement FirstSize { get; set; }

        #endregion

        #region Methods

        public void AddItemToBasket()
        {
            SeleniumHelper.Click(FirstSize);
        }

        #endregion
    }
}



