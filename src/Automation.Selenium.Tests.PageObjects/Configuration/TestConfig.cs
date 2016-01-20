using System.Configuration;

namespace Automation.Selenium.Tests.PageObjects.Configuration
{
    /// <summary>
    ///     To refactor: combine appsettings and this one
    /// </summary>
    public class TestConfig
    {
        public static string SecurityConnString
        {
            get { return ConfigurationManager.ConnectionStrings["Database:Security"].ConnectionString; }
        }

        public static string MailerConnString
        {
            get { return ConfigurationManager.ConnectionStrings["Mailer"].ConnectionString; }
        }

        public static string CurrentEnvironment
        {
            get { return ConfigurationManager.AppSettings["Test:Environment"]; }
        }

        public static string WebDriverType
        {
            get { return ConfigurationManager.AppSettings["Insight:WebDriverType"]; }
        }

        public static string DriverServerPath
        {
            get { return ConfigurationManager.AppSettings["Insight:DriverServerPath"]; }
        }

        public static string LoginPage
        {
            get { return ConfigurationManager.AppSettings["Insight:LoginPage"]; }
        }

        public static string ResetPasswordPage
        {
            get
            {
                return ConfigurationManager.AppSettings["Insight:LoginPage"] +
                       ConfigurationManager.AppSettings["Insight:ResetPassword"];
            }
        }

        public static string ChangePasswordPage
        {
            get
            {
                return ConfigurationManager.AppSettings["Insight:LoginPage"] +
                       ConfigurationManager.AppSettings["Insight:ChangePassword"];
            }
        }

        public static string DefaultLoginName
        {
            get { return ConfigurationManager.AppSettings["Insight:DefaultLoginName"]; }
        }

        public static string DefaultPassword
        {
            get { return ConfigurationManager.AppSettings["Insight:DefaultLoginPassword"]; }
        }

        public static string DefaultTimeOutSeconds
        {
            get { return "60"; }
        }
    }
}