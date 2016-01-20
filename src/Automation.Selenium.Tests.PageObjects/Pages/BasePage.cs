using System;
using System.Collections.Generic;
using System.Threading;
using Insight.Web.BddTests.PageObjects.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Insight.Web.BddTests.PageObjects.Pages
{
    public class BasePage
    {
        /// <summary>
        ///     Get the page's title
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentPageTitle()
        {
            return Driver.Instance.Title;
        }

        public static string FindElementBy(string css)
        {
            var lblNotificationError = Driver.Instance.FindElement(By.CssSelector(css));
            return lblNotificationError != null ? lblNotificationError.Text : null;
        }

        public static void EnterTextIntoField(Dictionary<string, string> myDictionary)
        {
            foreach (var fieldItem in myDictionary)
            {
                var originalInput = Driver.Instance.FindElement(By.Id(fieldItem.Key));
                originalInput.SendKeys(fieldItem.Value);
            }
        }

        public static void WaitForElementLoad(By by, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(d =>
            {
                var element = Driver.Instance.FindElement(by);
                if (!element.Displayed || !element.Enabled) return null;
                return element;
            });
        }

        public static void WaitForElementCount(By by, int count, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(d =>
            {
                var element = Driver.Instance.FindElements(by).Count == count;
                return element;
            });
        }

        public static void WaitForAjax(int timeoutSecs, bool throwException = false)
        {
            var driver = Driver.Instance;

            for (var i = 0; i < timeoutSecs; i++)
            {
                var ajaxIsComplete = (bool) (driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                if (ajaxIsComplete) return;
                Thread.Sleep(1000);
            }
            if (throwException)
            {
                throw new Exception("WebDriver timed out waiting for AJAX call to complete");
            }
        }

        public static string GetCurrentPageSubHeading()
        {
            var formSubHeading = Driver.Instance.FindElement(By.ClassName("FormSubHeading"));
            return formSubHeading.Text;
        }

        public static void ChangeBrowserInstanceToNewWindow(string windowName)
        {
            var baseWindow = Driver.Instance.CurrentWindowHandle;
            var handles = Driver.Instance.WindowHandles;
            foreach (var handle in handles)
            {
                if (handle != baseWindow)
                {
                    if (
                        Driver.Instance.SwitchTo()
                            .Window(handle)
                            .Title.Equals(windowName))
                        break;
                }
            }
        }
    }
}