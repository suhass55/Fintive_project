//using log4net;
using MbUnit.Framework;
using OpenQA.Selenium;
using SeleniumAutomation.Utilities;
using SeleniumAutomation.Listener;
using System.Diagnostics;
using System;
using System.Text.RegularExpressions;
using SpecFlow.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using SpecFlowSharp;
using log4net;

namespace SeleniumAutomation.Base
{
    public class BaseClass
    {
        public IWebDriver Driver;
        public ILog log4N;
        public BalloonPopuUp balloon;
        ChromeBrowser _chromebrowser;
        public AutomationUtilities _autoutilities ;
        public static Reporter reporter;

        public static int Total_Test_Count, Failed_Test_Count;
        public static int Failcount, Passcount, Skipcount = 0;
        public static Stopwatch stopwatch = new Stopwatch();
        public static string reportsPath;
        public static string FileName;


        public BaseClass()
        {
            new Waits().setWaits();
            log4N = LogManager.GetLogger("BaseClass");
            log4N.Info("Initializing waits..");
            balloon = new BalloonPopuUp();
            _autoutilities = new AutomationUtilities();
        }


        /// <summary>
        ///Getter method for Webdriver in which instance the testcase need to run whether on linear mode/null or remote mode(Grid and Saucelabs) Use this whenever want to use/pass webdriver
        /// </summary>
        /// <params>None</params>
        /// <return>Webdriver Instance</returns>

        public IWebDriver GetDriver(string featureName)
        {
            if (_autoutilities.GetKeyValue("MODEOFEXECUTION", "ExecutionMode").ToLower() == "linear" || _autoutilities.GetKeyValue("MODEOFEXECUTION", "ExecutionMode") == "" || _autoutilities.GetKeyValue("MODEOFEXECUTION", "ExecutionMode").ToLower() == null)
            {
                Driver = InitialSetupWebdriver(featureName);
            }
            else if (_autoutilities.GetKeyValue("MODEOFEXECUTION", "ExecutionMode").ToLower() == "remote")
            {
                //Driver = new RemoteBrowser().InitialiseRemoteDriver();
            }

            WebListener webListener = new WebListener(Driver);
            Driver = webListener.Driver;

            return Driver;
        }


        /// <summary>
        ///This method selects the required browsers from Config file, instantiates respective browser driver returns the driver.
        /// </summary>
        /// <params>None</params>
        /// <return>Webdriver Instance</returns>

        public IWebDriver InitialSetupWebdriver(string featureName)
        {
            try
            {
                IWebDriver _driver = SelectBrowser(Driver, featureName);
                log4N.Info("Initiated Webdriver...");
                _driver.Manage().Cookies.DeleteAllCookies();
                _driver.Manage().Window.Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width + 10, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height + 10);
                return _driver;
            }
            catch (WebDriverException E)
            {
                Assert.Fail("Error while Initializing the Webdriver class" + E.Message);
                return null;
            }
        }

        /// <summary>
        ///Method for selecting Browser by providing browsere type  along the with WebDriver Driver instances . If the browser type is null it will take as ff other than chrome.ie,ff/firefox or null ,  it will provide an error message of invalid browser type. 
        /// </summary>
        /// <params>Driver instance</params>
        /// <return>WebDriver Instance</returns>

        public IWebDriver SelectBrowser(IWebDriver _driver, string featureName)
        {
            string sType = _autoutilities.GetKeyValue("BROWSER", "Browser");
            switch (sType)
            {
                case "ie":
                    _driver = new InternetExplorerBrowser().InitIEDriver();
                    break;
                case "ff":
                case "firefox":
                    _driver = new FirefoxBrowser().GetFirefoxDriver();
                    break;
                case "chrome":
                    _chromebrowser = new ChromeBrowser();
                    _driver = _chromebrowser.InitChromeDriver(featureName);
                    break;
                case "":
                    _driver = new FirefoxBrowser().GetFirefoxDriver();
                    break;
                default:
                    Assert.Fail("Invalid Browser name specified in Config file");
                    break;

            }
            return _driver;
        }


        /// <summary>
        /// Metohod for Quiting all Driver Instances  
        /// </summary>
        /// <params>Driver instance</params>
        /// <return>None</returns>

        public void closeInstances(IWebDriver Driver)
        {
            balloon.disposeIcon();
            Driver.Quit();
        }

        public void CreateFolderForDownloads()
        {
            reportsPath = AutomationUtilities.DownloadFilesPath(TestBase.FeatureName);
            log4N.Info("Reports path is " + reportsPath);
            if (!Directory.Exists(reportsPath))
            {
                Directory.CreateDirectory(reportsPath);
                log4N.Info("Folder Download files created ...");
            }
            CleanDownloadsDir(reportsPath);
        }

