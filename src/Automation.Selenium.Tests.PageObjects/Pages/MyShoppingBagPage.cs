using System.Collections.Generic;
using Automation.Selenium.Tests.Common.Helpers;
using Automation.Selenium.Tests.Common.Selenium.Base;
using Automation.Selenium.Tests.PageObjects.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.Selenium.Tests.PageObjects.Pages
{
    public class MyShoppingBagPage : BasePage
    {
        #region Properties

        protected override string PageName
        {
            get { return "My Shopping Bag"; }
        }

        protected override string RelativeUrl
        {
            get { return "/bag"; }
        }

        protected override string Title
        {
            get { return "My Shopping Bag"; }
        }

        #endregion

        #region Elements

        [FindsBy(How = How.ClassName, Using = "total-qty")]
        private IWebElement TotalQuantity { get; set; }

        [FindsBy(How = How.ClassName, Using = "sub-price")]
        private IWebElement SubTotal { get; set; }

        #endregion

        #region Methods

        public List<MyShoppingBag> GetTotalQuantityAndSubTotal()
        {
            var getMyShoppingBagDetails = new MyShoppingBag();
            var myShoppingBagDetails = new List<MyShoppingBag>();

            getMyShoppingBagDetails.TotalQuantity = SeleniumHelper.GetText(TotalQuantity);
            getMyShoppingBagDetails.SubTotal = SeleniumHelper.GetText(SubTotal);

            myShoppingBagDetails.Add(getMyShoppingBagDetails);
            return myShoppingBagDetails;
        }
        
        #endregion
    }
}



