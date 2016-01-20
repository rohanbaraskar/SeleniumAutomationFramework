using Automation.Selenium.Tests.PageObjects;
using TechTalk.SpecFlow;

namespace Automation.Selenium.Tests.Specs.Hooks
{
    [Binding]
    public class BeforeAfterFeature
    {
        [BeforeFeature]
        public static void BeforeFeature()
        {
            TestBase.FeatureInitialize();
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            TestBase.AfterFeature();
        }
    }
}