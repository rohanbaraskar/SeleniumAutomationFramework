using System;
using Automation.Selenium.Tests.Common.Logging;
using Automation.Selenium.Tests.Drivers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Selenium.Tests.Common.Selenium.Base
{
    public abstract class BasePage
    {
        /// <summary>
        ///     User friendly name for the page
        /// </summary>
        protected abstract string PageName { get; }

        /// <summary>
        ///     The relative url to the page this should only include the url section after the environment and culture no leading
        ///     slash
        /// </summary>
        protected abstract string RelativeUrl { get; }

        /// <summary>
        ///     The page title this must match title in mark up
        /// </summary>
        protected abstract string Title { get; }

        protected string Url
        {
            get { return "/" + RelativeUrl; }
        }

        public virtual void Goto()
        {
            try
            {
                SeleniumDriver.Goto(Url);
            }
            catch (UriFormatException)
            {
            }
        }

        public virtual void LogDebug(string message)
        {
            NephilaLogger.Log.Debug(message);
        }

        public virtual bool IsAt()
        {
            try
            {
                var result = false;

                if (SeleniumDriver.Title != null)
                {
                    if (SeleniumDriver.Title.Contains(Title))
                    {
                        result = true;
                    }
                }

                Assert.IsTrue(result, "IsAt() Method, Title does contain {0}, URL: {1}", Title, SeleniumDriver.Url);

                //TestBase.Results.AddResult(
                //    string.Format("IsAt() Method, Title does contain {0}, URL: {1}", Title, SeleniumDriver.Url), result,
                //    Verbosity.Level.High);
                return result;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }
    }
}