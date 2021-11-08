using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using log4net;
using SeleniumAutomation.Utilities;
using SeleniumAutomation.Base;
using SpecFlow.Generic;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Gherkin.Model;
using NUnit.Framework;
using Finitive.StepsDefinitions;
using OneAtmosphere.Utilities.Generic;
using BoDi;
using System.Collections.Concurrent;

namespace SpecFlowSharp
{
    [Binding]
    public class TestBase : BaseClass
    {
        public ILog log = LogManager.GetLogger("TestBase");

        public static string FeatureName;
        public static string scenarioName;
        public static string tags;
        public static DateTime StartTime;
        public static DateTime EndTime;
        public static string ScenarioExecDuration;
        public static string ScenarioExecutionStatus;
        public static string ErrorMessage;


        public static int pass_counter = 0;
        public static int totalcounter = 0, failed_Counter = 0;
        public static string screenshotspath;

        private IObjectContainer _objectContainer;
        //public IWebDriver Driver;

        //Global Variable for Extend report
        private static ExtentTest featureNameCopy;
        private static ExtentTest featureName;
        [ThreadStatic]
        private static ExtentTest scenario;
        private static ExtentReports extent;
        private readonly FeatureContext featureContext;
        private readonly ScenarioContext scenarioContext;
        public static string ExecutionDate = DateTime.Now.ToString("dd MMMM yyyy").Replace(" ", "-");
        //static string jsonFileName;

        public static ConcurrentDictionary<string, ExtentTest> FeatureDictionary = new ConcurrentDictionary<string, ExtentTest>();
        public static ConcurrentDictionary<string, ExtentTest> FeatureDictionaryCopy = new ConcurrentDictionary<string, ExtentTest>();

        public static string ExtentReportFolderLocation = new AutomationUtilities().GetExtentReportsFolder();
        public static string ExtentReportCopyFolderLocation = new AutomationUtilities().GetExtentReportsCopyFolder();
        public static string log_path = @"file:///" + new AutomationUtilities().GetAutomationReportsFolder() + @"\ExtentReports\Log4NetLoggerDetails.log";

        public TestBase(IObjectContainer objectContainer, ScenarioContext _scenarioContext, FeatureContext _featureContext)
        {
            _objectContainer = objectContainer;
            //if (scenarioContext == null) throw new ArgumentNullException("scenarioContext");
            this.scenarioContext = _scenarioContext;
            //if (featureContext == null) throw new ArgumentNullException("featureContext");
            this.featureContext = _featureContext;
        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            DashboardUtility dashboardUtility = new DashboardUtility();
            dashboardUtility.CleanDashboardTestResult();
            dashboardUtility.CreateDashboardTestResultFolder();

            //Initialize Extent report before test starts
            var htmlReporter = new ExtentHtmlReporter(ExtentReportFolderLocation);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

            //htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            //Attach report to reporter
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            extent.AddSystemInfo("OS", "Windows");
            extent.AddSystemInfo("Host Name", "CI");
            extent.AddSystemInfo("Environment", "Finitive QA");
            extent.AddSystemInfo("User Name", "Finitive User");

            htmlReporter.LoadConfig(new AutomationUtilities().GetProjectLocation() + @"\extent-config.xml");
        }

        [BeforeFeature]
        public static void initializeDriver(FeatureContext featureContext)
        {
            FeatureName = featureContext.FeatureInfo.Title.ToString();
            //new Reporter().CreateHtmlHeader(FeatureName);
            screenshotspath = new Reporter().CreateScreenShotsFolder();
            totalcounter = 0; failed_Counter = 0;
            pass_counter = 0;
            new BaseClass().log4N.Info("InBeforeFeatureFeatureName:  " + featureContext.FeatureInfo.Title);
            //Create dynamic feature name
            featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
            FeatureDictionary.TryAdd(featureContext.FeatureInfo.Title, featureName);
            FeatureDictionaryCopy.TryAdd(featureContext.FeatureInfo.Title, featureName);
        }

        [AfterStep]
        public void InsertReportingSteps(IWebDriver driver)
        {

            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            object TestResult = scenarioContext.ScenarioExecutionStatus;
            log4N.Info("TestResult : " + TestResult);

            if (TestResult.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "When")
                    scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "And")
                    scenario.CreateNode<And>(scenarioContext.StepContext.StepInfo.Text).Skip("Step Definition Pending");
            }
            else if (scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(scenarioContext.StepContext.StepInfo.Text);
            }
            else if (scenarioContext.TestError != null)
            {
                var MediaEntity = new AutomationUtilities().CreateScreenshotforExtentReport(Driver);

                if (stepType == "Given")
                    scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Fail(new DataValidationSteps(Driver).CompName + scenarioContext.TestError.InnerException, MediaEntity);
                else if (stepType == "When")
                    scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Fail(new DataValidationSteps(Driver).CompName + scenarioContext.TestError.InnerException, MediaEntity);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Fail(new DataValidationSteps(Driver).CompName + scenarioContext.TestError.Message, MediaEntity);
                else if (stepType == "And")
                    scenario.CreateNode<And>(scenarioContext.StepContext.StepInfo.Text).Fail(new DataValidationSteps(Driver).CompName + scenarioContext.TestError.Message, MediaEntity);
            }
            extent.Flush();

