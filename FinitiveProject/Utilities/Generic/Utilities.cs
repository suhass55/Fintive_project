using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using Gallio.Framework;
using log4net;
using OpenQA.Selenium;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using SikuliSharp;
using System.Reflection;
using System.Text;
using System.Globalization;
using AventStack.ExtentReports;
using Match = SikuliSharp.Match;
using Oracle.ManagedDataAccess.Client;

namespace SeleniumAutomation.Utilities
{
    public class AutomationUtilities
    {

        public static ILog log = LogManager.GetLogger("UtilityMethods");

        /*
         * Method : Gets the graph content based on regular expression given
         * Params : String source - Extracted content from flashvars
         * Returns: Matching pattern of extracted content by running a regular expression
         */

        public string[] GetGraphContent(string source)
        {
            string[] matches = new string[1];
            int iCount = 0;
            string sPattern = @"(Wells Fargo|Bank of America|Banking) \([0-9]{1,}\.[0-9]{2}\, [0-9]{1,}\.[0-9]{2}\)";
            foreach (Match m in Regex.Matches(source, sPattern))
            {
                log.Info(m.ToString());
                matches[iCount] = m.ToString();
                iCount++;
            }
            return matches;

        }
        public static string DownloadFilesPath()
        {
            Uri uri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string path = Directory.GetParent(uri.LocalPath).Parent.Parent.Parent.FullName;
            path = Path.Combine(path, "DownloadFiles");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public static string DownloadFilesPath(string AdditionalPath)
        {
            Uri uri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string path = Directory.GetParent(uri.LocalPath).Parent.Parent.Parent.FullName;
            if (AdditionalPath != "ExcelReadingAndReplyEmail")
            {
                path = Path.Combine(path, "DownloadFiles", AdditionalPath);
            }
            else
            {
                path = Path.Combine(path, "DownloadFiles");
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
        public static void DeleteFiles(string path)
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
        //Returns .. \\OSVExperience\OneAtmosphere folder path ...
        public string GetProjectLocation()
        {
            string sDirectory = Environment.CurrentDirectory;
            /*string sDir = Directory.GetParent(System.AppDomain.CurrentDomain.BaseDirectory).Parent.FullName+"\\Debug";*/
            Uri uri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string sDir = Directory.GetParent(uri.LocalPath).FullName;
            Console.WriteLine("Full project path (Batch Execution):=>>>>>>>" + sDir);
            log.Info("path is:=>" + sDir);
            return sDir;
        }

        public string GetFolder(string folder)
        {
            string ProjectDirectory = this.GetProjectLocation();
            string Folder = Directory.GetParent(ProjectDirectory).FullName;
            Folder = Path.Combine(Folder, folder);
            log.Info("==========> " + folder + " folder directory is:=>" + Folder);
            return Folder;
        }

        public string GetResourcesFolder()
        {
            Uri uri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string LoadPath = Directory.GetParent(uri.LocalPath).Parent.Parent.FullName;
            return LoadPath + @"\Resources\";
        }

        public void WriteDateToTaxExDataFile(string FieldName, string FieldValue)
        {
            string json = File.ReadAllText(TestDataJSONFilePath());
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            jsonObj[FieldName] = FieldValue;
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(TestDataJSONFilePath(), output);
        }

        public string GetDataFromTaxExDataFile(string FieldName)
        {
            string json = File.ReadAllText(TestDataJSONFilePath());
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            return jsonObj[FieldName].ToString();
        }

        public string TestDataJSONFilePath()
        {
            Uri uri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string LoadPath = Directory.GetParent(uri.LocalPath).Parent.Parent.FullName;
            string JsonFilePath = LoadPath + @"\TaxExTestData\CompaniesData.json";
            return JsonFilePath;
        }

        public string GetXMLFolder()
        {
            return GetResourcesFolder() + @"\XMLWithCurrentQuarter.xml";
        }
        public string GetXMLFolder1(string XMLName)
        {
            return GetResourcesFolder() + XMLName + ".xml";
        }
        
        public string GetTestDataLocation()
        {
            return GetProjectLocation() + @"\TestData\";
        }

        public string GetTestResultsLocation()
        {
            return GetUserDesktopPath() + @"\TestResults\";
        }
        public static string GetConfigTextFilePath()
        {
            return GetBasePath() + @"\Config.txt";
        }
        public static string GetBasePath()
        {
            string BaseDirToProjects = Directory.GetParent(System.AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            //  BaseDirToProjects = Path.Combine(BaseDirToProjects, "Selenium_Automation");
            log.Info("=================>>> base path is:=>" + BaseDirToProjects);
            return BaseDirToProjects;
        }
        public string GetTestOutputLocation()
        {
            return GetUserDesktopPath() + @"\TestOutput\";
        }

        public string GetUserDesktopPath()
        {
            return System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Desktop";
        }

        public string GetUserProfilePath()
        {
            return System.Environment.GetEnvironmentVariable("USERPROFILE");
        }

        public string GetCurrentDirectoryPath()
        {
            return Environment.CurrentDirectory;
        }

        public string GetSolutionLocation()
        {
            string sDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            return sDirectory;
        }

        /*
         * Method : Gets the browser name & process info and kills the instances
         * Params : Nill
         * Returns: True after killing all the instances
         */

        public bool KillBrowserInstances()
        {
            string sType = GetKeyValue("BROWSER", "Browser").ToLower();

            foreach (Process p in Process.GetProcesses())
            {
                if (sType == "ie")
                {
                    if (p.ProcessName == "iexplore")
                    {
                        p.Kill();
                    }
                }
                if (sType == "ff")
                {
                    if (p.ProcessName == "firefox")
                    {
                        p.Kill();
                    }
                }
                if (sType == "chrome")
                {
                    if (p.ProcessName == "chrome")
                    {
                        p.Kill();
                    }
                }
            }
            return true;

        }

        /// <summary>
        /// Gets the key from the properties file
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string getProperty(string Key, string FilePath)
        {
            // string propertiesfilename= @"readFile.properties";          
            string[] lines = System.IO.File.ReadAllLines(FilePath);
            for (int i = 0; i < lines.Length; i++)
            {
                string FileKey = Regex.Split(lines[i], "=")[0].Trim();
                if (FileKey == Key)
                    return Regex.Split(lines[i], "=")[1].Trim();
            }
            return "-";
        }


        public  class Testcase
        {
            static string sTestName;
            public string CurrentTest
            {
                get
                {
                    return sTestName;
                }
                set
                {
                    sTestName = value;
                }


            }
        }

        /*
         * Method : Generates the random number
         * Params : Nill
         * Returns: Void
         */

        public int Random(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }


        /*
         * Method : Assigns the time count to wait for an element to load
         * Params : String
         * Returns: Time count to wait for an element to load
         */
        public int SelectTimeCount(string sWait)
        {
            int iVal;

            switch (sWait)
            {
                case "VERYLONGWAIT":
                    sWait = ConfigurationManager.AppSettings["VERYLONGWAIT"];
                    iVal = Convert.ToInt16(sWait);
                    break;
                case "LONGWAIT":
                    sWait = ConfigurationManager.AppSettings["LONGWAIT"];
                    iVal = Convert.ToInt16(sWait);
                    break;
                case "MEDIUMWAIT":
                    sWait = ConfigurationManager.AppSettings["MEDIUMWAIT"];
                    iVal = Convert.ToInt16(sWait);
                    break;
                case "IMPLICITWAIT":
                    sWait = ConfigurationManager.AppSettings["IMPLICITWAIT"];
                    iVal = Convert.ToInt16(sWait);
                    break;
                case "SHORTWAIT":
                    sWait = ConfigurationManager.AppSettings["SHORTWAIT"];
                    iVal = Convert.ToInt16(sWait);
                    break;
                case "VERYSHORTWAIT":
                    sWait = ConfigurationManager.AppSettings["VERYSHORTWAIT"];
                    iVal = Convert.ToInt16(sWait);
                    break;
                default:
                    iVal = 3;
                    break;
            }
            return iVal;
        }


        public static DataTable ReadTable(string connectionString, string selectQuery)
        {
            var returnValue = new DataTable();

            var conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                var command = new SqlCommand(selectQuery, conn);

                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(returnValue);
                }

            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return returnValue;
        }
        public static void WriteDataTableToCSVFile(DataTable dataSource, string fileOutputPath, bool firstRowIsColumnHeader = false, string seperator = ";")
        {
            var sw = new StreamWriter(fileOutputPath, false);

            int icolcount = dataSource.Columns.Count;

            if (!firstRowIsColumnHeader)
            {
                for (int i = 0; i < icolcount; i++)
                {
                    sw.Write(dataSource.Columns[i]);
                    if (i < icolcount - 1)
                        sw.Write(seperator);
                }

                sw.Write(sw.NewLine);
            }

            foreach (DataRow drow in dataSource.Rows)
            {
                for (int i = 0; i < icolcount; i++)
                {
                    if (!Convert.IsDBNull(drow[i]))
                        sw.Write(drow[i].ToString());
                    if (i < icolcount - 1)
                        sw.Write(seperator);
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        public static void WaitForFileOperations(string directory, string fileOperation, int timeOutInSeconds)
        {
            log.Info("Waiting For File " + fileOperation + "operation...\n max timeout is" + timeOutInSeconds + " seconds");
            System.IO.FileSystemWatcher watcher = new System.IO.FileSystemWatcher(directory);
            switch (fileOperation)
            {
                case "created":
                    watcher.WaitForChanged(System.IO.WatcherChangeTypes.Created, timeOutInSeconds * 1000);
                    log.Info("A file is created");
                    break;
                case "deleted":
                    watcher.WaitForChanged(System.IO.WatcherChangeTypes.Deleted, timeOutInSeconds * 1000);
                    log.Info("A file is deleted");
                    break;
                case "changed":
                    watcher.WaitForChanged(System.IO.WatcherChangeTypes.Changed, timeOutInSeconds * 1000);
                    log.Info("A file is changed");
                    break;
                case "renamed":
                    watcher.WaitForChanged(System.IO.WatcherChangeTypes.Renamed, timeOutInSeconds * 1000);
                    log.Info("A file is renamed");
                    break;
                case "all":
                    watcher.WaitForChanged(System.IO.WatcherChangeTypes.All, timeOutInSeconds * 1000);
                    log.Info("A file is created/deleted/changed/renamed");
                    break;
                default:
                    log.Error("invalid file operation provided.." + fileOperation);
                    break;
            }

        }

        public static void WaitForFileCreation(String filePath, int timeOutInSeconds = 120)
        {
            log.Info("Will wait for file - " + filePath + " until " + timeOutInSeconds + " seconds");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (true)
            {
                if (sw.ElapsedMilliseconds > timeOutInSeconds * 1000) { break; }

                FileInfo fInfo = new FileInfo(filePath);
                if (File.Exists(filePath) && fInfo.Length > 0)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(250);
                    log.Info("waiting for file - " + filePath);
                }

            }
        }

        public static bool renameFile(string oldFileName, string newFileName)
        {
            try
            {
                File.Delete(newFileName); // Delete the existing file if exists
                File.Move(oldFileName, newFileName);
                return true;
            }
            catch (Exception e)
            {
                log.Error("exception occured.." + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }

        public void killProcesses(string processName)
        {
            foreach (var process in Process.GetProcessesByName(processName))
            {
                process.Kill();
            }
        }

        public void HandleFirefoxDownload(string fileName)
        {
            string filePath = new SeleniumAutomation.Base.FirefoxBrowser().DownloadLocation + @"\" + fileName;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            System.Diagnostics.Process.Start(new AutomationUtilities().GetCurrentDirectoryPath() + "\\clickdownload.exe");
            AutomationUtilities.WaitForFileCreation(filePath);
            //MessageBox.Show("downloaded");
            new AutomationUtilities().killProcesses("clickdownload");
        }



        public bool TryAddCookie(string URL, WebRequest webRequest, OpenQA.Selenium.Cookie cookie)
        {
            HttpWebRequest httpRequest = webRequest as HttpWebRequest;
            if (httpRequest == null)
            {
                return false;
            }

            if (httpRequest.CookieContainer == null)
            {
                httpRequest.CookieContainer = new CookieContainer();
            }
            System.Net.Cookie netCookie = new System.Net.Cookie();
            netCookie.Name = cookie.Name;
            netCookie.Value = cookie.Value;
            netCookie.Domain = cookie.Domain;
            netCookie.Path = cookie.Path;
            netCookie.Secure = cookie.Secure;
            httpRequest.CookieContainer.Add(netCookie);
            return true;
        }

        public int DownloadFileFromHTTPResponse(OpenQA.Selenium.IWebDriver driver, String remoteFilename, String localFilename)
        {
            // Function will return the number of bytes processed
            // to the caller. Initialize to 0 here.
            int bytesProcessed = 0;

            // Assign values to these objects here so that they can
            // be referenced in the finally block
            Stream remoteStream = null;
            Stream localStream = null;
            WebResponse response = null;

            // Use a try/catch/finally block as both the WebRequest and Stream
            // classes throw exceptions upon error
            try
            {
                // Create a request for the specified remote file name
                WebRequest request = WebRequest.Create(remoteFilename);
                string sPMusername = ConfigurationManager.AppSettings["Portal Username"];
                string sPMpassword = ConfigurationManager.AppSettings["Portal Password"];
                var cookies = driver.Manage().Cookies.AllCookies;
                //copy all cookies from browser.
                foreach (OpenQA.Selenium.Cookie cookie in cookies)
                    TryAddCookie(remoteFilename, request, cookie);
                //TryAddCookie(request,new Cookie("UserAgreement","Version=1.0.0&AcceptedDate=3/24/2014","/",".worklist-alpha.enerpath.com"));
                request.Credentials = new NetworkCredential(sPMusername, sPMpassword);
                if (request != null)
                {
                    // Send the request to the server and retrieve the
                    // WebResponse object 
                    response = request.GetResponse();
                    if (response != null)
                    {
                        // Once the WebResponse object has been retrieved,
                        // get the stream object associated with the response's data
                        remoteStream = response.GetResponseStream();

                        // Create the local file
                        localStream = File.Create(localFilename);

                        // Allocate a 1k buffer
                        byte[] buffer = new byte[1024];
                        int bytesRead;

                        // Simple do/while loop to read from stream until
                        // no bytes are returned
                        do
                        {
                            // Read data (up to 1k) from the stream
                            bytesRead = remoteStream.Read(buffer, 0, buffer.Length);

                            // Write the data to the local file
                            localStream.Write(buffer, 0, bytesRead);

                            // Increment total bytes processed
                            bytesProcessed += bytesRead;
                        } while (bytesRead > 0);
                    }
                }
            }
            catch (Exception e)
            {
                log.Info(e.Message);
            }
            finally
            {
                // Close the response and streams objects here 
                // to make sure they're closed even if an exception
                // is thrown at some point
                if (response != null) response.Close();
                if (remoteStream != null) remoteStream.Close();
                if (localStream != null) localStream.Close();
            }

            // Return total bytes processed to caller.
            return bytesProcessed;
        }

        /// <summary>
        /// Takes screenshot and saves it in specified folder
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="screenshotPath"></param>
        public bool TakeScreenshot(IWebDriver driver, string screenshotPath)
        {
            bool flag;
            try
            {
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                //Use it as you want now
                string screenshot = ss.AsBase64EncodedString;
                byte[] screenshotAsByteArray = ss.AsByteArray;
                ss.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png); //use any of the built in image formating
                ss.ToString();//same as string screenshot = ss.AsBase64EncodedString
                flag = true;
            }
            catch (Exception e)
            {
                log.Info(e.Message + e.StackTrace);
                flag = false;
            }
            return flag;
        }



        /// <summary>
        ///  getScreenshot
        /// </summary>
        /// Purpose: Capture the screenshot and merge with Gallio Report
        /// <return>Bitmap</returns>

        public string getScreenshot(IWebDriver _driver)
        {
            try
            {
                //New Method Added
                StringBuilder stringBuilder = new StringBuilder();
                //string Title = ScenarioContext.Current.ScenarioInfo.Title;   Title.Substring(0, 5) +
                string ScreenShotName = DateTime.Now.ToString("yyyyMMddHHmmss");
                if (File.Exists(ScreenShotName))
                {
                    File.Delete(ScreenShotName);
                }
                Uri uri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
                string LoadPath = Directory.GetParent(uri.LocalPath).Parent.Parent.Parent.FullName;
                string ScreenShotDirectory = LoadPath + "\\Automation_Report\\OutPut\\screenshots\\";
                if (!Directory.Exists(ScreenShotDirectory))
                {
                    Directory.CreateDirectory(ScreenShotDirectory);
                }
                string ScreenShotFileName = ScreenShotDirectory + ScreenShotName + ".png";
                ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile(ScreenShotFileName);
                string UrlFile = Path.GetFullPath(ScreenShotFileName);
                Console.WriteLine("Screenshot: {0}", new Uri(UrlFile));
                return UrlFile;
            }
            catch (ArgumentNullException e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(" Unable  to  Take  screenshot  because  of  " + e.Message);
            }
        }
        public MediaEntityModelProvider CreateScreenshotforExtentReport(IWebDriver _driver)
        {
            //string Title = ScenarioContext.Current.ScenarioInfo.Title;  Title.Substring(0, 5) +
            string ScreenShotName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var ScreenShot = ((ITakesScreenshot)_driver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(ScreenShot, ScreenShotName).Build();
        }
        /*
         * Method : Adds a new add tag (with key and value) to appsettings tag in cofig file
         * Params : key name of the key, 'value' is value of key 
         * Returns: void
         */
        public void addKeyToConfigFile(string key, string value)
        {
            try
            {
                // Open App.Config file
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Add key and value to Application Settings
                config.AppSettings.Settings.Add(key, value);
                config.Save(ConfigurationSaveMode.Modified);



                // Force a reload of a changed section.
                ConfigurationManager.RefreshSection("appSettings");

            }
            catch (Exception e)
            {
                log.Info("................................................>from addKeyToConfigFile()", e);
            }

        }

        /*
         * Method : Adds a new add tag (with key and value) to appsettings tag in cofig file
         * Params : key name of the key, 'value' is value of key 
         * Returns: void
         */
        public void configAppSettings(string key, string value)
        {
            ////load config document for current assembly 
            XmlDocument doc = loadConfigDocument();
            // retrieve appSettings node 
            XmlNode node = doc.SelectSingleNode("//appSettings");
            if (node == null)
            {
                throw new InvalidOperationException("appSettings section not found in config file.");
            }
            try
            {
                // select the 'add' element that contains the key 
                XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));
                if (elem != null)
                {
                    // add value for key 
                    elem.SetAttribute("value", value);
                }
                else
                {
                    // key was not found so create the 'add' element 
                    // and set it's key/value attributes 
                    elem = doc.CreateElement("add");
                    elem.SetAttribute("key", key);
                    elem.SetAttribute("value", value);
                    node.AppendChild(elem);
                }

                doc.Save(getConfigFilePath());
                addKeyToConfigFile(key, value);
            }
            catch
            {
                throw;
            }
        }
        public static XmlDocument loadConfigDocument()
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(new AutomationUtilities().getConfigFilePath());
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
        }
        public string getConfigFilePath()
        {

            return GetProjectLocation() + @"\Selenium_Automation\Config\app.config";
        }



        /*
          * Method : Gets data from text file based on key
          * Params : filepath, key 
          * Returns: string
          */
        public static string GetPropertyValueFromTextFile(string filePath, string property)
        {
            string val = null;
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {

                    if (line.Contains(property))
                    {
                        val = line.Split('=')[1];
                        return val.Trim();
                    }
                }
                if (val == null)
                {
                    log.Error(property + " property/key doenot exist in specified file");
                    ///throw new Exception(property+" property/key doenot exist in specified file");
                }
                return val;
            }
            catch (FileNotFoundException e)
            {
                log.Error("File is not present in " + filePath + " location" + e.Message);
                throw new FileNotFoundException("File is not present in " + filePath + " location");
            }
        }


        /*
         * Method : write property(data) to text file
         * Params : filepath, property to write 
         * Returns: bool
         */
        public static bool writetDataToTextFile(string filePath, string property)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filePath, true);

                sw.WriteLine("\n" + property);
                sw.Close();
                return true;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return false;
            }

        }

