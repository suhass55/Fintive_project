using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections.Generic;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using SeleniumAutomation.Utilities;
using OpenQA.Selenium.Remote;

namespace SeleniumAutomation.Base
{
    class FirefoxBrowser
    {
        FirefoxProfile firefoxProfile = new FirefoxProfile();
        IWebDriver driver;
        AutomationUtilities _autoUtils = new AutomationUtilities();
        FirefoxOptions options = new FirefoxOptions();

        /// <summary>
        /// Method to setUp The Firefox Driver
        /// </summary>
        /// <params>None</params>
        /// <return>WebDriver instance</returns>

        public IWebDriver GetFirefoxDriver()
        {
            // get profile path from config.txt if any
            if (IsProfilePresent())
            {
                driver = FirefoxDriver(ProfileLocation);
            }
            // Else set the profilepath using preferences which are set below along with firefox binary
            else
            {
                driver = FirefoxDriver();
            }
            driver.Manage().Window.Maximize();
            return driver;

        }

        // Sets the profile using local profile path
        public IWebDriver FirefoxDriver(String profilePath)
        {
            var driverService = FirefoxDriverService.CreateDefaultService(DriverLocation, "geckodriver.exe");

            firefoxProfile = new FirefoxProfile(profilePath) { EnableNativeEvents = false };
            var options = new FirefoxOptions
            {
                Profile = firefoxProfile
            };
            FirefoxDriver driver = new FirefoxDriver(driverService, options, TimeSpan.FromMinutes(3));

            return driver;
        }


        // Sets the FF profile using preferences
        public IWebDriver FirefoxDriver(FirefoxProfile profilePath)
        {
            var driverService = FirefoxDriverService.CreateDefaultService(DriverLocation, "geckodriver.exe");
            options.Profile = CustomFirefoxProfile;

            FirefoxDriver driver = new FirefoxDriver(driverService, options, TimeSpan.FromMinutes(3));

            return driver;
        }

        /// <summary>
        /// Method for getting the firefox Driver
        /// </summary>
        /// <params>None</params>
        /// <return>WebDriver instance</returns>

        public IWebDriver FirefoxDriver()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(DriverLocation, "geckodriver.exe");
            service.HideCommandPromptWindow = true;
            service.SuppressInitialDiagnosticInformation = true;

            // Setting ff profile
            options.Profile = CustomFirefoxProfile;

            if (IsBinaryPathPresent())
                service.FirefoxBinaryPath = BinaryLocation;

            FirefoxDriver driver = new FirefoxDriver(service, options, TimeSpan.FromMinutes(3));

            return driver;
        }

        //public IWebDriver FirefoxDriver()
        //{
        //    if (IsBinaryPathPresent())
        //    {
        //        FirefoxBinary ffBinary = new FirefoxBinary(BinaryLocation);
        //        driver = new FirefoxDriver(ffBinary, FirefoxProfile);
        //    }
        //    else
        //        driver = new FirefoxDriver(firefoxProfile);

        //    return driver;
        //}

        /// <summary>
        /// Method for Getting the Firefox Driver when we supply the profile Path for the Same
        /// </summary>
        /// <params>ProfilePath as String</params>
        /// <return>WebDriver instance</returns>

        //public IWebDriver FirefoxDriver(String profilePath)
        //{
        //    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(DriverLocation, "geckodriver.exe");
        //    Environment.SetEnvironmentVariable("webdriver.gecko.driver", DriverLocation+"/geckodriver.exe");
        //    service.HideCommandPromptWindow = true;
        //    service.SuppressInitialDiagnosticInformation = true;

        //    if (IsBinaryPathPresent())
        //        service.FirefoxBinaryPath = BinaryLocation;

        //    FirefoxDriver driver = new FirefoxDriver(service, new FirefoxOptions(), TimeSpan.FromSeconds(60));

