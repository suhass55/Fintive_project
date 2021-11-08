using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;

namespace SeleniumAutomation.Base
{
    class InternetExplorerBrowser
    {
        private log4net.ILog log = log4net.LogManager.GetLogger("InternetExplorerBrowser");
        private InternetExplorerOptions capabilities = new InternetExplorerOptions();
        public InternetExplorerOptions options = new InternetExplorerOptions();

        IWebDriver driver;
        InternetExplorerDriverService driverService;

        /// Set up Internet Explorer driver
        public IWebDriver InitIEDriver()
        {
            driverService = InternetExplorerDriverService.CreateDefaultService(IEDriverPath, "IEDriverServer.exe");
            driver = new InternetExplorerDriver(driverService, Options, TimeSpan.FromMinutes(3));
            return driver;
        }

        //public IWebDriver Driver
        //{
        //    get
        //    {
        //        log4Net.Info("Launching IE with default settings");
        //        return new InternetExplorerDriver(IEDriverPath, Options);
        //    }
        //}

        /**
        * Sets the required properties for IEdriver to initialize 
        * @return IECapabilities with required properties set
        */
        private InternetExplorerOptions Options
        {
            get
            {
                options.EnablePersistentHover = false;
                options.EnableNativeEvents = false;
                options.EnsureCleanSession = true;
                options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                return options;
            }
        }

        private void SetCapabilities()
        {
            capabilities.AddAdditionalCapability("ignoreProtectedModeSettings", true);
            capabilities.AddAdditionalCapability("enablePersistentHover", false);
            capabilities.AddAdditionalCapability("native_events", false);
        }

        private string IEDriverPath
        {
            get
            {
                return new SeleniumAutomation.Utilities.AutomationUtilities().GetProjectLocation() + @"\Drivers";
            }
        }
    }
}
