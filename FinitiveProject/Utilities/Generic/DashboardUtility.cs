
using log4net;
using Newtonsoft.Json;
using SeleniumAutomation.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;


namespace OneAtmosphere.Utilities.Generic
{
    public class DashboardUtility  
    {
        public ILog log = LogManager.GetLogger("WriteToFile");
        public string liveDashboardJsonFile = new AutomationUtilities().GetFolder("LiveDashboard") + @"\FunctionalTestResults" + ".json";
        public string liveDashboardJsonFolder = new AutomationUtilities().GetFolder("LiveDashboard") + @"\JsonFolder";
        static bool updateStatus = false;
        public List<ExecutionData> ConsolidateJsonData;


        /// <summary>
        /// GetNewJsonFileFullPath is used to retun a string with unique filename
        /// </summary>
        /// <returns>string</returns>
        public string GetNewJsonFileFullPath()
        {
            string randomNumber = new Random().Next(10000).ToString();
            string jsonFileFullPath = Path.Combine(liveDashboardJsonFolder, DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss:ms").Replace(":", "_").Replace(" ", "")
                + randomNumber + ".json");
            return jsonFileFullPath;
        }
        /// <summary>
        /// UpdateRuntimeReport method is used to create a json file with Execution data
        /// </summary>
        /// <param name="_object"> Execution data </param>
        /// <param name="jsonFileName">Json file name</param>
        public void UpdateRuntimeReport(ExecutionData _object,string jsonFileName)
        {
            var json = JsonConvert.SerializeObject(_object); 
            log.Info("Before Update Json Data::" + _object.Tags+ " jsonFileName::"+ jsonFileName);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            try
            {
                FileStream reader = new FileStream(jsonFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                var sw = new StreamWriter(reader, Encoding.UTF8);
                    sw.WriteLine("[");
                    sw.WriteLine(json);
                    sw.WriteLine("]");
                    sw.Close();
            }
            catch(Exception e)
            {
                log.Info("exception occurs while Adding runtime results in livedashbaord json" + e.StackTrace);
            }
            UpdateLiveDashboardData();
        }
        /// <summary>
        /// CleanDashboardTestResult method is used to create a backup of livedashboard json file,
        /// clean live dashboard json file and clean json folder
        /// </summary>
        public void CleanDashboardTestResult()
        {
            try
            {
                if (File.Exists(liveDashboardJsonFile))
                {
                    string date = DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss:ms").Replace(":", "_");
                    string functionalTestResults = new AutomationUtilities().GetFolder("LiveDashboard") + @"\FunctionalTestResults"+date+ ".json";
                    File.Move(liveDashboardJsonFile, functionalTestResults);
                    File.Delete(liveDashboardJsonFile);
                    log.Info("Backup copy created fot FunctionalTestResults json");
                    log.Info("FunctionalTestResults json file deleted");
                }
                CleanLivedashboardJsonFolder();
            }
            catch (Exception e)
            {
                log.Info(e.StackTrace + " Exception occured While Deleting Json file");
            }
        }
        /// <summary>
        /// CleanLivedashboardJsonFolder method is for clean livedashboard json folder
        /// </summary>
        public void CleanLivedashboardJsonFolder()
        {
            try
            {
                if (Directory.Exists(liveDashboardJsonFolder))
                {
                    Directory.Delete(liveDashboardJsonFolder, true);
                    log.Info("Deleted json Folder");
                }
            }
            catch (Exception e)
            {
                log.Info(e.StackTrace + " Exception occured While Deleting Json file");
            }
        }
        /// <summary>
        /// CreateDashboardTestResultFolder method is used to create a JsonFolder if not exists
        /// </summary>
        public void CreateDashboardTestResultFolder()
        {
            try
            {
                if (!Directory.Exists(liveDashboardJsonFolder))
                {
                    Directory.CreateDirectory(liveDashboardJsonFolder);
                    log.Info("LiveDashboard JsonFolder created");
                }
            }
            catch (Exception e)
            {
                log.Info(e.StackTrace + " Exception occured While Creating Json folder");
            }
        }

        /// <summary>
        /// UpdateLiveDashboardData method is used to update Functional json file before and after execution
        /// </summary>
        public void UpdateLiveDashboardData()
        {
            ConsolidateJsonData = new List<ExecutionData>();
            try
            {
                string[] files = Directory.GetFiles(liveDashboardJsonFolder);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                foreach (string file in files)
                {
                    if (file.ToLower().Contains(".json"))
                    {
                         FileStream reader = new FileStream(file, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                        var sr = new StreamReader(reader, Encoding.UTF8);
                        string json = sr.ReadToEnd();
                        sr.Close();
                        var dashBoardJson = ser.Deserialize<List<ExecutionData>>(json);
                        foreach(ExecutionData jsonData in dashBoardJson)
                        {
                            ConsolidateJsonData.Add(jsonData);
                        }
                    }
                }
                var obj = ser.Serialize(ConsolidateJsonData);
                File.WriteAllText(liveDashboardJsonFile, obj, Encoding.UTF8);
            }
            catch (Exception e)
            {
                log.Info(e.StackTrace + " Exception occured While Consolidate Json LiveDashboard Data");
            }
        }
        /// <summary>
        /// UpdateAfterExecution method is to updates Test Results after execution in json file
        /// </summary>
        /// <param name="_object"></param>
        /// <param name="jsonFileName"></param>
        public void UpdateAfterExecution(ExecutionData _object,string jsonFileName)
        {
            
                JavaScriptSerializer ser = new JavaScriptSerializer();
            try { 
                FileStream reader = new FileStream(jsonFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                var sr = new StreamReader(reader, Encoding.UTF8);
                var sw = new StreamWriter(reader, Encoding.UTF8);
                string json = sr.ReadToEnd();
                sr.Close();
                var dashBoardJson = ser.Deserialize<List<ExecutionData>>(json);
                log.Info("After Update Json Data::" + _object);

                for (int i = dashBoardJson.Count - 1; i >= 0; i--)
                {
                    if (dashBoardJson[i].Tags.Equals(_object.Tags) && dashBoardJson[i].Feature.Equals(_object.Feature) && dashBoardJson[i].Scenario.Equals(_object.Scenario))
                    {
                        dashBoardJson[i].Date = _object.Date;
                        dashBoardJson[i].Scenario = _object.Scenario;
                        dashBoardJson[i].Feature = _object.Feature;
                        dashBoardJson[i].Duration = _object.Duration;
                        dashBoardJson[i].ExecutionStatus = _object.ExecutionStatus;
                        dashBoardJson[i].ExceptionInfo = _object.ExceptionInfo;
                        dashBoardJson[i].ScreenshotPath = _object.ScreenshotPath;
                        dashBoardJson[i].Tags = _object.Tags;
                        updateStatus = true;
                        break;
                    }
                }
                if (updateStatus == false)
                {
                    dashBoardJson.Add(_object);
                }
                var obj = ser.Serialize(dashBoardJson);
                File.WriteAllText(jsonFileName, obj, Encoding.UTF8);
                
            }
            catch(Exception e)
            {
                log.Info("exception occurs while Updating runtime results in livedashbaord json" + e.StackTrace);
            }
            UpdateLiveDashboardData();
        }
    }
}
