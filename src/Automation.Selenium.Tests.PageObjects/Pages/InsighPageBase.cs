using System.Linq;
using Insight.Web.BddTests.Common.Utils;
using Insight.Web.BddTests.PageObjects.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;

namespace Insight.Web.BddTests.PageObjects.Pages
{
    public class InsighPageBase : PageBase
    {
        public string UserLoggedAs
        {
            get
            {
                return LblLoggedInAs.Text;
            }
        }

        #region Elements

        [FindsBy(How = How.CssSelector, Using = ".menuspace>li>b")]
        private IWebElement LblLoggedInAs { get; set; }

        #endregion

        /// <summary>
        ///     Try to sign out. Wrapped in a try catch because this method will be used
        ///     inside the After Test Run hooks
        /// </summary>
        public void SignOut()
        {
            var tags = ScenarioContext.Current.ScenarioInfo.Tags;
            if (tags.Contains("nosignout")) return;

            var listLink = Driver.FindElements(By.CssSelector(".menuspace>li>a"));
            foreach (var link in listLink.Where(link => Utilities.TrimAndToLower(link.Text) == "sign out"))
            {
                link.Click();
                break;
            }
        }

        #region Actions

        #endregion

        #region Validations

        

        #endregion
    }
}