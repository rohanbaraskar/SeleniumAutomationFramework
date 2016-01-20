using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Automation.Selenium.Tests.PageObjects.Configuration
{
    public static class ApplicationSettings
    {
        public static string GetInvalidPassword
        {
            get { return GetSettingValue("InvalidPassword"); }
        }

        public static string GetInvalidUsername
        {
            get { return GetSettingValue("InvalidUsername"); }
        }

        public static string GetDefaultPassword
        {
            get { return GetSettingValue("DefaultPassword"); }
        }

        public static string GetDefaultUsername
        {
            get { return GetSettingValue("DefaultUsername"); }
        }

        public static string GetDefaultFirstName
        {
            get { return GetSettingValue("DefaultFirstName"); }
        }

        public static string GetScreenshotLocation
        {
            get { return GetSettingValue("Screenshot"); }
        }

        public static string WebServiceUrl
        {
            get { return GetSettingValue("WebServiceAPICall"); }
        }

        public static string WebServiceDataExpectedResults
        {
            get { return GetSettingValue("WebServiceGetExpectedResults"); }
        }

        public static string WebServiceKey
        {
            get { return GetSettingValue("WebServiceKey"); }
        }

        public static string GetVerbosity
        {
            get { return GetSettingValue("Verbosity"); }
        }

        public static string GetReportEmailAddress
        {
            get { return GetSettingValue("ReportEmailAddress"); }
        }

        public static string GetSmtpClient
        {
            get { return GetSettingValue("SMTPClient"); }
        }

        public static string GetSmtpEmailAddress
        {
            get { return GetSettingValue("SMTPEmailAddress"); }
        }

        public static string GetSmtpEmailPassword
        {
            get { return GetSettingValue("SMTPPassword"); }
        }

        public static string GetSmtpServerPort
        {
            get { return GetSettingValue("SMTPServerPort"); }
        }

        public static bool IsSeleniumGrid
        {
            get { return Convert.ToBoolean(GetSettingValue("Grid:SeleniumGrid")); }
        }

        public static string SeleniumGridBrowser
        {
            get { return GetSettingValue("Grid:SeleniumGridBrowser"); }
        }

        public static string SeleniumGridPlatform
        {
            get { return GetSettingValue("Grid:SeleniumGridPlatform"); }
        }

        public static string SeleniumGridURL
        {
            get { return GetSettingValue("Grid:SeleniumGridURL"); }
        }

        public static bool GetSauceLabTestRunner
        {
            get
            {
                var section = ConfigurationManager.GetSection("sauceLabs") as NameValueCollection;
                var value = section != null && Convert.ToBoolean(section["SauceLabTestRunner"]);
                return value;
            }
        }

        public static string GetSauceLabOperatingSystem
        {
            get
            {
                var section = ConfigurationManager.GetSection("sauceLabs") as NameValueCollection;
                return section != null ? section["os"] : null;
            }
        }

        public static string GetSauceLabOperatingSystemVersion
        {
            get
            {
                var section = ConfigurationManager.GetSection("sauceLabs") as NameValueCollection;
                return section != null ? section["osVersion"] : null;
            }
        }

        public static bool GetSauceLabDesktopTest
        {
            get
            {
                var section = ConfigurationManager.GetSection("sauceLabs") as NameValueCollection;
                return section != null && Convert.ToBoolean(section["DesktopTest"]);
            }
        }

        public static string GetSauceLabBrowserName
        {
            get
            {
                var section = ConfigurationManager.GetSection("sauceLabs") as NameValueCollection;
                return section != null ? section["browserName"] : null;
            }
        }

        public static string GetSauceLabResolution
        {
            get
            {
                var section = ConfigurationManager.GetSection("sauceLabs") as NameValueCollection;
                return section != null ? section["resolution"] : null;
            }
        }

        public static string GetSauceLabBrowserVersion
        {
            get
            {
                var section = ConfigurationManager.GetSection("sauceLabs") as NameValueCollection;
                return section != null ? section["browserVersion"] : null;
            }
        }

        public static string GetSauceLabJavascriptEnabled
        {
            get
            {
                var section = ConfigurationManager.GetSection("sauceLabs") as NameValueCollection;
                return section != null ? section["JavaScriptEnabled"] : null;
            }
        }

        public static string GetSauceLabDeviceName
        {
            get
            {
                var section = ConfigurationManager.GetSection("sauceLabs") as NameValueCollection;
                return section != null ? section["deviceName"] : null;
            }
        }

        public static string GetSauceLabDeviceOrientation
        {
            get
            {
                var section = ConfigurationManager.GetSection("sauceLabs") as NameValueCollection;
                return section != null ? section["deviceOrientation"] : null;
            }
        }

        public static string GetSauceLabDeviceOsVersion
        {
            get
            {
                var section = ConfigurationManager.GetSection("sauceLabs") as NameValueCollection;
                return section != null ? section["deviceOSVersion"] : null;
            }
        }

        public static string GetSauceLabDevicePlatform
        {
            get
            {
                var section = ConfigurationManager.GetSection("sauceLabs") as NameValueCollection;
                return section != null ? section["devicePlatform"] : null;
            }
        }

        public static string GetSauceLabDeviceType
        {
            get
            {
                var section = ConfigurationManager.GetSection("sauceLabs") as NameValueCollection;
                return section != null ? section["deviceType"] : null;
            }
        }

        public static string GetLocalBrowser
        {
            get
            {
                var section = ConfigurationManager.GetSection("localBrowser") as NameValueCollection;
                return section != null ? section["localDriver"].ToLower() : null;
            }
        }

        public static string GetIntegrationDbConnectionString
        {
            get
            {
                var section = ConfigurationManager.GetSection("integrationConnections") as NameValueCollection;
                return section != null ? section["dbIntegrationTesting"] : null;
            }
        }

        public static bool CheckBugsShouldBeRaised
        {
            get
            {
                var section = ConfigurationManager.GetSection("TFSBugLogging") as NameValueCollection;
                return section != null && Convert.ToBoolean(section["CreateBugs"]);
            }
        }

        public static string GetEnvironmentBaseUrl()
        {
            return GetSettingValue("EnvironmentBaseUrl");
        }

        public static string GetLogDirectory()
        {
            return GetSettingValue("LogRootDirectory");
        }

        private static string GetSettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}