        public static void CleanDownloadsDir(string path)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        public string GetFileName(string FilePath)
        {
            string FileName = Path.GetFileName(FilePath);
            log4N.Info("Downloaded file name is " + FileName);
            return FileName;
        }
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out string pszPath);

        //public void VerifyFileExists(string FileName)
        //{
        //    reportsPath = AutomationUtilities.DownloadFilesPath();
        //    string FileName1 = Path.GetFileName(reportsPath + @"\" + FileName.ToString());

        //    log4N.Info("Downloaded files project path:=>>>>>>>" + reportsPath + @"\" + FileName.ToString());

        //    if (File.Exists(reportsPath + @"\" + FileName))
        //    {
        //        log4N.Info(" file successfully downloaded to path " + reportsPath + @"\" + FileName.ToString());
        //    }
        //    else
        //    {
        //        log4N.Info(" file not downloaded");
        //        Assert.Fail("file not downloaded");
        //    }

        //}

        public static string FileName1;

        //public string VerifyDownloadedFileName(string FileName)
        //{
        //    reportsPath = AutomationUtilities.DownloadFilesPath();
        //    string[] filePaths = Directory.GetFiles(@reportsPath);

        //    foreach (string file in filePaths)
        //    {
        //        log4N.Info(Path.GetFileName(file));
        //        FileName1 = Path.GetFileName(file);
        //    }
        //    log4N.Info("Downloaded files project path:=>>>>>>>" + reportsPath + @"\" + FileName1.ToString());

        //    if (File.Exists(reportsPath + @"\" + FileName1))
        //    {
        //        log4N.Info(" file successfully downloaded to path " + reportsPath + @"\" + FileName1.ToString());
        //    }
        //    else
        //    {
        //        log4N.Info(" file not downloaded");
        //        Assert.Fail("file not downloaded");
        //    }

        //    Assert.Contains(FileName1, FileName.ToString());

        //    return reportsPath + @"\" + FileName1;
        //}

        public string VerifyDownloadedFileName(string filename)
        {
            string NoticedFileName=null;
            string rootpath = AutomationUtilities.DownloadFilesPath();
            string[] filePaths = Directory.GetFiles(rootpath);
            foreach (string file in filePaths)
            {
                log4N.Info(Path.GetFileName(file));
                NoticedFileName = Path.GetFileName(file);
                if (NoticedFileName.Contains(filename))
                {
                    log4N.Info($"Looking fine {filename} is Downloaded properly");
                    return Path.Combine(rootpath, NoticedFileName).ToString();
                }
            }
            //if (File.Exists(Path.Combine(rootpath, filename)))
            //{
            //    log4N.Info($"Looking fine {filename} is Downloaded properly");
            //    return Path.Combine(rootpath, filename).ToString();
            //}

            foreach (string subDir in Directory.GetDirectories(rootpath, "*", SearchOption.AllDirectories))
            {
                string[] filePathsSub = Directory.GetFiles(subDir);
                foreach (string file in filePathsSub)
                {
                    log4N.Info(Path.GetFileName(file));
                    NoticedFileName = Path.GetFileName(file);
                    if (NoticedFileName.Contains(filename))
                    {
                        log4N.Info($"Looking fine {filename} is Downloaded properly");
                        return Path.Combine(rootpath, subDir, NoticedFileName).ToString();
                    }
                }
                //if (File.Exists(Path.Combine(subDir, filename)))
                //{
                //    log4N.Info($"Looking fine {filename} is Downloaded properly");
                //    return Path.Combine(rootpath, subDir, filename).ToString();
                //}
            }
            log4N.Info($"Looking fine {filename} is not Downloaded properly");
            Assert.Fail($"Looking file {filename} is not presented under {rootpath} and its sib directories");
            return null;
        }

        public static string GetFilePathInDir(string path, string fileSubString, string filetype)
        {
            try
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                //foreach (FileInfo file in di.GetFiles())
                foreach (FileSystemInfo file in di.GetFileSystemInfos())
                {
                    //In case of folder search ...
                    if ((filetype == "") && file.Name.Contains(fileSubString))
                    {
                        return file.FullName;
                    }
                    // In case of file search ...
                    else if (file.Name.Contains(fileSubString) && file.Name.EndsWith(filetype))
                    {
                        return file.FullName;
                    }

                }
                return "File Not Found";
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return "File Not Found";
            }
        }

        public string VerifyFileExistsinDownloadsLocation(string filename)
        {
            string rootpath = AutomationUtilities.DownloadFilesPath();
            if (File.Exists(Path.Combine(rootpath, filename)))
            {
                log4N.Info($"Looking fine {filename} is Downloaded properly");
                return Path.Combine(rootpath, filename).ToString();
            }                
            foreach (string subDir in Directory.GetDirectories(rootpath, "*", SearchOption.AllDirectories))
            {
                if (File.Exists(Path.Combine(subDir, filename)))
                {
                    log4N.Info($"Looking fine {filename} is Downloaded properly");
                    return Path.Combine(rootpath, subDir, filename).ToString();
                }
            }
            log4N.Info($"Looking fine {filename} is not Downloaded properly");
            Assert.Fail($"Looking file {filename} is not presented under {rootpath} and its sib directories");
            return null;
        }

        //private bool FileExistsRecursive(string rootPath, string filename)
        //{
        //    if (File.Exists(Path.Combine(rootPath, filename)))
        //        return true;

        //    foreach (string subDir in Directory.GetDirectories(rootPath))
        //    {
        //        if (FileExistsRecursive(subDir, filename))
        //            result true;
        //    }

        //    return false;
        //}



    }
}