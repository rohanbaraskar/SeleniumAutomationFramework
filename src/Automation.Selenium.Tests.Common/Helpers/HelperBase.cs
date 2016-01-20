using Automation.Selenium.Tests.Common.Logging;
using Automation.Selenium.Tests.Drivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace Automation.Selenium.Tests.Common.Helpers
{
    public class HelperBase : NephilaLogger
    {
        public static void WaitForElementLoad(By by, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(SeleniumDriver.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(d =>
            {
                var element = SeleniumDriver.Instance.FindElement(by);
                if (!element.Displayed || !element.Enabled) return null;
                return element;
            });
        }

        public static void WaitForElementLoad(IWebElement newComparisonLink)
        {
            var wait = new WebDriverWait(SeleniumDriver.Instance, TimeSpan.FromSeconds(10));
            wait.Until(d =>
            {
                if (!newComparisonLink.Displayed || !newComparisonLink.Enabled) return null;
                return newComparisonLink;
            });
        }

        public static void WaitForAjax(int timeoutSecs, bool throwException = false)
        {
            var driver = SeleniumDriver.Instance;

            for (var i = 0; i < timeoutSecs; i++)
            {
                var ajaxIsComplete = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                if (ajaxIsComplete) return;
                Thread.Sleep(1000);
            }
            if (throwException)
            {
                throw new Exception("WebDriver timed out waiting for AJAX call to complete");
            }
        }

        public static void WaitForAjax()
        {
            var driver = SeleniumDriver.Instance;

            for (var i = 0; i < 60; i++)
            {
                var ajaxIsComplete = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                if (ajaxIsComplete) return;
                Thread.Sleep(1000);
            }
            if (true)
            {
                throw new Exception("WebDriver timed out waiting for AJAX call to complete");
            }
        }


        public static bool FindComparisonPage(string workbookName, string comparisonName)
        {         
            WaitForElementLoad(By.CssSelector("#tree"), 30);
            var workbookItem = SeleniumHelper.FindElements(By.CssSelector(".tree .jstree-open:first-of-type li a"));
            
            foreach (var item in workbookItem.Where(item => item.Text.Contains(workbookName)))
            {
                item.Click();
            }

            WaitForAjax();

            var comparisonItem = SeleniumHelper.FindElements(By.CssSelector("#mainGrid .item.group .comparisonAlignment"));

            var result = false;
            foreach (var item in comparisonItem.Where(item => item.Text.Contains(comparisonName)))
            {
                result = true;
            }
            return result;
        }


        public static void ChangeBrowserInstanceToNewWindow(string windowName)
        {
            var baseWindow = SeleniumHelper.GetDriver().CurrentWindowHandle;
            var handles = SeleniumHelper.GetDriver().WindowHandles;
            foreach (var handle in handles)
            {
                if (handle != baseWindow)
                {
                    if (SeleniumHelper.SwitchWindowHandleByTitle(windowName))
                        break;
                }
            }
        }

    }
}