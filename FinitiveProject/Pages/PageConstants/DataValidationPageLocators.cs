using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Finitive.Pages.PageConstants
{
    class DataValidationPageLocators
    {
        // For QA TaxEx application...
        public static By UserNameForTaxEx = By.Id("input-email");
        public static By PasswordForTaxEx = By.Id("input-password");
        public static By SignInButton = By.XPath("//input[@type='submit']");
        public static By SignupButton = By.XPath("//a[@class='btn-link mainMenu__link mainMenu__link_button']");
        public static By SignupPage = By.Id("signUp");
        public static By FirstNameTextField = By.Id("FirstName");
        public static By LastNameTextField = By.Id("LastName");
        public static By CompanyNameTextField = By.Id("CompanyName");
        public static By PositionTextField = By.Id("Position");
        public static By CompanyWebsiteUrl = By.Id("BusinessUrl");
        public static By CountryCodeDropdown = By.XPath("//div[@aria-controls='iti-0__country-listbox']");
        public static By CountryCodeDropdownList = By.XPath("//ul[@class='iti__country-list iti__country-list--dropup']/li");
        public static By TelephoneNumberTextfield = By.XPath("//input[@id='PhoneNumber']");
        public static By CountryDropdown = By.Id("CountryId");
        public static By CountryDropdownList = By.XPath("//select[@id='CountryId']/option");
        public static By MarketingSourceDropdown = By.Id("MarketingSourceId");
        public static By MarketingSourceDropdownList = By.XPath("//select[@name='MarketingSourceId']/option");
        public static By FinitivePlateformList = By.XPath("//div[@class='finitiveForm__groups signUpForm__groups']/div/label");
        public static By BusinessEmailTextField = By.Id("Email");
        public static By PasswordTextField = By.Id("Password");
        public static By InstitutionalInvestorCheckbox = By.XPath("//div[@class='finitiveForm__groups signUpForm__institutionalInvestorOptions']/div/label");
        public static By DescribeYourCompanyCheckbox = By.XPath("//div[@class='finitiveForm__groups signUpForm__institutionalInvestorCompanyDescriptionOptions']/div/label");
        public static By ButtonSignup = By.XPath("//button[@type='submit']");
        public static By TermsprivacyCheckbox = By.Id("HasAgreedToTerms");
        public static By SignUpPageText = By.Id("//h2[@class='finitiveForm__title signUpForm__title text-center']");
        public static By EmailVerificationPendingText = By.XPath("//h1[@class='finitiveForm__title confirmEmailForm__title']");


    }
}
