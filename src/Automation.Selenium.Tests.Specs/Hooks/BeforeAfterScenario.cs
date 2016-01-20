using Automation.Selenium.Tests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Automation.Selenium.Tests.Specs.Hooks
{
    [Binding]
    public class BeforeAfterScenario
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            TestBase.TestSetup();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var passedTest = !TestBase.HasError();
            TestBase.TearDown(passedTest);
            Assert.IsTrue(passedTest);
        }
    }
}