        public static bool WriteORUpdateTextFile(string filePath, string property)
        {
            try
            {

                StreamReader sr = new StreamReader(filePath);
                string fileContent = sr.ReadToEnd();
                string propertyName = property.Split('=')[0];
                string propertyVal = GetPropertyValueFromTextFile(filePath, propertyName);
                sr.Close();

                if (fileContent.Contains(property))
                {

                }
                else if (fileContent.Contains(propertyName))
                {

                    StreamWriter sw1 = new StreamWriter(filePath, false);
                    fileContent = fileContent.Replace(propertyName + "=" + propertyVal, property);
                    sw1.WriteLine(fileContent);
                    sw1.Close();
                }
                else
                {

                    StreamWriter sw2 = new StreamWriter(filePath, true);
                    sw2.WriteLine(property);
                    sw2.Close();

                }

                return true;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return false;
            }

        }




        public static bool DeleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);

                return true;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return false;
            }

        }


        public void HasThisTestPassed(string dependentTestName)
        {
            string val = null;
            string filePath = GetProjectLocation() + "\\TestData\\TestCasesStatus.txt";
            string currentTestName = null;
            try
            {
                currentTestName = TestContext.CurrentContext.Test.Name;
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {

                    if (line.Contains(dependentTestName))
                    {
                        val = line.Split('=')[1];

                        break;
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                log.Error(e.Message);
                throw new FileNotFoundException();
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception();
            }
            if (val == null)
            {
                log.Error("Dependent test - " + dependentTestName + " has null \n Hence test - " + currentTestName + "is ignored ");
                throw new Exception("Dependent test - " + dependentTestName + " has null \n Hence test - " + currentTestName + " is ignored ");
            }
            else if (val != "Passed")
            {
                log.Error("Dependent test - " + dependentTestName + " has failed \n Hence test - " + currentTestName + "is ignored ");
                throw new Exception("Dependent test - " + dependentTestName + " has failed \n Hence test - " + currentTestName + " is ignored ");
            }
        }



        /*
           * Method : Updates key old value with new value
           * Params : filepath, key, newvalue
           * Returns: bool
           */
        public static bool updatekeyValueInTextFile(string filePath, string key, string newval)
        {
            try
            {

                StreamReader sr = new StreamReader(filePath);
                string fileContent = sr.ReadToEnd();
                string oldval = GetPropertyValueFromTextFile(filePath, key);
                fileContent = fileContent.Replace(key + oldval, key + newval);
                sr.Close();

                StreamWriter sw = new StreamWriter(filePath);
                sw.WriteLine(fileContent);
                sw.Close();
                return true;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return false;
            }

        }

        public string GetCurrentDate()
        {
            string currentDate = DateTime.UtcNow.ToString("M/d/yyyy");
            return currentDate;
        }

        public string GetFutureDate(int month)
        {
            DateTime dt = DateTime.Now;
            return dt.AddMonths(month).ToString("M/d/yyyy");
        }

        /// <summary>
        /// Create a Oracle data base connection and returns the result
        /// </summary>
        /// <param name="Query"></param>
        public static Dictionary<string, string> GetDataFromOracleDB(string Query)
        {
            OracleConnection con = null;
            OracleDataReader reader = null;
            DataTable table = null;
            DataTable valueTable = new DataTable();
            Dictionary<string, string> fields = new Dictionary<string, string>();
            System.Collections.Generic.Dictionary<string, object> map = new System.Collections.Generic.Dictionary<string, object>();
            System.Collections.Generic.List<Dictionary<string, object>> list = new System.Collections.Generic.List<Dictionary<string, object>>();
            try
            {
                string connectionString = ConfigurationManager.AppSettings["DBConnectionString"];
                con = new OracleConnection(connectionString);
                con.Open();
                log.Info("Connected to Oracle:=>" + con.ServerVersion);
                OracleCommand command = con.CreateCommand();
                command.CommandText = Query;
                reader = command.ExecuteReader();
                table = reader.GetSchemaTable();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        for (int index = 0; index < reader.FieldCount; index++)
                        {
                            fields[reader.GetName(index)] = reader[index].ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Info("====>Exception occured while connectiong to DB:=>" + e.StackTrace);
            }
            finally
            {
                if (con != null)
                {
                    reader.Close();
                    con.Close();

                }
            }
            return fields;
        }

        /// Create a Oracle data base connection and returns the result
        /// </summary>
        /// <param name="Query"></param>
        public static List<Dictionary<int, string>> GetDataFromOracleDBMutipleRows(string Query)
        {
            OracleConnection con = null;
            OracleDataReader reader = null;
            List<Dictionary<int, string>> list = new List<Dictionary<int, string>>();
            Dictionary<string, List<string>> fields = new Dictionary<string, List<string>>();
            try
            {
                string connectionString = ConfigurationManager.AppSettings["DBConnectionString"];
                con = new OracleConnection(connectionString);
                con.Open();
                log.Info("Connected to Oracle:=>" + con.ServerVersion);
                OracleCommand command = con.CreateCommand();
                command.CommandText = Query;
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Dictionary<int, string> dict = new Dictionary<int, string>();
                        for (int index = 0; index < reader.FieldCount; index++)
                        {
                            string item = reader[index].ToString();
                            dict.Add(index, item);
                        }
                        list.Add(dict);
                    }
                }

            }
            catch (Exception e)
            {
                log.Info("====>Exception occured while connectiong to DB:=>" + e.StackTrace);
            }
            finally
            {
                if (con != null)
                {
                    reader.Close();
                    con.Close();
                }
            }
            return list;
        }
        /// <summary>
        ///  Method for generate the random text
        /// </summary>
        /// <params>No params</params>
        /// <return>String </returns
        public string GenerateRandomText()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path;
        }
        public string RandomDigitsN(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        /// <summary>
        ///  Method for getting the key value from config file
        /// </summary>
        /// <params>section name and keyname</params>
        /// <return>String </returns
        public string GetKeyValue(string sectionname, string keyname)
      {
          var section = (ConfigurationManager.GetSection(sectionname) as System.Collections.Specialized.NameValueCollection);
          return section[keyname];
      }        
        public string getScreenLocation()
      {
          string sDirectory = Directory.GetParent(GetProjectLocation()).FullName;
          string PathToscreenshot = Path.Combine(sDirectory, "Automation_Report");
          PathToscreenshot = Path.Combine(PathToscreenshot, "OutPut");
          PathToscreenshot = Path.Combine(PathToscreenshot, "screenshots\\");
          return PathToscreenshot;
      }


      /// <summary>
      /// Method for Running Sikuli Script
      /// We can perform many operations like click,find ,rightClick,doubleclick etc etc using the Sikuli IDE by creating a script,
      /// those scripts we can call directly by using the below method and the Path to Script as parameter to this method.
      /// e.g "@C:\Users\Admin\Desktop\ScreenSki.sikuli"
      /// 
      /// NOTE: Using Sikuli with Parallel testing and with Remote Driver is not reliable as when it interacts with Images,the Test case whose browser is 
      /// running upfront(visible on the screen) will get passed but other test cases which is running in back (not running upfront) will get failed
      /// We can use Sikuli for running test case Sequentially using LocalWebDriver instance.
      /// 
      /// Note:SikuliSharp wrapper is available for .net version 4.5 not for previous verison
      /// </summary>

      public void sikuliRunScript(String ScriptPath)
      {
          try
          {
              log.Info("Executing the Sikul Script with " + ScriptPath);
              Sikuli.RunProject(ScriptPath);
              log.Info("Successfully Executed Script :" + ScriptPath);
          }
          catch (Exception ex)
          {
              log.Error("Could not load the Sikuli script or SikuliScript.jar from Environment Path: "+ex.ToString());
          }

      }

      /// <summary>
      /// Method for Verifying the existance of a pattern i.e image in the Page
      /// 
      ///  NOTE: Using Sikuli with Parallel testing and with Remote Driver is not reliable as when it interacts with Images,the Test case whose browser is 
      /// running upfront(visible on the screen) will get passed but other test cases which is running in back (not running upfront) will get failed
      /// We can use Sikuli for running test case Sequentially using LocalWebDriver instance
      /// </summary>
      /// <param name="Pattern">Here "Pattern" refers to a partial(Sub image)/full image of a web image/page which we need to validate 
      /// e.g. We need to validate a part of google map,so we need to take a screenshot of that portion of the map and save it as a image
      /// with a name,and that saved image file's location along with the imagename is considered a "Pattern" i.e for an example @"C:\Users\Admin\Desktop\image.png"
      /// 
      /// Note:SikuliSharp wrapper is available for .net version 4.5 not for previous verison
      /// </param>

      public void SikuliPatternVerifier(String Pattern)
      {
          try
          {
              using (var session = Sikuli.CreateSession())
              {
                  log.Info("The Path of the Pattern is: " + Pattern);
                  var pattern = Patterns.FromFile(Pattern);
                  MbUnit.Framework.Assert.IsTrue(session.Exists(pattern));
                  log.Info("The image/Pattern " + Pattern + " is verified successfully with Sikuli ");
              }
          }
          catch (Exception)
          {
              log.Error("Exception occured while creating Sikuli session");
          }
      }

      public string GetAutomationReportsFolder()
      {
            Uri uri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string LoadPath = Directory.GetParent(uri.LocalPath).Parent.Parent.Parent.FullName;
            string automationReporterFolder = LoadPath + "\\Automation_Report";
            if (!Directory.Exists(automationReporterFolder))
            {
                Directory.CreateDirectory(automationReporterFolder);
            }
            log.Info("==========> Automation Reports folder directory is:=>" + automationReporterFolder);
            return automationReporterFolder;
      }

       public string GetExtentReportsFolder()
       {
           Uri uri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
           string LoadPath = Directory.GetParent(uri.LocalPath).Parent.Parent.Parent.FullName;
           string extentReporterFolder = LoadPath + "\\Automation_Report\\ExtentReports\\";
           if (!Directory.Exists(extentReporterFolder))
           {
               Directory.CreateDirectory(extentReporterFolder);
           }
           log.Info("==========> Extent Reports folder directory is:=>" + extentReporterFolder);
           return extentReporterFolder;
       }

        public string GetExtentReportsCopyFolder()
        {
            Uri uri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string LoadPath = Directory.GetParent(uri.LocalPath).Parent.Parent.Parent.FullName;
            string extentReporterFolder = LoadPath + "\\ExtentReportsUpdated\\";
            if (!Directory.Exists(extentReporterFolder))
            {
                Directory.CreateDirectory(extentReporterFolder);
            }
            log.Info("==========> Extent Reports folder directory is:=>" + extentReporterFolder);
            return extentReporterFolder;
        }

        /**
    * 
    * @param timeDisplayed:DD/MM/YYYY HH:mm GMT/CST 
    * @param plusOrMinusMinutes
    * @return DD/MM/YYYY HH:mm (+2 minutes)
    */

        public static String customDateUsingSystemDate(String dateFormat, int plusOrMinusDays, int plusOrMinusMonths, int plusOrMinusYears)
        {

            String modifiedDate = null;
            DateTime dt = DateTime.Now.AddDays(plusOrMinusDays).AddMonths(plusOrMinusMonths).AddYears(plusOrMinusYears);
            modifiedDate = dt.ToString(dateFormat, CultureInfo.GetCultureInfo("en-US"));
            Console.WriteLine("Customed Date : " + modifiedDate);
            return modifiedDate;
        }


        public static String customDateUsingGivenDate(string date, String dateFormat, int plusOrMinusDays, int plusOrMinusMonths, int plusOrMinusYears)
        {

            String modifiedDate = null;
            DateTime dt = Convert.ToDateTime(date).AddDays(plusOrMinusDays).AddMonths(plusOrMinusMonths).AddYears(plusOrMinusYears);
            modifiedDate = dt.ToString(dateFormat, CultureInfo.GetCultureInfo("en-US"));
            Console.WriteLine("Customed Date : " + modifiedDate);
            return modifiedDate;
        }

        public static void CreateTxtFileWithFederalTaxDetails(string path, string Code, string CheckDate)
        {
            try
            {
                StreamWriter stream = new StreamWriter(path+ "TD_Federal.txt");
                stream.WriteLine("Company_Code	TaxDetail_CheckDate	TaxDetail_PayCycleCode	TaxDetail_TaxCode	TaxDetail_IsPaid	TaxDetail_Tax	TaxDetail_Taxable	TaxDetail_Gross	TaxDetail_Resident	TaxDetail_SummaryFlag	TaxDetail_CreditCertificationNumber	Error_Message");
                stream.WriteLine(Code + "\t" + CheckDate + "\t"+ "RAB	FE FFCRA SICK EE	0	-101	0	0	0	1	CC0427202101	");
                stream.WriteLine(Code + "\t" + CheckDate + "\t" + "RAB	FE FFCRA FAMILY	0	-102	0	0	0	1	CC0427202102	");
                stream.WriteLine(Code + "\t" + CheckDate + "\t" + "RAB	FE FFCRA FAMILY ER HEALTH	0	-105	0	0	0	1	CC0427202103	");
                stream.WriteLine(Code + "\t" + CheckDate + "\t" + "RAB	FE FFCRA SICK OTHERS	0	-106	0	0	0	1	CC0427202104	");
                stream.WriteLine(Code + "\t" + CheckDate + "\t" + "RAB	FE FFCRA SICK EE MED	0	-107	0	0	0	1	CC0427202105	");
                stream.WriteLine(Code + "\t" + CheckDate + "\t" + "RAB	FE FFCRA SICK OTHERS MED	0	-108	0	0	0	1	CC0427202106	");
                stream.WriteLine(Code + "\t" + CheckDate + "\t" + "RAB	FE FFCRA SICK ER HEALTH	0	-109	0	0	0	1	CC0427202107	");
                stream.WriteLine(Code + "\t" + CheckDate + "\t" + "RAB	FE FFCRA FAMILY MED	0	-110	0	0	0	1	CC0427202108	");
                stream.WriteLine(Code + "\t" + CheckDate + "\t"+ "RAB	FE CARES EE RETENTION	0	-111	0	0	0	1	CC0427202109	");
                stream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void CopyFilesFromtoTo(string sourcePath, String targetPath, long RandomValueWithTime)
        {
            Directory.CreateDirectory(targetPath);
            //index.html
            string IndexReportFile = System.IO.Path.Combine(sourcePath, "index.html");
            string RenamedIndexName = "Index" + "_" + RandomValueWithTime + ".html";                       
            string MovedIndexFile = System.IO.Path.Combine(targetPath, RenamedIndexName);
            File.Copy(IndexReportFile, MovedIndexFile, true);

            //dashboard.html
            //string DashboardReportFile = System.IO.Path.Combine(sourcePath, "dashboard.html");
            //string RenamedDashboardName = "Dashboard" + "_" + RandomValueWithTime + ".html";
            //string MovedDashboardFile = System.IO.Path.Combine(targetPath, RenamedDashboardName);
            //File.Copy(DashboardReportFile, MovedDashboardFile, true);

            //Log4NetLoggerDetails.log
            string LogReportFile = System.IO.Path.Combine(sourcePath, "Log4NetLoggerDetails.log");
            string RenamedLogName = "Log4NetLoggerDetails" + "_" + RandomValueWithTime + ".log";
            string MovedLogFile = System.IO.Path.Combine(targetPath, RenamedLogName);
            File.Copy(LogReportFile, MovedLogFile, true);

        }
        

    }


}