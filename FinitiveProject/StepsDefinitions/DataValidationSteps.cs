using log4net;
using NUnit.Framework;
using Finitive.Pages.PageParts;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using SeleniumAutomation.Base;
using SeleniumAutomation.Selenium;
using SeleniumAutomation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Finitive.Pages.PageConstants;

namespace Finitive.StepsDefinitions
{
    [Binding]
    public class DataValidationSteps : BaseClass
    {
        public DataValidationPage _DataValidationPage;
        public string Filename;
        public string CompanyName1;
        public string ImportError;
        public IWebDriver driver;
        public ILog log4Net;
        public string FinitiveTestSetup;
        public string CompanyDateToUpdate;
        public string CompanyDateReceived;


        public Dictionary<string, string> _QTDDictionary = new Dictionary<string, string>();
        public Dictionary<string, string> _YTDDictionary = new Dictionary<string, string>();
        public string _QERQuarter;
        public List<string> _Covid19LinksTaxData = new List<string>();
        public DataValidationSteps(IWebDriver _driver)
        {
            driver = _driver;
            log4Net = LogManager.GetLogger("DataValidationSteps");
            _autoutilities = new AutomationUtilities();
            _DataValidationPage = new DataValidationPage(driver);
            FinitiveTestSetup = TestContext.Parameters["Setup"];
        }
        public string TaxCode { get; set; }
        public string ReportCode { get; set; }
        public string EmployeeSSN { get; set; }
        public string MessageType { get; set; }
        public string Message { get; set; }
        public string DataSetup { get; private set; }

        public string Resources_Path;
        public string CompName;
        public string CompName2;
        public string CompName3;
        public string CompName1;
        public string Url;
        public string portlet;
        public int iCategory, iCode;
        public static string cname = null;
        public static string ccode = null;
        public static string EIN = null;
        public static string cname2 = null;
        public static string cname3 = null;
        public static string Portlet = null;
        public string FileNameToImport = "XMLWithCurrentQuarter";


        [Given(@"Navigate to Finitive url And Verify")]
        public void ThenNavigateToUrl()
        {
       
            if (FinitiveTestSetup == "Yes")
            {
                string ActualResult;
                string ExpectedResult = "Finitive(UAT)";
                Url = TestContext.Parameters["FinitiveURL"];
                driver.Navigate().GoToUrl(Url);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.Manage().Window.Maximize();
                ActualResult = driver.Title;
                if (ActualResult.Contains(ExpectedResult))
                {
                    Console.WriteLine("Test passed");
                }
                else
                {

                    Console.WriteLine("Test failed");
                }
                
            }
        }

        [Then(@"Click On Signup Button")]
        public void ThenClickOnSignupButton()
        {
            _DataValidationPage.ClickOnSignupButton();
        }

        [Then(@"Enter Signup Details")]
        public void ThenEnterSignupDetails()
        {
            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            _DataValidationPage.ThenEnterSignupDetails("Test", "ZenQ", "ZenQ", "Test Engineer", "+91", "9630405561", "WWW.Test.com", "India", "Online Search", "testzenq98+"+ milliseconds + "@gmail.com", "Second@123", "Invest", "Yes", "Asset Manager");

        }

        [Then(@"Click On Signup")]
        public void ThenClickOnSignup()
        {
            _DataValidationPage.ClickOnSignup();

        }


        [Then(@"Click On Verify Button From Registered Email")]
        public void ThenClickOnVerifyButtonFromRegisteredEmail()
        {

            _DataValidationPage.ReadAndGetTheTextFromEmail("testzenq98@gmail.com", "Finitive: New User Verification", 1);

        }

    }

}
