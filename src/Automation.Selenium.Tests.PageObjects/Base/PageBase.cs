using System;
using System.Collections.Generic;
using System.Linq;
using Insight.Web.BddTests.Common.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Insight.Web.BddTests.PageObjects.Base
{
    public abstract partial class PageBase : CommonBase
    {
        // So that we can check that the page URL is correct
        public static string BaseUrl { get; private set; }

        public virtual string Url { get { return ""; } }

        // So that we can check that the page title is correct
        public virtual string DefaultTitle { get { return ""; } }

        protected PageBase()
        {
            BaseUrl = "";
        }

        /// <summary>
        /// Get Instance of the current page
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="driver"></param>
        /// <param name="expectedTitle"></param>
        /// <param name="ignoreTitle"></param>
        /// <returns></returns>
        protected TPage GetInstance<TPage>(IWebDriver driver = null, string expectedTitle = "", bool ignoreTitle = false) where TPage : PageBase, new()
        {
            return GetAndAssertInstance<TPage>(driver ?? Driver, expectedTitle, ignoreTitle);
        }

        /// <summary>
        /// Asserts that the current page is of the given type
        /// </summary>
        public void Is<TPage>() where TPage : PageBase, new()
        {
            if (!(this is TPage))
            {
                throw new AssertionException(string.Format("Page Type Mismatch: Current page is not a '{0}'", typeof(TPage).Name));
            }
        }

        /// <summary>
        /// Inline cast to the given page type
        /// </summary>
        public TPage As<TPage>() where TPage : PageBase, new()
        {

            return (TPage)this;
        }

        #region Common Services

        /// <summary>
        /// In the context of an anonymous user.
        /// </summary>
        /// <returns></returns>
        public TPage OpenPageInNewWindow<TPage>(out string parentWindow) where TPage : PageBase, new()
        {
            // NOTE: CALL ELEMENT.CLICK() HERE for an element that is supposed to open in a new tab
            IEnumerable<string> handles = Driver.WindowHandles;
            var childWindow = handles.ElementAt(1);
            parentWindow = handles.FirstOrDefault();
            Driver.SwitchTo().Window(childWindow);
            return GetInstance<TPage>(Driver);
        }

        #endregion

        #region Private

        /// <summary>
        /// Returns the new page instance and at the same time ensures that the
        /// page URL and the Title matches.
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="driver"></param>
        /// <param name="expectedTitle"></param>
        /// <param name="ignoreTitle"></param>
        /// <returns></returns>
        private static TPage GetAndAssertInstance<TPage>(IWebDriver driver, string expectedTitle = "", bool ignoreTitle = false, bool ignoreUrl = false) where TPage : PageBase, new()
        {
            TPage pageInstance = new TPage()
            {
                Driver = driver,
            };
            PageFactory.InitElements(driver, pageInstance);

            if (string.IsNullOrWhiteSpace(expectedTitle)) expectedTitle = pageInstance.DefaultTitle;

            //Selenium no longer waits for page to load after 2.21
            new WebDriverWait(driver, TimeSpan.FromSeconds(Convert.ToInt32(TestConfig.DefaultTimeOutSeconds)))
                                            .Until(d => d.FindElement(ByChained.TagName("body")));

            if (!ignoreTitle)
            {
                bool isEqual = (expectedTitle == driver.Title);
                //Assert.IsTrue(isEqual, "Page Title");
                Assert.AreEqual(expectedTitle, driver.Title, "Page Title");
            }

            if (!ignoreUrl)
            {
                if (!driver.Title.Contains(pageInstance.Url))
                {
                    //TODO: check url HERE
                }
            }

            return pageInstance;
        }


        #endregion
    }
}
