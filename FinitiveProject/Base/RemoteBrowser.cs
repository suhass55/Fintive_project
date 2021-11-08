using System;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Net;
using SeleniumAutomation.Utilities;
using MbUnit.Framework;

namespace SeleniumAutomation.Base
{
    public class RemoteBrowser
    {
        public IWebDriver Driver;
        public ILog log = LogManager.GetLogger("RemoteBrowser");
        AutomationUtilities _autoutilities;


        /// <summary>
        /// Method for initializing RemoteDriver and Invoke Remote driver based on browser type specified in config file ie  If the browser type is null it will take as ff and other than chrome,ff/firefox or null ,  it will provide an error message of invalid browser type
        /// </summary>
        /// <params>None</params>
        /// <return>WebDriver Reference</returns>

        [Obsolete]
        public IWebDriver InitialiseRemoteDriver(string sBrowserType)
        {
            _autoutilities = new AutomationUtilities();
            //  string sBrowserType = _autoutilities.GetKeyValue("BROWSER", "Browser").ToLower();
            string _ip = _autoutilities.GetKeyValue("REMOTE", "IP");
            switch (sBrowserType)
            {
                case "ie":
                case "iexplore":
                    SetRemoteDriver("internet explorer", _ip);
                    break;
                case "ff":
                case "firefox":
                    SetRemoteDriver("firefox", _ip);
                    break;
                case "chrome":
                    SetRemoteDriver("chrome", _ip);
                    break;
                case "safari":
                    SetRemoteDriver("safari", _ip);
                    break;
                case "":
                    SetRemoteDriver("firefox", _ip);
                    break;

                default:
                    Assert.Fail(" Browser name mentioned in config file is Invalid");
                    break;
            }
            return GetRemoteDriver();

        }


        /// <summary>
        ///Method for setting the Remote Driver for GRID as well as Sauce Labs
        /// </summary>
        /// <params>browsertype as String and IP as String</params>
        /// <return>none</returns>
        [Obsolete]
        public void SetRemoteDriver(string sBrowserType, string ip)
        {
            _autoutilities = new AutomationUtilities();
            string executionMode = _autoutilities.GetKeyValue("MODEOFEXECUTION", "ExecutionMode").ToLower();
            try
            {
                /// compare the execution mode and IP that are specified in config file is Grid then execute the test in Grid else execute in saucelabs
                if (executionMode.Equals("remote") && _autoutilities.GetKeyValue("REMOTE", "IP").Contains("4444"))
                {
                    DesiredCapabilities capabilities = new DesiredCapabilities();
                    capabilities.SetCapability(CapabilityType.BrowserName, sBrowserType);
                    capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
                    this.Driver = new RemoteWebDriver(new Uri("http://" + ip + "/wd/hub"), capabilities, TimeSpan.FromSeconds(300));
                    this.Driver.Manage().Cookies.DeleteAllCookies();

                }
                //Running on Browser Stack ....
                else if (executionMode.Equals("remote") && _autoutilities.GetKeyValue("REMOTE", "IP").Contains("browserstack"))
                {
                    DesiredCapabilities capability = new DesiredCapabilities();
                    capability.SetCapability("os", "OS X");
                    capability.SetCapability("os_version", "Mojave");
                    capability.SetCapability("browser", "Safari");
                    capability.SetCapability("browser_version", "12.0");
                    capability.SetCapability("resolution", "1920x1080");
                    capability.SetCapability("project", "Mac_Safari12");
                    capability.SetCapability("build", "Build_Test");
                    capability.SetCapability("browserstack.user", "silpavajja2");
                    capability.SetCapability("browserstack.key", "3BqtD3z8zqJn4ZEqyZy7");

                    this.Driver = new RemoteWebDriver(new Uri("http://" + ip + "/wd/hub"), capability, TimeSpan.FromSeconds(300));
                }
                // execute the test using saucelabs
                else if (executionMode.Equals("remote") && _autoutilities.GetKeyValue("REMOTE", "IP").Contains("saucelabs"))
                {
                    string SAUCE_LABS_ACCOUNT_NAME = _autoutilities.GetKeyValue("REMOTE", "SAUCE_LABS_ACCOUNT_NAME");
                    string SAUCE_LABS_ACCOUNT_KEY = _autoutilities.GetKeyValue("REMOTE", "SAUCE_LABS_ACCOUNT_KEY");
                    // string browserName = _autoutilities.GetKeyValue("BROWSER", "Browser").ToLower();
                    string version = "34";
                    string platform = "Windows 7";
                    DesiredCapabilities desiredCapabilites = new DesiredCapabilities(sBrowserType, version, Platform.CurrentPlatform); // set the desired browser
                    desiredCapabilites.SetCapability("platform", platform); // operating system to use
                    desiredCapabilites.SetCapability("username", SAUCE_LABS_ACCOUNT_NAME); // supply sauce labs username 
                    desiredCapabilites.SetCapability("accessKey", SAUCE_LABS_ACCOUNT_KEY);  // supply sauce labs account key
                                                                                            //      desiredCapabilites.SetCapability("name", TestContext.CurrentContext.Test.Name); // give the test a name
                    this.Driver = new RemoteWebDriver(new Uri("http://" + ip + "/wd/hub"), desiredCapabilites, TimeSpan.FromSeconds(300));
                    this.Driver.Manage().Cookies.DeleteAllCookies();
                }

                MaximizeBrowser();
            }
            catch (WebException e)
            {
                log.Error("Error while Initializing the Remote Webdriver " + e.Message);
                Assert.Fail("Error while Initializing the Remote Webdriver " + e.Message.Replace('{', '[').Replace('}', ']'));
            }
            catch (Exception e)
            {
                log.Error("Error while Initializing the Remote Webdriver " + e.Message);
                Assert.Fail("Error while Initializing the Remote Webdriver " + e.Message.Replace('{', '[').Replace('}', ']'));
            }

        }

        public IWebDriver GetRemoteDriver()
        {

            return this.Driver;
        }

        /// <summary>
        ///Method for maximizing the browser
        /// </summary>
        /// <params>none</params>
        /// <return>none</returns>

        public void MaximizeBrowser()
        {
            this.Driver.Manage().Window.Maximize();
        }


        /// <summary>
        ///Method for navigating to the provided URL
        /// </summary>
        /// <params>URL as string</params>
        /// <return>void</returns>

        public void Navigate(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }
    }
}

