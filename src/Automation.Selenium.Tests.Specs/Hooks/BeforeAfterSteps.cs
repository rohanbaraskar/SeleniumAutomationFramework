using Automation.Selenium.Tests.PageObjects;
using TechTalk.SpecFlow;

namespace Automation.Selenium.Tests.Specs.Hooks
{
    public class BeforeAfterSteps
    {
        [BeforeStep]
        public static void BeforeStep()
        {
            TestBase.AddResult(TestBase.GetCurrentPositionText(), true);
        }
    }
}