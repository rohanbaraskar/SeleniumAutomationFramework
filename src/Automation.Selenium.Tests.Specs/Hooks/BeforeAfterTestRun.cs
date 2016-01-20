using Automation.Selenium.Tests.PageObjects;
using TechTalk.SpecFlow;

namespace Automation.Selenium.Tests.Specs.Hooks
{
    [Binding]
    public class BeforeAfterTestRun
    {
        /// <summary>
        ///     Method should be static
        /// </summary>
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            TestBase.Initialize();
        }

        /// <summary>
        ///     Method should be static
        /// </summary>
        [AfterTestRun]
        public static void AfterTestRun()
        {
            TestBase.TestFixtureTearDown();
        }
    }
}