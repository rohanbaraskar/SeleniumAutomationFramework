using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Insight.Web.BddTests.PageObjects.Selenium
{
    public class Driver
    {
        public static IWebDriver Instance { get; set; }

        public static void Initialize()
        {
            Instance = new FirefoxDriver();
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            Instance.Manage().Window.Maximize();
        }

        public static void Close()
        {
            if (Instance != null)
            {
               Instance.Close();
            }
        }
    }
}