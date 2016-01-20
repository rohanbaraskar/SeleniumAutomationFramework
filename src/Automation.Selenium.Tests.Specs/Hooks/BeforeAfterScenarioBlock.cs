using TechTalk.SpecFlow;

namespace Automation.Selenium.Tests.Specs.Hooks
{
    [Binding]
    public class BeforeAfterScenarioBlock
    {
        [BeforeScenarioBlock]
        public void BeforeScenarioBlock()
        {
        }

        [AfterScenarioBlock]
        public void AfterScenarioBlock()
        {
        }
    }
}
