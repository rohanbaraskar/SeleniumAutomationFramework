using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Automation.Selenium.Tests.Common.Helpers;
using Automation.Selenium.Tests.Common.Selenium.Test;
using Automation.Selenium.Tests.Drivers;

namespace Automation.Selenium.Tests.Common.Test
{
    public class Result
    {
        private readonly List<Outcome> result;
        private bool reportHigh;
        private bool reportLow;
        private bool reportMedium;

        public Result()
        {
            result = new List<Outcome>();
            SetVerbosity();
        }

        public string VerbosityLevel { get; set; }

        public bool AddResult(string description, bool itemResult, Verbosity.Level verbosity)
        {
            try
            {
                var currentOutcome = new Outcome(description, itemResult, verbosity);
                result.Add(currentOutcome);
                if (!itemResult)
                {
                    //TODO
                    // write a method in here to log a bug in JIRA Calling the API
                    // not sure how to do so yet so leaving as a todo
                    //LogBug(currentOutcome.VerbosityLevel);
                }

                return true;
            }
            catch (NullReferenceException)
            {
                //Logger.WriteExceptionToLog(string.Format("Value {0} has not been added to the string list of results. Exeception thrown {1}", _result, ex.InnerException));
                return false;
            }
        }

        public bool AddResult(string description, bool itemResult)
        {
            try
            {
                var currentOutcome = new Outcome(description, itemResult);
                result.Add(currentOutcome);
                if (!itemResult)
                {
                    //TODO as per the previous Add Result method something as follows
                    //  LogBug(Verbosity.Level.Null);
                }

                return true;
            }
            catch (NullReferenceException)
            {
                // Logger.WriteExceptionToLog(string.Format("Value {0} has not been added to the string list of results. Exeception thrown {1}", _result, ex.InnerException));
                return false;
            }
        }

        private StringBuilder BuildReproSteps()
        {
            var steps = new StringBuilder();

            if (SeleniumDriver.IsSauceLabsTest)
            {
                steps.AppendLine("<p><b>Test Runner - </b> Remote</p>");
                steps.AppendLine(string.Format("<p><b>Remote Browser - </b> {0} - {1}</p>",
                    SeleniumDriver.BrowserName, SeleniumDriver.BrowserVersion));
                steps.AppendLine(string.Format("<p><b>Operating System - </b> {0} </p>",
                    SeleniumDriver.OperatingSystem));
                steps.AppendLine(string.Format("<p><b>Screen Resolution - </b> {0} </p>",
                    SeleniumDriver.Resolution));
            }
            else
            {
                steps.AppendLine("<p><b>Test Runner - </b> Local</p>");
                steps.AppendLine(string.Format("<p><b>Local Browser - </b> {0}</p>", SeleniumDriver.LocalBrowser));
            }
            steps.AppendLine("<br>");
            var step = 1;
            foreach (var item in result)
            {
                if (item.ItemResult)
                {
                    steps.Append("<p>" + step + " - " + item.Description + " - <b><font color=\"green\"> " +
                                 item.ItemResult.ToString().ToUpper() + "</font></b></p>").AppendLine();
                }
                else
                {
                    steps.Append("<p>" + step + " - " + item.Description + " - <b><font color=\"red\"> " +
                                 item.ItemResult.ToString().ToUpper() + "</font></b></p>").AppendLine();
                }
                step++;
            }

            return steps;
        }

        private string BuildBugTitle()
        {
            string testRunner;
            string browser;

            if (SeleniumDriver.IsSauceLabsTest)
            {
                testRunner = "Remote";
                browser = SeleniumDriver.BrowserName;
            }
            else
            {
                testRunner = "Local";
                browser = SeleniumDriver.LocalBrowser;
            }
            var bugTitle = string.Format("{0} [{1}] - {2} - {3} - Assert failed on step {4}", testRunner, browser,
                SeleniumHelper.FeatureName, SeleniumHelper.TestName, result.Count());
            return bugTitle;
        }

        public bool PrintListToLogger()
        {
            try
            {
                //Logger.WriteToLog("Start of Test Results");
                foreach (var item in result)
                {
                    if (item.ItemResult)
                    {
                        //     Logger.WritePassedAssertToLog(string.Format("{0} - [Result - {1}] - [Verbosity - {2}]", item.Description, item.ItemResult, item.VerbosityLevel.ToString()));
                    }
                }
                // Logger.WriteToLog("End of Test Results");
                return true;
            }
            catch (Exception)
            {
                // Logger.WriteExceptionToLog(string.Format("Unable to write to Logger for the List errors due to the following exeception {0}", ex.InnerException));
                return false;
            }
        }

        public bool ClearErrorList()
        {
            try
            {
                if (result != null)
                {
                    //   Logger.WriteInformationToLog("Clearing down the result log");
                    result.Clear();
                }
                return true;
            }
            catch (NullReferenceException)
            {
                // Logger.WriteExceptionToLog(ex.InnerException.ToString());
                return false;
            }
        }

        private void SetVerbosity()
        {
            var verbosityConfig = VerbosityLevel;

            if (string.IsNullOrEmpty(verbosityConfig))
            {
                reportHigh = reportMedium = reportLow = true;
            }
            else
            {
                var verbosityArray = verbosityConfig.ToLower().Replace(" ", "").Split(',');

                foreach (var item in verbosityArray)
                {
                    if (item == Verbosity.Level.High.ToString().ToLower())
                    {
                        reportHigh = true;
                    }
                    if (item == Verbosity.Level.Medium.ToString().ToLower())
                    {
                        reportMedium = true;
                    }
                    if (item == Verbosity.Level.Low.ToString().ToLower())
                    {
                        reportLow = true;
                    }
                }
            }
        }

        public bool HasError()
        {
            var hasError = false;
            var errorCount = 0;

            if (result != null)
            {
                if (reportHigh)
                {
                    errorCount +=
                        result.Count(i => i.ItemResult == false && i.VerbosityLevel == Verbosity.Level.High);
                }
                if (reportMedium)
                {
                    errorCount +=
                        result.Count(i => i.ItemResult == false && i.VerbosityLevel == Verbosity.Level.Medium);
                }
                if (reportLow)
                {
                    errorCount +=
                        result.Count(i => i.ItemResult == false && i.VerbosityLevel == Verbosity.Level.Low);
                }

                errorCount += result.Count(i => i.ItemResult == false && i.VerbosityLevel == Verbosity.Level.Null);

                if (errorCount > 0)
                {
                    hasError = true;
                }
            }

            return hasError;
        }
    }
}