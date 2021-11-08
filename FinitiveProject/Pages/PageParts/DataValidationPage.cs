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
        public string sBaseURL;
        public string sTestCaseName;
        public static string NoOfRecords;
        public static int NoOfDPRRecordsInAtmos;
        public static string CompName;
        public static string CompanyName;
        public static string TaxExMessageType;
        public static string TaxExMessage;
        public string Url;
        public static int noofrecords1 = 0;
        public static int noofrecords2 = 0;
        public static int noofrecords3 = 0;
        public static int noofrecords4 = 0;
        public static int noofrecords5 = 0;
        public static int noofrecords6 = 0;
        public static int noofrecords7 = 0;
        public static int noofrecords8 = 0;
        public static int noofrecordsScheduledFilingFederal = 0;
        public static int noofrecordsHeldFilingFederal = 0;
        public static int noofrecordsCompletedFilingFederal = 0;
        public static int noofrecordsSchedulePaymentFederal = 0;
        public static int noofrecordsHeldPaymentFederal = 0;
        public static int noofrecordsCompletedPaymentFederal = 0;
        public static int noofrecordsScheduledPaymentFederal = 0;
        public static int noofrecordsScheduledFilingState = 0;
        public static int noofrecordsHeldFilingState = 0;
        public static int noofrecordsCompletedFilingState = 0;
        public static int noofrecordsScheduledPaymentState = 0;
        public static int noofrecordsHeldPaymentState = 0;
        public static int noofrecordsCompletedPaymentState = 0;
        public static int noofrecordsScheduledFilingLocal = 0;
        public static int noofrecordsHeldFilingLocal = 0;
        public static int noofrecordsCompletedFilingLocal = 0;
        public static int noofrecordsSchedulePaymentLocal = 0;
        public static int noofrecordsHeldPaymentLocal = 0;
        public static int noofrecordsCompletedPaymentLocal = 0;

        public static List<List<string>> CompleteFilingsAtmosData = new List<List<string>>();
        public static List<List<string>> CompleteFilingTaxExData = new List<List<string>>();
        public static List<List<string>> ScheduledFilingAtmosData = new List<List<string>>();
        public static List<List<string>> ScheduledFilingTaxExData = new List<List<string>>();
        public static List<List<string>> HeldFilingAtmosData = new List<List<string>>();
        public static List<List<string>> HeldFilingTaxExData = new List<List<string>>();
        public static List<List<string>> ScheduledPaymentsAtmosData = new List<List<string>>();
        public static List<List<string>> ScheduledPaymentsTaxExData = new List<List<string>>();
        public static List<List<string>> HeldPaymentAtmosData = new List<List<string>>();
        public static List<List<string>> HeldPaymentlstTaxExData = new List<List<string>>();
        public static List<List<string>> CompletePaymentsAtmosData = new List<List<string>>();
        public static List<List<string>> CompletePaymentsTaxExData = new List<List<string>>();
        public static List<List<string>> MissingTaxIDTaxExData = new List<List<string>>();
        public static List<List<string>> MissingTaxIdAtmosData = new List<List<string>>();
        //Federal Tab Records List
        public static List<List<string>> ScheduledFilingsFederalTaxEx = new List<List<string>>();
        public static List<List<string>> HeldFilingsFederalTaxEx = new List<List<string>>();
        public static List<List<string>> CompletedFilingsFederalTaxEx = new List<List<string>>();
        public static List<List<string>> ScheduledPaymentsFederalTaxEx = new List<List<string>>();
        public static List<List<string>> HeldPaymentsFederalTaxEx = new List<List<string>>();
        public static List<List<string>> CompletedPaymentsFederalTaxEx = new List<List<string>>();
        public static List<List<string>> ScheduledFilingsFederalAtmos = new List<List<string>>();
        public static List<List<string>> HeldFilingsFederalAtmos = new List<List<string>>();
        public static List<List<string>> CompletedFilingsFederalAtmos = new List<List<string>>();
        public static List<List<string>> ScheduledPaymentsFederalAtmos = new List<List<string>>();
        public static List<List<string>> HeldPaymentsFederalAtmos = new List<List<string>>();
        public static List<List<string>> CompletedPaymentsFederalAtmos = new List<List<string>>();

        //State Tab Records List
        public static List<List<string>> ScheduledFilingsStateTaxEx = new List<List<string>>();
        public static List<List<string>> HeldFilingsStateTaxEx = new List<List<string>>();
        public static List<List<string>> CompletedFilingsStateTaxEx = new List<List<string>>();
        public static List<List<string>> ScheduledPaymentsStateTaxEx = new List<List<string>>();
        public static List<List<string>> HeldPaymentsStateTaxEx = new List<List<string>>();
        public static List<List<string>> CompletedPaymentsStateTaxEx = new List<List<string>>();
        public static List<List<string>> ScheduledFilingsStateAtmos = new List<List<string>>();
        public static List<List<string>> HeldFilingsStateAtmos = new List<List<string>>();
        public static List<List<string>> CompletedFilingsStateAtmos = new List<List<string>>();
        public static List<List<string>> ScheduledPaymentsStateAtmos = new List<List<string>>();
        public static List<List<string>> HeldPaymentsStateAtmos = new List<List<string>>();
        public static List<List<string>> CompletedPaymentsStateAtmos = new List<List<string>>();

        //Local Tab Records List
        public static List<List<string>> ScheduledFilingsLocalTaxEx = new List<List<string>>();
        public static List<List<string>> HeldFilingsLocalTaxEx = new List<List<string>>();
        public static List<List<string>> CompletedFilingsLocalTaxEx = new List<List<string>>();
        public static List<List<string>> ScheduledPaymentsLocalTaxEx = new List<List<string>>();
        public static List<List<string>> HeldPaymentsLocalTaxEx = new List<List<string>>();
        public static List<List<string>> CompletedPaymentsLocalTaxEx = new List<List<string>>();
        public static List<List<string>> ScheduledFilingsLocalAtmos = new List<List<string>>();
        public static List<List<string>> HeldFilingsLocalAtmos = new List<List<string>>();
        public static List<List<string>> CompletedFilingsLocalAtmos = new List<List<string>>();
        public static List<List<string>> ScheduledPaymentsLocalAtmos = new List<List<string>>();
        public static List<List<string>> HeldPaymentsLocalAtmos = new List<List<string>>();
        public static List<List<string>> CompletedPaymentsLocalAtmos = new List<List<string>>();

        // All Filings for Federal , state & Local

        public static List<List<string>> AllFilingsAllTabsTaxEx = new List<List<string>>();
        public static List<List<string>> AllFilingsFederalTabsTaxEx = new List<List<string>>();
        public static List<List<string>> AllFilingsStateTabsTaxEx = new List<List<string>>();
        public static List<List<string>> AllFilingsLocalTabsTaxEx = new List<List<string>>();
        public static List<List<string>> AllPaymentsAllTabsTaxEx = new List<List<string>>();
        public static List<List<string>> AllPaymentsFederalTabsTaxEx = new List<List<string>>();
        public static List<List<string>> AllPaymentsStateTabsTaxEx = new List<List<string>>();
        public static List<List<string>> AllPaymentsLocalTabsTaxEx = new List<List<string>>();
        public static List<List<string>> AllFilingsAllTabsAtmos = new List<List<string>>();
        public static List<List<string>> AllFilingsFederalTabsAtmos = new List<List<string>>();
        public static List<List<string>> AllFilingsStateTabsAtmos = new List<List<string>>();
        public static List<List<string>> AllFilingsLocalTabsAtmos = new List<List<string>>();
        public static List<List<string>> AllPaymentsAllTabsAtmos = new List<List<string>>();
        public static List<List<string>> AllPaymentsFederalTabsAtmos = new List<List<string>>();
        public static List<List<string>> AllPaymentsStateTabsAtmos = new List<List<string>>();
        public static List<List<string>> AllPaymentsLocalTabsAtmos = new List<List<string>>();

        public static List<string> Covid19LinksTaxData = new List<string>();
        public static List<string> Covid19LinksAtmosData = new List<string>();
        public Dictionary<string, string> QTDDictionary = new Dictionary<string, string>();
        public Dictionary<string, string> YTDDictionary = new Dictionary<string, string>();
        public static List<string> PaymentSummaryAtmosData = new List<string>();
        public static List<string> TaxCodeDeferralAtmosData = new List<string>();


        public BaseClass _BaseClass = null;
        public ExcelManager _Excelmanager = null;

        public string Tax;
        public string YTax;
        public string QtdId;
        public string YTDId;
        public string QBCompany;
        public string YBCompany;
        public string QERQuarter;


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
            log4Net = LogManager.GetLogger("TaxExDataValidationPage");
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
        // enter Username and password for Atmosphere application...
        public void EnterUserNameAndPasswordForAtmosphere(string UName, string Pwd)
        {
            WaitUntilElementIsDisplayed(DataValidationPageLocators.UserNameForAtmos, 20);
            log4Net.Info("Username is " + UName);
            SafeSendKeys(DataValidationPageLocators.UserNameForAtmos, UName, 20);

            WaitUntilElementIsDisplayed(DataValidationPageLocators.PasswordForAtmos, 20);
            log4Net.Info("Password is " + Pwd);
            SafeSendKeys(DataValidationPageLocators.PasswordForAtmos, Pwd, 20);

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