        //    //Initialize firefox browser with FF profile
        //    if (IsBinaryPathPresent())
        //    {
        //        FirefoxBinary ffBinary = new FirefoxBinary(BinaryLocation);
        //        driver = new FirefoxDriver(ffBinary, FirefoxProfile);
        //    }
        //    else
        //        driver = new FirefoxDriver(firefoxProfile);
        //    return driver;
        //}

        /// <summary>
        ///Firefox profile set up
        /// </summary>
        /// <return>firefoxProfile</returns>

        public FirefoxProfile CustomFirefoxProfile
        {
            get
            {
                firefoxProfile.AcceptUntrustedCertificates = true;
                firefoxProfile.SetPreference("browser.download.folderList", 2);
                firefoxProfile.SetPreference("browser.download.dir", DownloadLocation);
                firefoxProfile.SetPreference("browser.helperApps.neverAsk.openFile", "application/pdf, application/x-pdf, application/acrobat, applications/vnd.pdf, text/pdf, text/x-pdf, application/octet-stream, application/vnd.openxmlformats-officedocument.wordprocessingml.document, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/x-rar-compressed, application/zip,application/vnd.adobe.pdfxml,application/vnd.adobe.x-mars,application/vnd.fdf,application/vnd.adobe.xfdf,application/vnd.adobe.xdp+xml,application/vnd.adobe.xfd+xml");
                firefoxProfile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf, application/x-pdf, application/acrobat, applications/vnd.pdf, text/pdf, text/x-pdf, application/octet-stream, application/vnd.openxmlformats-officedocument.wordprocessingml.document, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/x-rar-compressed, application/zip,application/vnd.adobe.pdfxml,application/vnd.adobe.x-mars,application/vnd.fdf,application/vnd.adobe.xfdf,application/vnd.adobe.xdp+xml,application/vnd.adobe.xfd+xml");
                firefoxProfile.SetPreference("browser.helperApps.alwaysAsk.force", false);
                firefoxProfile.SetPreference("browser.download.manager.showAlertOnComplete", false);
                firefoxProfile.SetPreference("browser.download.panel.shown", false);
                firefoxProfile.SetPreference("browser.download.manager.closeWhenDone", true);
                firefoxProfile.SetPreference("browser.tabs.remote.force-enable", true);
                firefoxProfile.SetPreference("accessibility.force_disabled", 1);

                return firefoxProfile;
            }
        }


        /// To get the Download Location specified in the app.comfig file

        public string DownloadLocation
        {
            get
            {
                return _autoUtils.GetUserDesktopPath() + ConfigurationManager.AppSettings["BrowserDownloadLocation"];
            }
        }


        ///  To get the binary Location of the Firefox mentioned in the config.txt file
        ///  
        public string BinaryLocation
        {
            get
            {
                return ConfigurationManager.AppSettings["FirefoxBinaryPath"];
            }
        }


        ///  to get the Firefox profile location for firefox

        public string ProfileLocation
        {
            get
            {
                return ConfigurationManager.AppSettings["FirefoxProfilePath"];
            }
        }

        /// <summary>
        ///Method for checking whether the BinaryPath is present or not
        /// </summary>
        /// <params>none</params>
        /// <return>boolean i.e True/false</returns>

        public bool IsBinaryPathPresent()
        {
            return File.Exists(BinaryLocation);
        }

        /// <summary>
        /// Method for checking whether the profile is present or not 
        /// </summary>
        /// <params>none</params>
        /// <return>boolean i.e True/false</returns>

        public bool IsProfilePresent()
        {
            return File.Exists(ProfileLocation);
        }

        /// <summary>
        ///Method to retrieve Chrome Driver Path
        /// </summary>
        /// <params>None</params>
        /// <return>Returning the Driver Path as String</returns>

        public string DriverLocation
        {
            get
            {
                return _autoUtils.GetProjectLocation() + @"\Drivers";

            }
        }
    }
}