            if (scenarioContext.TestError != null && (stepType == "Given" || stepType == "When" || stepType == "Then" || stepType == "And" || stepType == "But"))
            {
                screenshotspath = _autoutilities.getScreenshot(driver);
                log.Info(stepType + "-->" + scenarioContext.StepContext.StepInfo.Text + " Screenshot path" + screenshotspath);
            }
        }

        [BeforeScenario]
        public void SetUpTest(FeatureContext _featureContext)
        {
            //Registering the driver instance
            Driver = GetDriver(_featureContext.FeatureInfo.Title);
            _objectContainer.RegisterInstanceAs<IWebDriver>(Driver);

            totalcounter = totalcounter + 1;
            //balloon.showBaloonPopUp(scenarioContext.ScenarioInfo.Title);
            //balloon.disposeIcon();

            string InBSName = _featureContext.FeatureInfo.Title;
            log4N.Info("InBeforeScenarioFeatureName:  " + InBSName);

            //Create dynamic scenario name
            //featureName = extent.CreateTest<Feature>(_featureContext.FeatureInfo.Title);

            if (FeatureDictionary.ContainsKey(InBSName))
            {               
                scenario = FeatureDictionary[InBSName].CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            }
            //scenario = featureName.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            string currentscenario = scenarioContext.ScenarioInfo.Title.ToString();
            log4N.Info("Starting Execution of Scenario:=>" + currentscenario + " in Feature:" + featureContext.FeatureInfo.Title.ToString());
            pass_counter = 0;

        }


        [AfterScenario]
        public void TearDown()
        {
            //IWebDriver Driver = (IWebDriver)scenarioContext["driver"];
            try
            {
                if (scenarioContext.TestError != null)
                {
                    string UrlFile = _autoutilities.getScreenshot(Driver);
                    TestContext.AddTestAttachment(UrlFile, "ScreenShot");

                }

            }
            catch (Exception e)
            {
                log4N.Info(e.Message);
            }
            //string scenario = scenarioContext.ScenarioInfo.Title.ToString().Substring(0, 15);
            //On Test Fail ..
            if (scenarioContext.TestError != null)
            {
                failed_Counter = failed_Counter + 1;

                var error = scenarioContext.TestError;
                log4N.Info("An error ocurred:" + error.Message);
                log4N.Info("It was of type:" + error.GetType().Name);
                log4N.Info("An error ocurred:" + error.Message);
                log4N.Info("It was of type:" + error.GetType().Name);
            }
            else
            {
                //Pass cases ..
                pass_counter = pass_counter + 1;
            }
           Driver.Quit();            

        }

        [AfterFeature]
        public static void TerminateDriver(FeatureContext _featureContext)
        {
            //new Reporter().report_tests_count(totalcounter, failed_Counter, log_path);
            //new Reporter().CloseFileStream();

            string InBSName = _featureContext.FeatureInfo.Title;
            if (FeatureDictionaryCopy.ContainsKey(InBSName))
            {
                FeatureDictionaryCopy.TryRemove(InBSName, out featureNameCopy);
            }

            if (FeatureDictionaryCopy.IsEmpty)
            {
                long RandomValueWithTime = DateTime.Now.Ticks;
                scenario.CreateNode<And>("<button style='background-color:#0b6690;width:100%;font-family: Candara;padding:5px;' ><a href=" + @"./Log4NetLoggerDetails_" + RandomValueWithTime + ".log" + " ><font color='white'>Log Output</font></a></button>");
                extent.Flush();
                new AutomationUtilities().CopyFilesFromtoTo(ExtentReportFolderLocation, ExtentReportCopyFolderLocation, RandomValueWithTime);

            }

        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            if (FeatureDictionaryCopy.IsEmpty)
            {
                long RandomValueWithTime = DateTime.Now.Ticks;
                scenario.CreateNode<And>("<button style='background-color:#0b6690;width:100%;font-family: Candara;padding:5px;' ><a href=" + @"./Log4NetLoggerDetails_" + RandomValueWithTime + ".log" + " ><font color='white'>Log Output</font></a></button>");
                extent.Flush();
                new AutomationUtilities().CopyFilesFromtoTo(ExtentReportFolderLocation, ExtentReportCopyFolderLocation, RandomValueWithTime);

            }
        }
    }
}


