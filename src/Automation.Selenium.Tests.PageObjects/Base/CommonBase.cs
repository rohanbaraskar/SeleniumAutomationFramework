using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Insight.Web.BddTests.PageObjects.Base
{
    public class CommonBase
    {
        public IWebDriver Driver { get; set; }

        /// <summary>
        ///     Clears the element prior to typing in a given value.
        /// </summary>
        public void ClearAndType(IWebElement element, string value)
        {
            element.Clear();
            element.SendKeys(value);
        }

        #region Waits

        /// <summary>
        ///     Wait upto a specified milliseconds
        ///     Uses Selenium WebDriverWait for specified milliseconds until a specified element appears
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <param name="bys"></param>
        /// <param name="elementDescription"></param>
        public void WaitUpTo(int milliseconds, By[] bys, string elementDescription)
        {
            try
            {
                new WebDriverWait(Driver, TimeSpan.FromMilliseconds(milliseconds))
                    .Until(d => d.FindElement(new ByChained(bys)));
            }
            catch (Exception e)
            {
                e.ToString();
                throw new AssertionException(string.Format("WaitUpTo Failed: Could not find '{0}'", elementDescription));
            }
        }

        /// <summary>
        ///     Waits up to specified milliseconds and a boolean condition.
        ///     Uses Thread.Sleep
        /// </summary>
        public void WaitUpTo(int milliseconds, Func<bool> condition, string description)
        {
            var timeElapsed = 0;

            while (!condition() && timeElapsed < milliseconds)
            {
                Thread.Sleep(100);
                timeElapsed += 100;
            }

            if (timeElapsed >= milliseconds || !condition())
            {
                throw new AssertionException("Timed out while waiting for: " + description);
            }
        }

        #endregion

        #region Assertions

        /// <summary>
        ///     Verifies that a string contains a given sub-string.
        /// </summary>
        public static void AssertContains(string expectedValue, string actualValue)
        {
            Assert.True(actualValue.Contains(expectedValue));
        }

        /// <summary>
        ///     Verfies if the string is equal
        /// </summary>
        /// <param name="expectedValue"></param>
        /// <param name="actualValue"></param>
        /// <param name="message"></param>
        public static void AssertIsEqual(string expectedValue, string actualValue, string message)
        {
            Assert.AreEqual(expectedValue, actualValue, message);
        }

        // NOTE: May Need refactoring - This method actually checks if an element is displayed 
        // as opposed to being present on a page
        // What if the element is null?
        public static bool IsElementPresent(IWebElement element)
        {
            try
            {
                var b = element.Displayed;
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public static void AssertElementPresent(IWebElement element, string elementDescription)
        {
            if (!IsElementPresent(element))
                throw new AssertionException(string.Format("AssertElementPresent Failed: Could not find '{0}'",
                    elementDescription));
        }

        /// <summary>
        ///     Search for an element not present at a page level using the Driver
        ///     NOTE: Searching for something that doesn't exists throws an exception
        ///     NOTE: Its a non static method
        /// </summary>
        public void AssertElementNotPresent(By by, string elementDescription)
        {
            try
            {
                Driver.FindElement(by);

                // Element found so lets throw an exception
                throw new AssertionException(
                    string.Format("AssertElementNotPresent Failed: Element '{0}' exists on the page", elementDescription));
            }
            catch (NoSuchElementException e)
            {
                // Element is not present so nothing to do. The test passes

                e.ToString(); // avoid compile time warnings
            }
        }

        // NOTE: Again as above, this method checks if an element is Displayed as opposed to being present
        //       Although there is a try and catch and the catch will work just fine if an element is not present.
        public static bool IsElementPresent(ISearchContext context, By searchBy)
        {
            try
            {
                var b = context.FindElement(searchBy).Displayed;
                return true;
            }
            catch
            {
                return false;
            }
        }

        // NOTE: Again as above, this method checks if an element is Displayed as opposed to being present
        //       Although there is a try and catch and the catch will work just fine if an element is not present.
        public static bool IsElementPresent(ISearchContext context, By searchBy, int maxDuration)
        {
            var retryInterval = 1000; // 1 sec
            var maxDuration2 = maxDuration;
            var isPresent = false;

            while (maxDuration2 > 0)
            {
                try
                {
                    var b = context.FindElement(searchBy).Displayed;
                    isPresent = true;
                    break;
                }
                catch (Exception ex)
                {
                    Trace.Write(ex.Message);
                }

                Thread.Sleep(retryInterval);
                maxDuration2 -= retryInterval;
            }
            return isPresent;
        }

        public static void AssertElementPresent(ISearchContext context, By searchBy, string elementDescription)
        {
            if (!IsElementPresent(context, searchBy))
                throw new AssertionException(string.Format("AssertElementPresent Failed: Could not find '{0}'",
                    elementDescription));
        }

        /// <summary>
        ///     First checks if the target element is present
        ///     Then checks if the text matches with the expectedValue
        /// </summary>
        /// <param name="element"></param>
        /// <param name="expectedValue"></param>
        /// <param name="elementDescription"></param>
        /// <param name="ignoreCasing"></param>
        public static void AssertElementText(IWebElement element, string expectedValue, string elementDescription,
            bool ignoreCasing = false)
        {
            AssertElementPresent(element, elementDescription);
            var actualtext = element.Text;
            if (ignoreCasing)
            {
                if (actualtext.ToLower() != expectedValue.ToLower())
                {
                    throw new AssertionException(
                        string.Format(
                            "AssertElementText IgnoreCasingTrue Failed: Value for '{0}' did not match expectations. Expected: [{1}], Actual: [{2}]",
                            elementDescription, expectedValue, actualtext));
                }
            }
            else
            {
                if (actualtext != expectedValue)
                {
                    throw new AssertionException(
                        string.Format(
                            "AssertElementText Failed: Value for '{0}' did not match expectations. Expected: [{1}], Actual: [{2}]",
                            elementDescription, expectedValue, actualtext));
                }
            }
        }

        public static IWebElement FindElement(ISearchContext context, By searchBy, string elementDescription)
        {
            IWebElement element = null;
            try
            {
                element = context.FindElement(searchBy);
            }
            catch (NoSuchElementException e)
            {
                e.ToString();

                if (!IsElementPresent(context, searchBy))
                    throw new AssertionException(string.Format("AssertElementPresent Failed: Could not find '{0}'",
                        elementDescription));
            }
            return element;
        }

        // NOTE: Instead of using driverUrl.IndexOf, better approach would be to use driverUrl.Contains
        public void AssertUrlContains(string queryStringVariableNameAndValue)
        {
            var driverUrl = HttpUtility.UrlDecode(Driver.Url);

            var contains =
                driverUrl != null &&
                driverUrl.IndexOf(queryStringVariableNameAndValue, StringComparison.InvariantCultureIgnoreCase) > 0;

            if (!contains)
            {
                throw new AssertionException(
                    string.Format(
                        "AssertUrlContains Failed: Value for '{0}' did not match expectations. Actual Url: [{1}]",
                        queryStringVariableNameAndValue, driverUrl));
            }
        }

        public void AssertTextContains(string expected, string actual, string elementDescription)
        {
            if (!actual.Contains(expected))
            {
                throw new AssertionException(
                    string.Format(
                        "AssertTextContains Failed: Value for '{0}' did not match expectations. Actual Value: {1}",
                        elementDescription, actual));
            }
        }

        /// <summary>
        ///     Throws to assist diagnosis
        /// </summary>
        /// <param name="kvp"></param>
        /// <returns></returns>
        private bool PageSourceContains(KeyValuePair<string, string> kvp)
        {
            var isPresent = Driver.PageSource.Contains(string.Format("'{0}': '{1}'", kvp.Key, kvp.Value));
            if (!isPresent)
            {
                throw new NotFoundException(string.Format("Page Source does not contain key '{0}' value '{1}' pair.",
                    kvp.Key, kvp.Value));
            }
            return true;
        }

        #endregion

        #region Browser Tab Controls

        /// <summary>
        ///     When a link is opened in a new tab,
        ///     we can use this method to select the previous tab
        ///     Assuming only one new tab was opened
        /// </summary>
        public void SelectPreviousTab()
        {
            var handles = Driver.WindowHandles.ToList();
            if (handles.Count > 1)
            {
                // Get handle to the previous tab
                var previousTabHandle = Driver.WindowHandles[0];

                // Close the current tab
                Driver.Close();

                // Switch driver to the previous tab (assuming the only tab open)
                Driver.SwitchTo().Window(previousTabHandle);
            }
        }

        /// <summary>
        ///     Select the newly opened tab.
        ///     Assuming only one new tab was opened
        /// </summary>
        public void SelectNewlyOpenedTab()
        {
            var handles = Driver.WindowHandles.ToList();
            if (handles.Count > 1)
            {
                // Switch the context of the Driver to the newly opened tab
                Driver.SwitchTo().Window(handles.Last());
            }
        }

        #endregion
    }
}