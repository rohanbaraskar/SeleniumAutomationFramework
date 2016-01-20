using Automation.Selenium.Tests.Common.Utils;
using Automation.Selenium.Tests.PageObjects.PageFactory;
using TechTalk.SpecFlow;

namespace Automation.Selenium.Tests.Specs.StepDefinitions
{
    [Binding]
    public class CommonStepDefinition
    {
        [Given(@"that I am on the ""(.*)"" page")]
        public void GivenThatIAmOnThePage(Constants.Page page)
        {
            switch (page)
            {
                case Constants.Page.Home:
                    {
                        Pages.HomePage.Goto();
                    }
                    break;
            }
        }

        [When(@"I navigate to ""(.*)""")]
        public void WhenINavigateTo(Constants.Page page)
        {
            switch (page)
            {
                case Constants.Page.Home:
                    {
                        Pages.HomePage.Goto();
                    }
                    break;
                case Constants.Page.Bag:
                    {
                        Pages.Header.NavigateToBag();
                    }
                    break;
            }
        }
    }
}