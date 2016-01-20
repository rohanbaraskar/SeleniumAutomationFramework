using System.Collections.Generic;
using Insight.Web.BddTests.PageObjects.Selenium;
using OpenQA.Selenium;

namespace Insight.Web.BddTests.PageObjects.Pages.Command
{
    public class ChangePasswordPageCommand : BasePage
    {
        private readonly string originalPassword;
        private string confirmPassword;
        private string newPassword;

        #region Public Methods

        public ChangePasswordPageCommand(string password)
        {
            originalPassword = password;
        }

        public ChangePasswordPageCommand WithPassword(string password)
        {
            newPassword = password;
            return this;
        }

        public ChangePasswordPageCommand ConfirmPassword(string password)
        {
            confirmPassword = password;
            return this;
        }


        /// <summary>
        ///     Login into the application with a username and a password
        /// </summary>
        public void Change()
        {
            #region Textbox

            var dictionaryOfFields = new Dictionary<string, string>
            {
                {"OriginalPassword", originalPassword},
                {"NewPassword", confirmPassword},
                {"ConfirmNewPassword", newPassword}
            };


            EnterTextIntoField(dictionaryOfFields);

            #endregion

            #region Button

            // Logon
            var subitButton = Driver.Instance.FindElement(By.Id("submit"));
            subitButton.Click();

            #endregion
        }

        #endregion
    }
}