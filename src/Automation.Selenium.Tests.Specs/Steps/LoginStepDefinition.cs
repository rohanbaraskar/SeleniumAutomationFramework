using System;
using Insight.Web.BddTests.Common.Model;
using Insight.Web.BddTests.Common.Utils;
using Insight.Web.BddTests.PageObjects.Pages;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Insight.Web.BddTests.Specs.Steps
{
    [Binding]
    public class LoginStepDefinition
    {
        [Given(@"I login with valid credentials")]
        public void GivenILoginWithValidCredentials()
        {
            LoginPage.GoTo();

            LoginPage.LoginAsDefaultUser()
                .WithDefaultPassword()
                .Login();
        }

        [When(@"I log out and back in with the following credentials")]
        [When(@"I login with the following credentials")]
        [Given(@"I login with the following credentials")]
        public void WhenILoginWithTheFollowingCredentials(Credential credentials)
        {
            LoginPage.GoTo();

            LoginPage.LoginAs(credentials.Username)
                .WithPassword(credentials.Password)
                .Login();
        }

        [Then(@"I should see the error message ""(.*)"" below the ""(.*)"" field")]
        public void ThenIShouldSeeTheErrorMessage(string expectedValidationErrorMessage, Constants.LoginFields fieldName)
        {
            var actualValidationErrorMessage = LoginPage.ValidationFieldError;
            Assert.AreEqual(expectedValidationErrorMessage, actualValidationErrorMessage,
                "Validation message is not as expected");
        }

        [Then(@"I should see the following error messages")]
        public void ThenIShouldSeeTheFollowingErrorMessages(Table expectedMessageTable)
        {
            var actualValidationErrorMessages = LoginPage.ValidationFieldErrors;
            expectedMessageTable.CompareToSet(actualValidationErrorMessages);
        }

        [Then(@"the ""(.*)"" field should be higlighted in ""(.*)""")]
        public void ThenTheFieldShouldBeHiglightedIn(Constants.LoginFields fieldName, Constants.Colour colour)
        {
            var expectedRgbaColour = Utilities.GetRgbaColour(colour);
            Assert.NotNull(expectedRgbaColour);

            string actualColour;
            switch (fieldName)
            {
                case Constants.LoginFields.Email:
                {
                    actualColour = LoginPage.EmailValidationColor;
                }
                    break;
                case Constants.LoginFields.Password:
                {
                    actualColour = LoginPage.PasswordValidationColor;
                }
                    break;
                default:
                {
                    throw new AssertionException("No field name defined in test framework");
                }
            }
            Assert.NotNull(expectedRgbaColour);

            Assert.AreEqual(expectedRgbaColour, actualColour);
        }

        [Then(@"the following fields should be higlighted in ""(.*)""")]
        public void ThenTheFollowingFieldsShouldBeHiglightedIn(Constants.Colour colour, Table expectedFields)
        {
            var expectedRgbaColour = Utilities.GetRgbaColour(colour);

            var expectedFieldList = expectedFields.CreateDynamicSet();

            foreach (var fieldItem in expectedFieldList)
            {
                var convertedEnumField =
                    (Constants.LoginFields) Enum.Parse(typeof (Constants.LoginFields), fieldItem.Field, true);
                var actualColour = LoginPage.GetValidationFieldColour(convertedEnumField);
                Assert.AreEqual(expectedRgbaColour, actualColour);
            }
        }

        [Then(@"I should see the validation error message ""(.*)""")]
        public void ThenIShouldSeeTheValidationErrorMessage(string expectedErrorMessage)
        {
            var actualNotificationError = LoginPage.NotificationError;
            Assert.AreEqual(expectedErrorMessage, actualNotificationError);
        }

        [Then(@"I should receive the following incorrect password messages")]
        public void ThenIShouldReceiveTheFollowingValidationMessage(dynamic instance)
        {
            var expectedErrorMessage = (string) instance.OriginalPasswordValidation;
            var actualErrorMessage = ChangePasswordPage.MatchingPasswordNotificationError;
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "ERROR - Fields do not match");
        }

        [Then(@"I should receive the following validation messages")]
        public void ThenIShouldReceiveTheFollowingValidationMessages(dynamic instance)
        {
            Assert.AreEqual((string) instance.OriginalPasswordValidation,
                ChangePasswordPage.OriginalPasswordNotificationError);
            Assert.AreEqual((string) instance.NewPasswordValidation, ChangePasswordPage.NewPasswordNotificationError);
            Assert.AreEqual((string) instance.ConfirmPasswordValidation,
                ChangePasswordPage.ConfirmPasswordNotificationError);
        }

        [When(@"I should see the following succesful message")]
        public void WhenIShouldSeeTheFollowingSuccesfulMessage(dynamic instance)
        {
            var expectedErrorMessage = (string) instance.Message;
            var actualErrorMessage = ChangePasswordPage.MatchingPasswordSuccessNotificationError;
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "ERROR - Fields do not match");
        }

        [When(@"I login with the following credentials ""(.*)"" times")]
        public void WhenILoginWithTheFollowingCredentialsTimes(int numberOfTimes, Credential credentials)
        {
            for (var count = 0; count < numberOfTimes; count++)
            {
                LoginPage.GoTo();

                LoginPage.LoginAs(credentials.Username)
                    .WithPassword(credentials.Password)
                    .Login();
            }
        }

        [When(@"I submit the form to change my password using the following details")]
        public void WhenISubmitTheFormToChangeMyPasswordUsingTheFollowingDetails(Credential credentials)
        {
            ChangePasswordPage.GoTo();

            ChangePasswordPage.ChangeOriginalPassword(credentials.OriginalPassword)
                .WithPassword(credentials.Newpassword)
                .ConfirmPassword(credentials.ConfirmPassword)
                .Change();
        }

        [When(@"I submit the form to reset my password using the following details")]
        public void WhenISubmitTheFormToResetMyPasswordUsingTheFollowingDetails(UserDetails details)
        {
            ResetPasswordPage.GoTo();

            ResetPasswordPage.ResetPasswordFor(details.FirstName)
                .WithLastName(details.LastName)
                .WithEmail(details.Email)
                .WithUsername(details.UserName)
                .Submit();
        }

        [Then(@"I should receive the following validation message ""(.*)""")]
        public void ThenIShouldReceiveTheFollowingValidationMessage(string expectedErrorMessage)
        {
            var actualNotificationError = ResetPasswordPage.ValidationFieldError;
            Assert.AreEqual(expectedErrorMessage, actualNotificationError);
        }

        [Then(@"I should see the following notification error ""(.*)""")]
        public void ThenIShouldSeeTheFollowingCapchaNotificationError(string expectednotificationMessage)
        {
            var actualNotificationError = ResetPasswordPage.NotificationError;
            Assert.AreEqual(expectednotificationMessage, actualNotificationError);
        }

        [When(@"I submit the email address ""(.*)"" to change my password")]
        public void WhenISubmitTheEmailAddressToChangeMyPassword(string email)
        {
            ForgotPasswordPage.GoTo();

            ForgotPasswordPage.EnterEmail(email);
        }

        [Then(@"I should receive the following success ""(.*)"" message")]
        public void ThenIShouldReceiveTheFollowingSuccessMessage(string message)
        {
            Assert.AreEqual(ForgotPasswordPage.NotificationFieldSuccess, message);
        }

        [Given(@"I am logged in and on the Insight ""(.*)"" page")]
        public void GivenIAmLoggedInAndOnTheInsightPage(Constants.Page pageType)
        {
            var hsd = new HttpStepDefinition();
            hsd.GivenTheInsightApplicationIsRunning();
            GivenILoginWithValidCredentials();
            var csd = new CommonStepDefinition();
            csd.ThenIShouldBeOnTheInsightPage(pageType);
        }
    }
}