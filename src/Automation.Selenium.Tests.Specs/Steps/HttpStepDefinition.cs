using Insight.Web.BddTests.Common.Configuration;
using Insight.Web.BddTests.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Insight.Web.BddTests.Specs.Steps
{
    [Binding]
    public class HttpStepDefinition
    {
        [Given(@"the Insight application is running")]
        public void GivenTheInsightApplicationIsRunning()
        {
            var httpResponseCode = HttpHelper.GetInstance.GetResponseCode(TestConfig.LoginPage, HttpHelper.HttpType.Get);
            Assert.AreEqual(200, httpResponseCode);
        }
    }
}