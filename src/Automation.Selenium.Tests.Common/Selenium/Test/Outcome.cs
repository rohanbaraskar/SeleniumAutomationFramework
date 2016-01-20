using Automation.Selenium.Tests.Common.Test;

namespace Automation.Selenium.Tests.Common.Selenium.Test
{
    public class Outcome
    {
        public Outcome(string description, bool itemResult, Verbosity.Level verbosity)
        {
            Description = description;
            ItemResult = itemResult;
            VerbosityLevel = verbosity;
        }

        public Outcome(string description, bool itemResult)
        {
            Description = description;
            ItemResult = itemResult;
            VerbosityLevel = Verbosity.Level.Null;
        }

        public string Description { get; set; }
        public bool ItemResult { get; set; }
        public Verbosity.Level VerbosityLevel { get; set; }
    }
}
