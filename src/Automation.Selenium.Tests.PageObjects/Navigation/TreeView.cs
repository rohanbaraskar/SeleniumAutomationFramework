using System.Linq;
using Insight.Web.BddTests.Common.Utils;
using Insight.Web.BddTests.PageObjects.Pages;
using Insight.Web.BddTests.PageObjects.Selenium;
using OpenQA.Selenium;

namespace Insight.Web.BddTests.PageObjects.Navigation
{
    public class TreeViewPage : BasePage
    {
        /// <summary>
        ///     Sign out of the application
        /// </summary>
        public static void SignOut()
        {
            var listLink = Driver.Instance.FindElements(By.CssSelector(".menuspace>li>a"));
            foreach (var link in listLink.Where(link => Utilities.TrimAndToLower(link.Text) == "sign out"))
            {
                link.Click();
                break;
            }
        }
    }
}