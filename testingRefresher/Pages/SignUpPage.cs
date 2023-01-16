using OpenQA.Selenium;

namespace automationPractice.Pages
{
    internal class SignUpPage : BasePage
    {
        public SignUpPage(IWebDriver driver) : base(driver)
        {
        }

        IWebElement inputFirstName => FindElement(By.Id("firstname"));
        IWebElement inputLastName => FindElement(By.Id("lastname"));
        IWebElement checkBoxNewsletter => FindElement(By.XPath("//div/input[following-sibling::label]"));
        IWebElement inputEmail => FindElement(By.Id("email_address"));
        IWebElement inputPassword => FindElement(By.Id("password"));
        IWebElement inputConfirmPassword => FindElement(By.Id("password-confirmation"));
        IWebElement btnCreateAccount => FindElement(By.XPath("//button[@title='Create an Account']"));
        IWebElement errorInvalidEmail => FindElement(By.Id("email_address-error"));
        IWebElement errorFirstName => FindElement(By.Id("firstname-error"));
        IWebElement errorLastName => FindElement(By.Id("lastname-error"));
        IWebElement errorPassword => FindElement(By.Id("password-error"));
        IWebElement errorConfirmPassword => FindElement(By.Id("password-confirmation-error"));

        private readonly Dictionary<string, string> expectedErrorMessages = new()
        {
            {$"{SignUpPageErrorMsgKeys.missingFirstName}", "This is a required field."},
            {$"{SignUpPageErrorMsgKeys.missingLastName}", "This is a required field." },
            {$"{SignUpPageErrorMsgKeys.invalidEmail}", "Please enter a valid email address (Ex: johndoe@domain.com)." },
            {$"{SignUpPageErrorMsgKeys.missingEmail}", "This is a required field." },
            {$"{SignUpPageErrorMsgKeys.missingPassword}", "This is a required field." },
            {$"{SignUpPageErrorMsgKeys.shortPassword}", "Minimum length of this field must be equal or greater than 8 symbols. Leading and trailing spaces will be ignored." },
            {$"{SignUpPageErrorMsgKeys.weakPassword}",  "Minimum of different classes of characters in password is 3. Classes of characters: Lower Case, Upper Case, Digits, Special Characters."},
            {$"{SignUpPageErrorMsgKeys.passwordNotMatching}", "Please enter the same value again." },
            {$"{SignUpPageErrorMsgKeys.missingConfirmPassword}", "This is a required field." }
        };

        public void EnterFirstName(string firstName) => inputFirstName.SendKeys(firstName);

        public void EnterLastName(string lastName) => inputLastName.SendKeys(lastName);
        
        public void CheckNewsletterOption() => checkBoxNewsletter.Click();
        
        public void EnterEmail(string email) => inputEmail.SendKeys(email);

        public void EnterPassword(string password) => inputPassword.SendKeys(password);

        public void EnterConfirmPassword(string password) => inputConfirmPassword.SendKeys(password);

        public void CreateAccount() => btnCreateAccount.Click();

        public string GetEmailErrorMsg() => errorInvalidEmail.Text;
        public string GetFirstNameErrorMsg() => errorFirstName.Text;
        public string GetLastNameErrorMsg() => errorLastName.Text;
        public string GetPasswordErrorMsg() => errorPassword.Text;
        public string GetConfirmPasswordErrorMsg() => errorConfirmPassword.Text;

        public Dictionary<string, string> GetExpectedErrorMessages() => expectedErrorMessages;
    }
}
