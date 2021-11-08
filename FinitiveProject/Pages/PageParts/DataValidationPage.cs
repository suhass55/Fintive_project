using log4net;
using OpenQA.Selenium;
using Finitive.Pages.PageConstants;
using SeleniumAutomation.Selenium;
using System;
using NUnit.Framework;
using System.Collections.Generic;
using Finitive.Pages.PageConstants;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumAutomation.XMLGeneration;
using System.Linq;
using System.Collections;
using SeleniumAutomation.Base;
using SeleniumAutomation.DataProvider;
using OneAtmosphere.Utilities.Generic;
using System.IO;
using System.Diagnostics;
using java.awt;
using System.Windows.Forms;
using OpenQA.Selenium.Support.Extensions;
using SeleniumAutomation.Utilities;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Finitive.Pages.PageParts
{

    public class DataValidationPage : UA
    {
        IWebDriver _localDriver;
        ILog log4Net;
        Actions action;


        /// <summary>
        /// Parameterized Constructor of the class
        /// </summary>
        /// <params>WebDriver instance </params>

        public DataValidationPage(IWebDriver Driver)
            : base(Driver)
        {
            this._localDriver = Driver;
            log4Net = LogManager.GetLogger("DataValidationPage");
        }

        // Enter username and password for TaxEx application...
        public void EnterUserNameAndPasswordForTaxEx(string Username, string Password)
        {
            WaitUntilElementIsDisplayed(DataValidationPageLocators.UserNameForTaxEx, 10);
            log4Net.Info("Username is " + Username);
            SafeType(DataValidationPageLocators.UserNameForTaxEx, Username, false, 10);
            WaitUntilElementIsDisplayed(DataValidationPageLocators.PasswordForTaxEx, 10);
            log4Net.Info("Password is " + Password);
            SafeType(DataValidationPageLocators.PasswordForTaxEx, Password, false, 10);
        }
        // Enter username and password for Dev Admin site...
        public void EnterUserNameAndPasswordForDevAdmin(string Username, string Password)
        {
            WaitUntilElementIsDisplayed(DataValidationPageLocators.UserNameForTaxEx, 10);
            log4Net.Info("Username is " + Username);
            SafeType(DataValidationPageLocators.UserNameForTaxEx, Username, false, 10);
            WaitUntilElementIsDisplayed(DataValidationPageLocators.PasswordForTaxEx, 10);
            log4Net.Info("Password is " + Password);
            SafeType(DataValidationPageLocators.PasswordForTaxEx, Password, false, 10);
        }
        

        public void ClickOnSignupButton()
        {
            WaitUntilElementIsDisplayed(DataValidationPageLocators.SignupButton, 20);
            SafeNormalClick(DataValidationPageLocators.SignupButton, 10);
            WaitUntilElementIsExist(DataValidationPageLocators.SignupPage);
            log4Net.Info("Navigated to Signup Page");
        }

        public void ThenEnterSignupDetails(string firstname, string lastname, string companyname, string position, string countrycode, string Telenum, string websiteUrl, string country, string marketingsource, string email, string pwd, string platform, string investor_question, string describe_company)
        {
            WaitUntilElementIsDisplayed(DataValidationPageLocators.FirstNameTextField, 20);
            SafeSendKeys(DataValidationPageLocators.FirstNameTextField, firstname, 20);
            WaitUntilElementIsDisplayed(DataValidationPageLocators.LastNameTextField, 20);
            SafeSendKeys(DataValidationPageLocators.LastNameTextField, lastname, 20);
            WaitUntilElementIsDisplayed(DataValidationPageLocators.CompanyNameTextField, 20);
            SafeSendKeys(DataValidationPageLocators.CompanyNameTextField, companyname, 20);
            WaitUntilElementIsDisplayed(DataValidationPageLocators.PositionTextField, 20);
            SafeSendKeys(DataValidationPageLocators.PositionTextField, position, 20);
            SafeActionClick(DataValidationPageLocators.CountryCodeDropdown,20);
            SafeClickFromListOfElements(DataValidationPageLocators.CountryCodeDropdownList, countrycode);
            SafeSendKeys(DataValidationPageLocators.TelephoneNumberTextfield, Telenum, 12);
            SafeSendKeys(DataValidationPageLocators.CompanyWebsiteUrl, websiteUrl, 20);
            SafeActionClick(DataValidationPageLocators.CountryDropdown, 20);
            SafeClickFromListOfElements(DataValidationPageLocators.CountryDropdownList, country);
            SafeActionClick(DataValidationPageLocators.MarketingSourceDropdown, 20);
            SafeClickFromListOfElements(DataValidationPageLocators.MarketingSourceDropdownList, marketingsource);
            ScrollIntoView(DataValidationPageLocators.FinitivePlateformList);

            SafeClickFromListOfElements(DataValidationPageLocators.FinitivePlateformList, platform);
            SafeClickFromListOfElements(DataValidationPageLocators.InstitutionalInvestorCheckbox, investor_question);
            SafeClickFromListOfElements(DataValidationPageLocators.DescribeYourCompanyCheckbox, describe_company);

            SafeSendKeys(DataValidationPageLocators.BusinessEmailTextField, email, 20);
            SafeSendKeys(DataValidationPageLocators.PasswordTextField, pwd, 20);
            SafeActionClick(DataValidationPageLocators.TermsprivacyCheckbox);
            waitForTime(5);

        }

        public void ClickOnSignup()
        {
            ScrollIntoView(DataValidationPageLocators.ButtonSignup);
            WaitUntilElementIsDisplayed(DataValidationPageLocators.ButtonSignup, 20);
            SafeActionClick(DataValidationPageLocators.ButtonSignup);
            waitForTime(5);
            log4Net.Info("Navigated to Email Verification pending page");
        }

        public string ReadAndGetTheTextFromEmail(string emailID, string emailsubject, int countofEmail)
        {
            string emailpart = emailID.Replace("@gmail.com", "");
            GmailUtility gmailUtility = new GmailUtility(emailID, "UNREAD", "\\Resources\\GmailAuth\\" + emailpart + "credentials.json", "\\Resources\\GmailAuth\\" + emailpart + "token.json");
            waitForTime(TimeOuts.SHORTWAIT);
            string emailData = gmailUtility.getTheDataFromTheEmail(emailsubject, countofEmail);
            //Console.WriteLine(emailData);
            string ResetPasswordURL = this.FindTextBetween(emailData, "href=", "target=");
            string NewURL = ResetPasswordURL.Replace("amp;", "");
            Console.WriteLine(NewURL);
            Process.Start(NewURL);
            return ResetPasswordURL;

        }

        public string FindTextBetween(string text, string left, string right)
        {
            // TODO: Validate input arguments
            int beginIndex = text.IndexOf(left); // find occurence of left delimiter
            if (beginIndex == -1)
                return string.Empty; // or throw exception?
            beginIndex += left.Length;
            int endIndex = text.IndexOf(right, beginIndex); // find occurence of right delimiter
            if (endIndex == -1)
                return string.Empty; // or throw exception?


            return text.Substring(beginIndex, endIndex - beginIndex).Trim();
        }


          
        
    }
}


