using System.Collections.Generic;
using Insight.Web.BddTests.PageObjects.Selenium;
using OpenQA.Selenium;

namespace Insight.Web.BddTests.PageObjects.Pages.Command
{
    public class ResetPasswordPageCommand : BasePage
    {
        private readonly string firstname;
        private string email;
        private string lastname;
        private string username;

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

        public ResetPasswordPageCommand(string firstName)
        {
            firstname = firstName;
        }

        public ResetPasswordPageCommand WithLastName(string lastName)
        {
            lastname = lastName;
            return this;
        }

        public ResetPasswordPageCommand WithEmail(string emailAddress)
        {
            email = emailAddress;
            return this;
        }

        public ResetPasswordPageCommand WithUsername(string userName)
        {
            username = userName;
            return this;
        }
        #endregion
        /// <summary>
        ///     Login into the application with a username and a password
        /// </summary>
        public void Submit()
        {
            #region Textbox

            var dictionaryOfFields = new Dictionary<string, string>
            {
                {"FirstName", firstname},
                {"LastName", lastname},
                {"Email", email},
                {"Username", username}
            };


            EnterTextIntoField(dictionaryOfFields);

            var loginButton = Driver.Instance.FindElement(By.Id("submit"));
            loginButton.Click();
        }
    }

    #endregion
}