using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Automation.Selenium.Tests.Specs.StepDefinitions
{
    [Binding]
    public class CustomTransforms
    {
        [StepArgumentTransformation]
        public dynamic DynamicTableTransform(Table messageTable)
        {
            return messageTable.CreateDynamicInstance();
        }
    }
}