using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace Automation.Selenium.Tests.Drivers
{
    public static class SeleniumDriver
    {
        #region Properties

        public static IWebDriver WebDriver;

        #endregion

        public static string Title
        {
            get { return Instance.Title; }
        }

        public static string Url
        {
            get { return Instance.Url; }
        }

        public static IWebDriver Instance
        {
            get { return WebDriver ?? (WebDriver = CreateDriver()); }
        }

        public static ISearchContext SearchContextDriver
        {
            get { return Instance; }
        }

        public static bool IsSauceLabsTest { get; set; }
        public static string BaseUrl { get; set; }
        public static bool IsSauceLabsDesktopTest { get; set; }
        public static bool IsSeleniumGrid { get; set; }
        public static string SeleniumGridPlatform { get; set; }
        public static string SeleniumGridURL { get; set; }
        public static string SeleniumGridBrowser { get; set; }
        public static string BrowserVersion { get; set; }
        public static string OperatingSystem { get; set; }
        public static string Resolution { get; set; }
        public static string BrowserName { get; set; }
        public static string DeviceType { get; set; }
        public static string DevicePlatform { get; set; }
        public static string DeviceOsVersion { get; set; }
        public static string DeviceName { get; set; }
        public static string DeviceOrientation { get; set; }
        public static string JavaScriptEnabled { get; set; }
        public static string LocalBrowser { get; set; }
        public static string ScreenshotLocation { get; set; }
        public static string TestName { get; set; }
        public static string[] Tags { get; set; }

        public static IWebDriver CreateDriver()
        {
            // IWebDriver webDriver;
            if (IsSauceLabsTest)
            {
                var capabilities = BuildDesiredCapabilities();
                WebDriver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), capabilities,
                    TimeSpan.FromSeconds(180));
            }
            else if(IsSeleniumGrid)
            {
                //DesiredCapabilities capabilities = DesiredCapabilities.Chrome();
                //ChromeOptions options = new ChromeOptions();
                //options.BinaryLocation = "c:\\chromedriver.exe";
                //capabilities.SetCapability(ChromeOptions.Capability, options);


                DesiredCapabilities capabilities = DesiredCapabilities.Chrome();

              //  dc.SetCapability(CapabilityType.Platform, "WINDOWS");
                WebDriver = new RemoteWebDriver(new Uri(SeleniumGridURL), capabilities,
                        TimeSpan.FromSeconds(60));            
            }
            else
            {
                switch (LocalBrowser.ToLower())
                {
                    case "chrome":
                        WebDriver = new ChromeDriver();
                        break;
                    case "firefox":
                        WebDriver = new FirefoxDriver();
                        break;
                    case "ie":
                        WebDriver = new InternetExplorerDriver();
                        break;
                    default:
                        // if no options set correctly in config then use the default
                        WebDriver = new FirefoxDriver();
                        break;
                }

                // adding in 50 second implicit wait due to selenium grid needing at least 45s for nodes since thats the socket connection time
                WebDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            }
            WebDriver.Manage().Window.Maximize();


            return WebDriver;
        }

        private static DesiredCapabilities BuildDesiredCapabilities()
        {
            DesiredCapabilities dc;

            if (IsSauceLabsDesktopTest)
            {
                switch (BrowserName.ToLower())
                {
                    case "firefox":
                        dc = DesiredCapabilities.Firefox();
                        break;
                    case "chrome":
                        dc = DesiredCapabilities.Chrome();
                        break;
                    case "safari":
                        dc = DesiredCapabilities.Safari();
                        break;
                    case "ie":
                        dc = DesiredCapabilities.InternetExplorer();
                        break;
                    default:
                        dc = DesiredCapabilities.InternetExplorer();
                        break;
                }
                dc.SetCapability(CapabilityType.Version, BrowserVersion);
                dc.SetCapability(CapabilityType.Platform, OperatingSystem);
                dc.SetCapability("screen-resolution", Resolution);
            }
            else
            {
                switch (DeviceType.ToLower())
                {
                    case "android":
                        dc = DesiredCapabilities.Android();
                        break;
                    case "iphone":
                        dc = DesiredCapabilities.IPhone();
                        break;
                    default:
                        dc = DesiredCapabilities.IPhone();
                        break;
                }
                dc.SetCapability(CapabilityType.Platform, DevicePlatform);
                dc.SetCapability(CapabilityType.Version, DeviceOsVersion);
                dc.SetCapability("deviceName", DeviceName);
                dc.SetCapability("device-orientation", DeviceOrientation);
            }

            dc.SetCapability(CapabilityType.IsJavaScriptEnabled, JavaScriptEnabled);
            dc.SetCapability(CapabilityType.HandlesAlerts, "dismiss");
            dc.SetCapability(CapabilityType.TakesScreenshot, "true");
            dc.SetCapability("tags", Tags);
            dc.SetCapability("name", TestName);
            dc.SetCapability("username", "seanrand");
            dc.SetCapability("accessKey", "2002db9c-71fb-4f6a-a691-2e249eea98c2");

            return dc;
        }

        #region Saucelabs

        public static void SetPassFailOfTest(bool pass)
        {
            if (IsSauceLabsTest)
            {
                ((IJavaScriptExecutor) Instance).ExecuteScript("sauce:job-result=" + (pass ? "passed" : "failed"));
            }
        }

        #endregion

        public static void Goto(string url)
        {
            Instance.Url = url != null && url.Trim() == "//" ? BaseUrl : BaseUrl + url;
        }

        public static void Close()
        {
            if (WebDriver == null) return;
            Instance.Quit();
            WebDriver = null;
        }
    }
}