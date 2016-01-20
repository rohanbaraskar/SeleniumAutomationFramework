using System.Collections.Generic;
using Insight.Web.BddTests.Common.Configuration;
using Insight.Web.BddTests.PageObjects.Selenium;
using OpenQA.Selenium;

namespace Insight.Web.BddTests.PageObjects.Pages.Command
{
    public class LoginPageCommand : BasePage
    {
        private readonly string username;
        private string password;

        #region Private Methods

        /// <summary>
        ///     Try to login even if the current user is still in session
        /// </summary>
        private static void TryToLogin()
        {
            try
            {
                var linkForceTheLogout = Driver.Instance.FindElement(By.CssSelector(".notification.tip>a"));
                if (linkForceTheLogout != null)
                {
                    // Force logout
                    linkForceTheLogout.Click();
                }
            }
            catch (NoSuchElementException)
            {
            }
        }

        #endregion

        #region Public Methods

        public LoginPageCommand(string insightUsername)
        {
            username = insightUsername;
        }

        public LoginPageCommand WithPassword(string insightPassword)
        {
            password = insightPassword;
            return this;
        }

        public LoginPageCommand WithDefaultPassword()
        {
            password = TestConfig.DefaultPassword;
            return this;
        }

        /// <summary>
        /// Login into the application with a username and a password
        /// </summary>
        public void Login()
        {
            #region Textbox

            var dictionaryOfFields = new Dictionary<string, string>
            {
                {"Email", username},
                {"Password", password}
            };

            EnterTextIntoField(dictionaryOfFields);

            #endregion

            #region Button

            // Logon
            var loginButton = Driver.Instance.FindElement(By.Id("submit"));
            loginButton.Click();

            // Login or Force Login
            TryToLogin();

            #endregion
        }

        #endregion
    }
}