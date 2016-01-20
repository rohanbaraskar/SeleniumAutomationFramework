using System.Collections.Generic;
using Automation.Selenium.Tests.Common.Helpers;
using Automation.Selenium.Tests.Common.Selenium.Base;
using Automation.Selenium.Tests.PageObjects.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Automation.Selenium.Tests.PageObjects.Navigation
{
    public class Header : BasePage
    {
        #region Properties

        protected override string PageName
        {
            get { return "Header"; }
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

        [FindsBy(How = How.CssSelector, Using = "#bagNotification p")]
        private IWebElement AddedToBasketNotification { get; set; }

        [FindsBy(How = How.ClassName, Using = "itemCount")]
        private IWebElement ItemCount { get; set; }

        [FindsBy(How = How.Id, Using = "basket-value")]
        private IWebElement BasketValue { get; set; }

        #endregion

        #region Methods

        public bool AddedToBasketMessageAppears(string expectedMessage)
        {
            return SeleniumHelper.GetText(AddedToBasketNotification).Equals(expectedMessage);
        }

        public List<AddedToBasket> ChangesAfterAddingItemToBasket()
        {
            var getAddedToBasketChanges = new AddedToBasket();
            var addedToBasketChanges = new List<AddedToBasket>();

            getAddedToBasketChanges.Message = SeleniumHelper.GetText(AddedToBasketNotification);
            getAddedToBasketChanges.ItemCount = SeleniumHelper.GetText(ItemCount);
            getAddedToBasketChanges.BasketTotal = SeleniumHelper.GetText(BasketValue);

            addedToBasketChanges.Add(getAddedToBasketChanges);
            return addedToBasketChanges;
        }

        public void NavigateToBag()
        {
            SeleniumHelper.Click(BasketValue);
        }

        #endregion
    }
}






