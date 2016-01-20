using System.Configuration;

namespace Insight.Web.BddTests.Common.Configuration
{
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

        public static string LoginPage
        {
            get { return ConfigurationManager.AppSettings["Insight:LoginPage"]; }
        }

        public static string ResetPasswordPage
        {
            get
            {
                return ConfigurationManager.AppSettings["Insight:LoginPage"] + ConfigurationManager.AppSettings["Insight:ResetPassword"];
            }
        }

        public static string ForgotPasswordPage
        {
            get
            {
                return ConfigurationManager.AppSettings["Insight:LoginPage"] +
                       ConfigurationManager.AppSettings["Insight:ForgotPassword"];
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
    }
}