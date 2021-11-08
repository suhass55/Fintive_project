using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using log4net;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumAutomation.Utilities;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SeleniumAutomation.Base
{
    class ChromeBrowser
    {
        private static ILog log4Net = LogManager.GetLogger("ChromeBrowser");
        private ChromeOptions options = new ChromeOptions();
        AutomationUtilities _autoUtils = new AutomationUtilities();
        
        public IWebDriver InitChromeDriver(string featureName)
        {
            log4Net.Info("Launching google chrome with specified profile - " + ProfileName);
            string path = AutomationUtilities.DownloadFilesPath(featureName);
            options.AddUserProfilePreference("download.default_directory", path);
            if (IsProfileDirPresent())
            {
                log4Net.Info("Running with specified chrome profile");
                options.AddArguments("user-data-dir=" + UserDataLocation);
                options.AddArguments("--profile-directory=" + ProfileName);
                log4Net.Info("Init chrome driver with custom profile is completed..");
            }
            else
            {
                log4Net.Info("Specified chrome profile does not exists in 'User Data' folder");
                log4Net.Info("Hence Chrome Browser is launched with a new profile..");
            }

            //new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            //return new ChromeDriver(Options);
            return new ChromeDriver(DriverLocation, Options, TimeSpan.FromMinutes(3));
        }

        private ChromeOptions Options
        {
            get
            {
                options.AddArgument("--disable-background-timer-throttling");
                options.AddArgument("--disable-backgrounding-occluded-windows");
                options.AddArgument("--disable-breakpad");
                options.AddArgument("--disable-component-extensions-with-background-pages");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--disable-extensions");
                options.AddArgument("--disable-features=TranslateUI,BlinkGenPropertyTrees");
                options.AddArgument("--disable-ipc-flooding-protection");
                options.AddArgument("--disable-renderer-backgrounding");
                options.AddArgument("--enable-features=NetworkService,NetworkServiceInProcess");
                options.AddArgument("--force-color-profile=srgb");
                options.AddArgument("--hide-scrollbars");
                options.AddArgument("--metrics-recording-only");
                options.AddArgument("--mute-audio");
                //options.AddArgument("--headless");
                options.AddArgument("--no-sandbox");
                options.AddUserProfilePreference("credentials_enable_service", false);
                options.AddUserProfilePreference("profile.password_manager_enabled", false);
                options.AddArgument("--disable-background-mode");
                options.AddArgument("--disable-features=VizDisplayCompositor");
                options.AddArguments("--dns-prefetch-disable");
                options.AddArgument("--disable-notifications");
                options.AddUserProfilePreference("download.prompt_for_download", false);
                options.AddUserProfilePreference("disable-popup-blocking", "true");
                return options;
            }
        }

        /// <summary>
        /// Method to retrieve Chrome Driver Path
        /// </summary>
        /// <returns>Chrome Driver Path</returns>
        public string DriverLocation
        {
            get
            {
               //return _autoUtils.GetProjectLocation() + @"\Drivers";
               return _autoUtils.GetProjectLocation();

            }
        }


        /// <summary>
        ///  Method to retrieve the Chrome 'User Data' path given in Properties file
        /// </summary>
        /// <returns>Chrome user data path</returns>

        public string UserDataLocation
        {
            get
            {
                return ConfigurationManager.AppSettings["ChromeUserDirectoryPath"];
            }
        }

        /// <summary>
        /// Method to retrieve the Chrome 'Profile Folder' path given in Properties file
        /// </summary>
        /// <returns>Chrome 'Profile Folder' path</returns>
        public string ProfileName
        {
            get
            {
                return ConfigurationManager.AppSettings["ChromeProfileFoldername"];
            }
        }

        /// <summary>
        /// Verifies if Chrome User Data Folder is available
        /// </summary>
        /// <returns>true/false</returns>
        public bool IsUserDataDirPresent()
        {
            string sUserData = UserDataLocation;
            try
            {
                if (sUserData.Length!= 0)
                {
                    return File.Exists(sUserData);
                }
                else
                {
                    return false;
                }
            }
            catch (NullReferenceException)
            {
                log4Net.Error("folder does not exists" + sUserData);
                //Assert.fail("folder does not exists"+sUserData);
                return false;
            }
        }

        /// <summary>
        /// Verifies if Chrome Profile Folder is available
        /// </summary>
        /// <returns>true/false</returns>
        public bool IsProfileDirPresent()
        {
            string profilePath = UserDataLocation + "/" + ProfileName;
            try
            {
                if (profilePath.Length!= 0)
                {
                    return File.Exists(profilePath);
                }
                else
                {
                    return false;
                }
            }
            catch (NullReferenceException)
            {
                log4Net.Error("Profile does not exists" + ProfileName);
                Assert.Fail("Profile does not exists" + ProfileName);
                return false;
            }
        }

        public static class KnownFolder
        {
            public static readonly Guid Downloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");
        }



        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out string pszPath);

    }
}
