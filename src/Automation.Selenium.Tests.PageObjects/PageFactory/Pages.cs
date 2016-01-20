using Automation.Selenium.Tests.Drivers;
using Automation.Selenium.Tests.PageObjects.Pages;
using Automation.Selenium.Tests.Drivers;
using Automation.Selenium.Tests.PageObjects.Navigation;

namespace Automation.Selenium.Tests.PageObjects.PageFactory
{
    public static class Pages
    {
        public static T GetPage<T>() where T : new()
        {
            var page = new T();
            OpenQA.Selenium.Support.PageObjects.PageFactory.InitElements(SeleniumDriver.Instance, page);
            return page;
        }

        public static HomePage HomePage
        {
            get { return GetPage<HomePage>(); }
        }

        public static SearchResultPage SearchResultPage
        {
            get { return GetPage<SearchResultPage>(); }
        }

        public static ItemPage ItemPage
        {
            get { return GetPage<ItemPage>(); }
        }

        public static DefaultItemPage DefaultItemPage
        {
            get { return GetPage<DefaultItemPage>(); }
        }

        public static Header Header
        {
            get { return GetPage<Header>(); }
        }
    }
}