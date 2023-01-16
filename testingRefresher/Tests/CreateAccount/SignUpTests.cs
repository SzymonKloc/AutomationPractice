using automationPractice.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automationPractice.Tests.CreateAccount
{
    internal class SignUpTests : BaseTest
    {
        SignUpPage signUpPage;

        private static readonly SignUpRepository Repository = GetRepository<SignUpRepository>("Tests//CreateAccount/SignUpData.json");

        [SetUp]
        public void Setup()
        {
            driver.Url += PagesUrls.SignUpPage;
            signUpPage = new SignUpPage(driver);
        }

        [Test, Category("SignUp")]
        public void ValidUser()
        {
            var firstName = string.Format(Repository.ValidUser.FirstName, UniqueNameForTest);
            var lastName = string.Format(Repository.ValidUser.LastName, UniqueNameForTest);
            var email = string.Format(Repository.ValidUser.Email, UniqueNameForTest);
            var password = string.Format(Repository.ValidUser.Password);

            signUpPage.EnterFirstName(firstName);
            signUpPage.EnterLastName(lastName);
            signUpPage.EnterEmail(email);
            signUpPage.EnterPassword(password);
            signUpPage.EnterConfirmPassword(password);
            signUpPage.CreateAccount();

            var expectedUrl = PagesUrls.HomePage + PagesUrls.MyAccountPage;
            Assert.That(driver.Url, Is.EqualTo(expectedUrl));
        }

        [Test, Category("SignUp")]
        public void EmptyFirstName()
        {
            var lastName = string.Format(Repository.ValidUser.LastName, UniqueNameForTest);
            var email = string.Format(Repository.ValidUser.Email, UniqueNameForTest);
            var password = string.Format(Repository.ValidUser.Password);

            signUpPage.EnterLastName(lastName);
            signUpPage.EnterEmail(email);
            signUpPage.EnterPassword(password);
            signUpPage.EnterConfirmPassword(password);
            signUpPage.CreateAccount();

            var messages = signUpPage.GetExpectedErrorMessages();
            var myAccountPageUrl = PagesUrls.HomePage + PagesUrls.MyAccountPage;
            Assert.Multiple(() =>
            {
                Assert.That(messages[$"{SignUpPageErrorMsgKeys.missingFirstName}"], Is.EqualTo(signUpPage.GetFirstNameErrorMsg()));
                Assert.That(driver.Url, Is.Not.EqualTo(myAccountPageUrl));
            });
        }

        [Test, Category("SignUp")]
        public void EmptyLastName()
        {
            var firstName = string.Format(Repository.ValidUser.FirstName, UniqueNameForTest);
            var password = string.Format(Repository.ValidUser.Password);

            signUpPage.EnterFirstName(firstName);
            signUpPage.EnterPassword(password);
            signUpPage.EnterConfirmPassword(password);
            signUpPage.CreateAccount();

            var messages = signUpPage.GetExpectedErrorMessages();
            var myAccountPageUrl = PagesUrls.HomePage + PagesUrls.MyAccountPage;
            Assert.Multiple(() =>
            {
                Assert.That(messages[$"{SignUpPageErrorMsgKeys.missingLastName}"], Is.EqualTo(signUpPage.GetLastNameErrorMsg()));
                Assert.That(driver.Url, Is.Not.EqualTo(myAccountPageUrl));
            });
        }

        [Test, Category("SignUp")]
        public void EmptyEmail()
        {
            var firstName = string.Format(Repository.ValidUser.FirstName, UniqueNameForTest);
            var lastName = string.Format(Repository.ValidUser.LastName, UniqueNameForTest);
            var password = string.Format(Repository.ValidUser.Password);

            signUpPage.EnterFirstName(firstName);
            signUpPage.EnterLastName(lastName);
            signUpPage.EnterPassword(password);
            signUpPage.EnterConfirmPassword(password);
            signUpPage.CreateAccount();

            var messages = signUpPage.GetExpectedErrorMessages();
            var myAccountPageUrl = PagesUrls.HomePage + PagesUrls.MyAccountPage;
            Assert.Multiple(() =>
            {
                Assert.That(messages[$"{SignUpPageErrorMsgKeys.missingEmail}"], Is.EqualTo(signUpPage.GetEmailErrorMsg()));
                Assert.That(driver.Url, Is.Not.EqualTo(myAccountPageUrl));
            });
        }

        private static string[] GetInvalidEmails() => Repository.InvalidEmails;

        [Test, Category("SignUp")]
        [TestCaseSource(nameof(GetInvalidEmails))]
        public void InvalidEmails(string email)
        {
            var firstName = string.Format(Repository.ValidUser.FirstName, UniqueNameForTest);
            var lastName = string.Format(Repository.ValidUser.LastName, UniqueNameForTest);
            email = string.Format(email, UniqueNameForTest);
            var password = string.Format(Repository.ValidUser.Password);


            signUpPage.EnterFirstName(firstName);
            signUpPage.EnterLastName(lastName);
            signUpPage.EnterEmail(email);
            signUpPage.EnterPassword(password);
            signUpPage.EnterConfirmPassword(password);
            signUpPage.CreateAccount();

            var messages = signUpPage.GetExpectedErrorMessages();
            var myAccountPageUrl = PagesUrls.HomePage + PagesUrls.MyAccountPage;

            Assert.Multiple(() =>
            {
                Assert.That(messages[$"{SignUpPageErrorMsgKeys.invalidEmail}"], Is.EqualTo(signUpPage.GetEmailErrorMsg()));
                Assert.That(driver.Url, Is.Not.EqualTo(myAccountPageUrl));
            });
        }

        [Test, Category("SignUp")]
        public void EmptyPassword()
        {
            var firstName = string.Format(Repository.ValidUser.FirstName, UniqueNameForTest);
            var lastName = string.Format(Repository.ValidUser.LastName, UniqueNameForTest);
            var email = string.Format(Repository.ValidUser.Email, UniqueNameForTest);

            signUpPage.EnterFirstName(firstName);
            signUpPage.EnterLastName(lastName);
            signUpPage.EnterEmail(email);
            signUpPage.CreateAccount();

            var messages = signUpPage.GetExpectedErrorMessages();
            var myAccountPageUrl = PagesUrls.HomePage + PagesUrls.MyAccountPage;

            Assert.Multiple(() =>
            {
                Assert.That(messages[$"{SignUpPageErrorMsgKeys.missingPassword}"], Is.EqualTo(signUpPage.GetPasswordErrorMsg()));
                Assert.That(driver.Url, Is.Not.EqualTo(myAccountPageUrl));
            });
        }

        private static string[] GetInvalidPasswords() => Repository.InvalidPasswords;

        [Test, Category("SignUp")]
        [TestCaseSource(nameof(GetInvalidPasswords))]
        public void InvalidPasswords(string password)
        {
            var firstName = string.Format(Repository.ValidUser.FirstName, UniqueNameForTest);
            var lastName = string.Format(Repository.ValidUser.LastName, UniqueNameForTest);
            var email = string.Format(Repository.ValidUser.Email, UniqueNameForTest);

            signUpPage.EnterFirstName(firstName);
            signUpPage.EnterLastName(lastName);
            signUpPage.EnterEmail(email);
            signUpPage.EnterPassword(password);
            signUpPage.EnterConfirmPassword(password);
            signUpPage.CreateAccount();

            var messages = signUpPage.GetExpectedErrorMessages();
            var myAccountPageUrl = PagesUrls.HomePage + PagesUrls.MyAccountPage;
            var error = signUpPage.GetPasswordErrorMsg();

            Assert.That(driver.Url, Is.Not.EqualTo(myAccountPageUrl));

            if (password.Length < 8 || password.Contains(' '))
            {
                Assert.That(messages[$"{SignUpPageErrorMsgKeys.shortPassword}"], Is.EqualTo(error));
            }
            else
            {
                Assert.That(messages[$"{SignUpPageErrorMsgKeys.weakPassword}"], Is.EqualTo(error));
            }
        }

        [Test, Category("SignUp")]
        public void EmptyConfirmPassword()
        {
            var firstName = string.Format(Repository.ValidUser.FirstName, UniqueNameForTest);
            var lastName = string.Format(Repository.ValidUser.LastName, UniqueNameForTest);
            var email = string.Format(Repository.ValidUser.Email, UniqueNameForTest);
            var password = string.Format(Repository.ValidUser.Password);

            signUpPage.EnterFirstName(firstName);
            signUpPage.EnterLastName(lastName);
            signUpPage.EnterEmail(email);
            signUpPage.EnterPassword(password);
            signUpPage.CreateAccount();

            var messages = signUpPage.GetExpectedErrorMessages();
            var myAccountPageUrl = PagesUrls.HomePage + PagesUrls.MyAccountPage;
            
            Assert.Multiple(() =>
            {
                Assert.That(driver.Url, Is.Not.EqualTo(myAccountPageUrl));
                Assert.That(messages[$"{SignUpPageErrorMsgKeys.missingConfirmPassword}"], Is.EqualTo(signUpPage.GetConfirmPasswordErrorMsg()));
            });
        }

        [Test, Category("SignUp")]
        public void InvalidConfirmPassword()
        {
            var firstName = string.Format(Repository.ValidUser.FirstName, UniqueNameForTest);
            var lastName = string.Format(Repository.ValidUser.LastName, UniqueNameForTest);
            var email = string.Format(Repository.ValidUser.Email, UniqueNameForTest);
            var password = string.Format(Repository.ValidUser.Password);
            var invalidConfirmPassword = password + " ";

            signUpPage.EnterFirstName(firstName);
            signUpPage.EnterLastName(lastName);
            signUpPage.EnterEmail(email);
            signUpPage.EnterPassword(password);
            signUpPage.EnterConfirmPassword(invalidConfirmPassword);
            signUpPage.CreateAccount();

            var messages = signUpPage.GetExpectedErrorMessages();
            var myAccountPageUrl = PagesUrls.HomePage + PagesUrls.MyAccountPage;

            Assert.Multiple(() =>
            {
                Assert.That(driver.Url, Is.Not.EqualTo(myAccountPageUrl));
                Assert.That(messages[$"{SignUpPageErrorMsgKeys.passwordNotMatching}"], Is.EqualTo(signUpPage.GetConfirmPasswordErrorMsg()));
            });
        }

    }
}
