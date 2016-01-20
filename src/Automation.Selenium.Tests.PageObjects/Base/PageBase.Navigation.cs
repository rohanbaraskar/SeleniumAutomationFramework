using Insight.Web.BddTests.Common.Configuration;
using Insight.Web.BddTests.PageObjects.Pages;
using OpenQA.Selenium;

namespace Insight.Web.BddTests.PageObjects.Base
{
    public abstract partial class PageBase
    {
        public static LoginPage LoadLoginPage(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(TestConfig.LoginPage);
            return GetAndAssertInstance<LoginPage>(driver);
        }
    }
}
