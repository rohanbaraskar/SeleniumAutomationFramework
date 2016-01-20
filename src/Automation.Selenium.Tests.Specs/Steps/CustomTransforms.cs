using System.Collections.Generic;
using Insight.Web.BddTests.Common.Model;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Insight.Web.BddTests.Specs.Steps
{
    [Binding]
    public class CustomTransforms
    {
        [StepArgumentTransformation]
        public Credential CredentialsTransform(Table tableCredential)
        {
            return tableCredential.CreateInstance<Credential>();
        }

        [StepArgumentTransformation]
        public UserDetails UserDetailsTransform(Table tableUserDetails)
        {
            return tableUserDetails.CreateInstance<UserDetails>();
        }

        [StepArgumentTransformation]
        public dynamic DynamicTableTransform(Table messageTable)
        {
            return messageTable.CreateDynamicInstance();
        }

        [StepArgumentTransformation]
        public ComparisonInformation ComparisonInformationTransform(Table tableComparisonInformation)
        {
            return tableComparisonInformation.CreateInstance<ComparisonInformation>();
        }
    }
}