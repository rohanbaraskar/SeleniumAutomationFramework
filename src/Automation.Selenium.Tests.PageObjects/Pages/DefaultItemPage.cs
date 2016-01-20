using System.Collections.Generic;
using Automation.Selenium.Tests.Common.Helpers;
using Automation.Selenium.Tests.Common.Selenium.Base;
using Automation.Selenium.Tests.PageObjects.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.Selenium.Tests.PageObjects.Pages
{
    public class DefaultItemPage : BasePage
    {
        #region Properties

        protected override string PageName
        {
            get { return "Default Item Page"; }
        }

        protected override string RelativeUrl
        {
            get { return "/men/shirts/check-shirts/red-check-casual-double-pocket-shirt-290412"; }
        }

        protected override string Title
        {
            get { return "Red check casual double pocket shirt - check shirts - shirts - men"; }
        }

        #endregion

        #region Elements

        [FindsBy(How = How.CssSelector, Using = ".right-side .button-green")]
        private IWebElement AddToBag { get; set; }

        [FindsBy(How = How.Id, Using = "SizeKey")]
        private IWebElement Size { get; set; }

        #endregion

        #region Methods

        public void AddItemToBasket()
        {
            SelectSize("Size M (UK)");
            SelectQuantity("1");
            SeleniumHelper.Click(AddToBag);
        }

        public void SelectSize(string size)
        {
            var dictionaryOfFields = new Dictionary<string, string>
            {
                {"#SizeKey", size}
            };
            SeleniumHelper.SelectElementFromDropdown(dictionaryOfFields);
        }

        public void SelectQuantity(string quantity)
        {
            var dictionaryOfFields = new Dictionary<string, string>
            {
                {"#Quantity", quantity}
            };
            SeleniumHelper.SelectElementFromDropdown(dictionaryOfFields);
        }

        #endregion
    }
}



