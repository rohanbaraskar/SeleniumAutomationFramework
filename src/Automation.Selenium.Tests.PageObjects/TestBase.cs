using System;
using Automation.Selenium.Tests.Common.Helpers;
using Automation.Selenium.Tests.Common.Test;
using Automation.Selenium.Tests.Drivers;
using Automation.Selenium.Tests.PageObjects.Configuration;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Automation.Selenium.Tests.PageObjects
{
    public class TestBase
    {
        public static Result Results;

        public static void FeatureInitialize()
        {
        }

        public static void Initialize()
        {
            Assert.AreEqual(200, HttpHelper.GetInstance.GetResponseCode(TestConfig.LoginPage, HttpHelper.HttpType.Get));
        }

        public static void TestFixtureTearDown()
        {
            if (SeleniumDriver.WebDriver != null)
            {
                SeleniumDriver.Instance.Quit();
            }
        }

        public static void TestSetup()
        {
            Results = new Result
            {
                VerbosityLevel = ApplicationSettings.GetVerbosity
            };
            // set up of properties for desired capabilities
            SeleniumDriver.TestName = ScenarioContext.Current.ScenarioInfo.Title;
            SeleniumHelper.FeatureName = FeatureContext.Current.FeatureInfo.Title;
            SeleniumHelper.Tags = ScenarioContext.Current.ScenarioInfo.Tags;

            SeleniumDriver.BaseUrl = ApplicationSettings.GetEnvironmentBaseUrl();
            SeleniumDriver.IsSeleniumGrid = ApplicationSettings.IsSeleniumGrid;

            // setting driver properties to make a decision on which driver to create.
            SeleniumDriver.IsSauceLabsTest = ApplicationSettings.GetSauceLabTestRunner;
            if (SeleniumDriver.IsSauceLabsTest)
            {
                SeleniumDriver.IsSauceLabsDesktopTest = ApplicationSettings.GetSauceLabDesktopTest;
                SeleniumDriver.JavaScriptEnabled = ApplicationSettings.GetSauceLabJavascriptEnabled;

                if (SeleniumDriver.IsSauceLabsDesktopTest)
                {
                    SeleniumDriver.BrowserName = ApplicationSettings.GetSauceLabBrowserName;
                    SeleniumDriver.BrowserVersion = ApplicationSettings.GetSauceLabBrowserVersion;
                    SeleniumDriver.OperatingSystem = ApplicationSettings.GetSauceLabOperatingSystem;
                    SeleniumDriver.Resolution = ApplicationSettings.GetSauceLabResolution;
                }
                else
                {
                    // must be set in order to device test on iOS and Android
                    SeleniumDriver.DeviceType = ApplicationSettings.GetSauceLabDeviceType;
                    SeleniumDriver.DevicePlatform = ApplicationSettings.GetSauceLabDevicePlatform;
                    SeleniumDriver.DeviceOsVersion = ApplicationSettings.GetSauceLabDeviceOsVersion;
                    SeleniumDriver.DeviceName = ApplicationSettings.GetSauceLabDeviceName;
                    SeleniumDriver.DeviceOrientation = ApplicationSettings.GetSauceLabDeviceOrientation;
                }
            }
            else if(SeleniumDriver.IsSeleniumGrid)
            {
                SeleniumDriver.SeleniumGridBrowser = ApplicationSettings.SeleniumGridBrowser;
                SeleniumDriver.SeleniumGridPlatform = ApplicationSettings.SeleniumGridPlatform;
                SeleniumDriver.SeleniumGridURL = ApplicationSettings.SeleniumGridURL;
            }
            else
            {
                // must be a local test run
                SeleniumDriver.LocalBrowser = ApplicationSettings.GetLocalBrowser;
            }

            SeleniumDriver.ScreenshotLocation = ApplicationSettings.GetScreenshotLocation;


            if (ApplicationSettings.GetSauceLabTestRunner)
            {
                SeleniumDriver.CreateDriver();
            }

            // this needs to be removed if we go down the route of not opening a 
            // new browser for each test. And add at the feature setup this driver.createdriver() 
            if (SeleniumDriver.WebDriver == null)
            {
                SeleniumDriver.CreateDriver();
            }
        }

        public static void TearDown(bool passedTest)
        {
            // For saucelabs only, if its a sauce test we have to close the browsing session
            // also send the result for the sauce labs dashboard.
            // add in browser.close only if we remove the main close from this tear down.
            if (ApplicationSettings.GetSauceLabTestRunner)
            {
                SeleniumDriver.SetPassFailOfTest(passedTest);
            }

            SeleniumDriver.Close();
        }

        public static void AfterFeature()
        {
        }

        public static string GetCurrentPositionText()
        {
            return SeleniumHelper.GetCurrentPositionText();
        }

        public static void AddResult(string description, bool result)
        {
            try
            {
                Results.AddResult(description, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool HasError()
        {
            return Results.HasError();
        }
    }
}