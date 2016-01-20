using System.Linq;
using Insight.Web.BddTests.Common.Utils;
using Insight.Web.BddTests.PageObjects.Selenium;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Insight.Web.BddTests.PageObjects.Navigation
{
    public class TopMenu
    {
        public static string UserLoggedAs
        {
            get
            {
                var loggedAsName = Driver.Instance.FindElement(By.CssSelector(".menuspace>li>b"));
                return !string.IsNullOrEmpty(loggedAsName.Text) ? loggedAsName.Text.Trim() : string.Empty;
            }
        }

        /// <summary>
        ///     Try to sign out. Wrapped in a try catch because this method will be used
        ///     inside the After Test Run hooks
        /// </summary>
        public static void SignOut()
        {
            var tags = ScenarioContext.Current.ScenarioInfo.Tags;
            if (tags.Contains("nosignout")) return;

            var listLink = Driver.Instance.FindElements(By.CssSelector(".menuspace>li>a"));
            foreach (var link in listLink.Where(link => Utilities.TrimAndToLower(link.Text) == "sign out"))
            {
                link.Click();
                break;
            }
        }
    }
}