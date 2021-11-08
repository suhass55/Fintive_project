using SeleniumAutomation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Gurock.TestRail;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpecFlowSharp.Utilities.Generic
{
    public class TestRailUtils
    {
        public AutomationUtilities _autoutilities = new AutomationUtilities();
        private log4net.ILog _testRailUtilsLog = log4net.LogManager.GetLogger("TestRailUtils");
        public string RunId;
        public string TestCaseId;
        /// <summary>
        /// Method for getting the Run Id for Test Rail from the Config file
        /// </summary>
        /// <returns></returns>
        public string GetRunIdForTestRail()
        {
            string RunId = "";
            try
            {
                RunId = _autoutilities.GetKeyValue("TestRailConfig", "ExistingRunID");
                _testRailUtilsLog.Info("RunID is : " + _testRailUtilsLog);
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error("Unable to get the RunId from the Config File: "+ex.ToString());
            }
            return RunId;
        }


        /// <summary>
        /// Method for getting All the test cases which is under a Project and a Suite
        /// </summary>
        /// <returns>Dictionary<string,string></returns>
        public Dictionary<string, string> GetAllTestCases()
        {
            Gurock.TestRail.APIClient client = GetTestRailClientConfig();
            string ProjectId = GetProjectID();
            string SuiteId = GetSuiteId();
            Dictionary<string, string> AllTestCases = new Dictionary<string, string>();
            JArray response = (JArray)client.SendGet("get_cases/" + ProjectId + "&suite_id=" + SuiteId);
            for (int i = 0; i < response.LongCount(); i++)
            {
                JObject Case = (JObject)response.ElementAt(i);
                AllTestCases.Add(Case.GetValue("id").ToString(), Case.GetValue("title").ToString());
            }
            return AllTestCases;
        }

        /// <summary>
        /// Method for getting Test Case Title from the FeatureFile
        /// </summary>
        /// <returns>string</returns>

        public string GetCaseNamesFromFeatureFile()
        {
            string TC_Name = "";
            try
            {
                string TC_Description = "Test"; // ScenarioContext.Current.ScenarioInfo.Title.ToString();
                TC_Name = TC_Description;

            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error("Unable to fetch the Test Case Id from the Config File");
                _testRailUtilsLog.Error(ex);
            }
            return TC_Name;

        }

        /// <summary>
        /// Method for getting the Test Case ID with respect to Feature file and as well as TestRail Suite and TestRun
        /// </summary>
        /// <returns></returns>
        public string GetTestCase()
        {
            string NameOfTest = GetCaseNamesFromFeatureFile();
            Dictionary<string, string> AllCases = GetAllTestCasesFromAllSuites();
            foreach (KeyValuePair<string, string> entry in AllCases)
            {
                if (entry.Value.Equals(NameOfTest))
                {
                    _testRailUtilsLog.Info("matched for test:=>" + NameOfTest);
                    return entry.Key;
                }
                else
                {
                    _testRailUtilsLog.Info("The Test Case name does not get matched");
                }

            }
            return "";
        }

        /// <summary>
        /// Method for fetching the Test Rail Client configuration from the Config file
        /// </summary>
        /// <returns></returns>
        public Gurock.TestRail.APIClient GetTestRailClientConfig()
        {
            Gurock.TestRail.APIClient client = null;
            try
            {
                client = GetClientServerUrl();
                string UserClientName = GetUserClientName(client);
                string UserClientPwd = GetUserClientPwd(client);
                _testRailUtilsLog.Info("UserClientName" + UserClientName);
                _testRailUtilsLog.Info("UserClientPwd" + UserClientPwd);
                _testRailUtilsLog.Info("Client" + client);
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error("Unable to fetch the Client configuration from the Config File");
                _testRailUtilsLog.Error(ex);
            }
            return client;
        }


        /// <summary>
        /// Method for getting the the Client User name for Test Rail Account
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public string GetUserClientName(Gurock.TestRail.APIClient client)
        {
            try
            {
                client.User = _autoutilities.GetKeyValue("TestRailConfig", "UserClientName");
                _testRailUtilsLog.Info("UserClientName" + client.User);
                return client.User;
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error("Exception occured while retrieving the Client name "+ex);
                return null;
            }
        }

        /// <summary>
        /// Method for getting the Server's Url for the Test Rail Profile
        /// </summary>
        /// <returns></returns>
        public Gurock.TestRail.APIClient GetClientServerUrl()
        {
            string ServerUrl = "";
            try
            {
                ServerUrl = _autoutilities.GetKeyValue("TestRailConfig", "ClientServerUrl");
                _testRailUtilsLog.Info("UserClientName" + ServerUrl);

            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error("Unable to fetch the Server URL from Config File");
                _testRailUtilsLog.Error(ex);
            }

            return new Gurock.TestRail.APIClient(ServerUrl);
        }

        /// <summary>
        /// Method for getting the the Client Password for Test Rail Account
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public string GetUserClientPwd(Gurock.TestRail.APIClient client)
        {
            try
            {
                client.Password = _autoutilities.GetKeyValue("TestRailConfig", "UserClientPwd");
                _testRailUtilsLog.Info("UserClientPassword" + client.Password);
                return client.Password;
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error("Unable to get the User Client Password "+ex);
                return null;
            }
        }

        /// <summary>
        /// Method for capturing the Failed Test status and Error
        /// </summary>
        /// <param name="testResult"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public Dictionary<string, object> CaptureFailResult(Dictionary<string, object> testResult, string Error)
        {
            testResult["status_id"] = "5"; //failed;
            testResult["comment"] = Error;
            return testResult;
        }

        /// <summary>
        /// Method for Capturing the Test Result which is passed
        /// </summary>
        /// <param name="testResult"></param>
        /// <returns></returns>
        public Dictionary<string, object> CapturePassResult(Dictionary<string, object> testResult)
        {
            testResult["status_id"] = "1"; //passed
            testResult["comment"] = "Test Is Passed Successfully";
            return testResult;
        }

        /// <summary>
        /// Method for Posting the Test status and failure information(if any) to Test Rail
        /// </summary>
        /// <param name="client"></param>
        /// <param name="RunId"></param>
        /// <param name="TestCaseId"></param>
        /// <param name="testResult"></param>
        public void PostResultToTestRail(Gurock.TestRail.APIClient client, string RunId, Dictionary<string, Object> testResult)
        {
            //RunId = GetRunId();
            TestCaseId = GetTestCase();
            try
            {
                client.SendPost("add_result_for_case/" + RunId + "/" + TestCaseId + "", testResult);
                _testRailUtilsLog.Info("Client Post is successfull");
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error("Unable to Post the result to Test rail" + ex);
            }
        }

        public void PostResultToTestRailUsingMileStones(Gurock.TestRail.APIClient client, Dictionary<string, Object> testResult)
        {
            string MileStoneId = GetMileStoneId();
            IList<string> RunIds = new List<string>();
            RunIds = GetRunIdsForProjectAndMileStone(MileStoneId);
            _testRailUtilsLog.Info("=====>>>>" + RunIds.ToString());
            _testRailUtilsLog.Info("size:=>" + RunIds.Count);
            foreach (string RunId in RunIds)
            {
                _testRailUtilsLog.Info("==>" + RunId);
                // _testRailUtilsLog.Info("&&" + RunId + "/" + TestCaseId);
                IList<string> TestCaseId = getCaseIdsForRun(RunId);
                foreach (string CaseId in TestCaseId)
                {
                    if (CaseId.Equals(GetTestCase()))
                    {
                        client.SendPost("add_result_for_case/" + RunId + "/" + CaseId + "", testResult);
                    }
                }
                _testRailUtilsLog.Info("Client Post is successfull");
            }

        }



        /// <summary>
        /// Method for generating a New Test Run Configuration at run time and Posting to the
        /// TestRail
        /// </summary>
        /// <returns>string</returns>

        public string AddTestRun()
        {
            try
            {
                Gurock.TestRail.APIClient client = GetTestRailClientConfig();
                Dictionary<string, object> data = new Dictionary<string, object>();
                string RunName = GenerateTestRunName("TestRun-");
                string Project_Id = GetProjectID();
                string MileStoneID = GetMileStone();
                string SuiteId = GetSuiteId();
                data.Add("milestone_id", MileStoneID);
                data.Add("name", RunName);
                data.Add("include_all", 1);
                data.Add("suite_id", SuiteId);
                client.SendPost("add_run/" + Project_Id, data);//2
                return RunName;
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error("Unable to Add new Test Run "+ ex);
                return null;

            }
        }

        /// <summary>
        /// 
        /// Method for Generating the Current Date TimeStamp
        /// </summary>
        /// <returns></returns>
        private string getCurrentDateTime()
        {
            _testRailUtilsLog.Info("Get current date");
            DateTime date = DateTime.Now;
            string Date = date.ToString("yyyy/MM/dd__HH-mm-ss");
            return Date;

        }

        /// <summary>
        /// Method for Generating the TestRun Name 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string GenerateTestRunName(string Name)
        {
            try
            {
                string RunName = Name + getCurrentDateTime();
                return RunName;
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error("unable to Generate the Test Run Name "+ ex);
            }
            return null;
        }


        /// <summary>
        /// Method for getting the Project Name from Config file for Test Rail Client
        /// </summary>
        /// <returns></returns>
        public string GetProjectName()
        {

            try
            {
                Gurock.TestRail.APIClient client = GetTestRailClientConfig();
                string ProjectName = _autoutilities.GetKeyValue("TestRailConfig", "ProjectName");
                return ProjectName;
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error("Unable to get the Project Name from the Config " + ex);
            }
            return null;
        }
        /// <summary>
        /// Method for getting the Suite Name from the COnfiguration file
        /// </summary>
        /// <returns></returns>
        public string GetSuiteName()
        {
            string SuiteName = _autoutilities.GetKeyValue("TestRailConfig", "SuiteName");
            return SuiteName;
        }


        /// <summary>
        /// Method for all the Projects Name and their unique IDs for TestRail Client.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAllProjects()
        {
            Gurock.TestRail.APIClient client = GetTestRailClientConfig();
            Dictionary<string, string> AllProjects = new Dictionary<string, string>();
            try
            {
                JArray response = (JArray)client.SendGet("get_projects");
                for (int i = 0; i < response.LongCount(); i++)
                {
                    JObject project = (JObject)response.ElementAt(i);
                    AllProjects.Add(project.GetValue("id").ToString(), project.GetValue("name").ToString());
                }
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error(ex);

            }
            return AllProjects;

        }

        /// <summary>
        /// Method for getting all the TestRuns for Certain ProjectID
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>


        public Dictionary<string, string> GetAllRunsForAProject(string ProjectId)
        {
            Gurock.TestRail.APIClient client = GetTestRailClientConfig();
            Dictionary<string, string> AllRuns = new Dictionary<string, string>();
            try
            {
                JArray response = (JArray)client.SendGet("get_runs/" + ProjectId + "");
                for (int i = 0; i < response.LongCount(); i++)
                {
                    JObject Run = (JObject)response.ElementAt(i);
                    AllRuns.Add(Run.GetValue("id").ToString(), Run.GetValue("name").ToString());
                }
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error(ex);
            }
            return AllRuns;

        }

        /// <summary>
        /// Method for Triggering the TestRun creation and capturing the RunID 
        /// </summary>
        /// <returns></returns>

        public string GetIdForGeneratedRun()
        {

            string NameOfGeneratedRun = AddTestRun();
            string ProjectId = GetProjectID();
            Dictionary<string, string> AllRuns = GetAllRunsForAProject(ProjectId);
            try
            {
                foreach (KeyValuePair<string, string> entry in AllRuns)
                {
                    if (entry.Value.Equals(NameOfGeneratedRun))
                    {
                        return entry.Key;
                    }

                }
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error(ex);
            }
            return "";
        }

        /// <summary>
        /// Method for getting the Project ID for respective Project Name
        /// </summary>
        /// <returns></returns>
        public string GetProjectID()
        {
            string NameOfProject = GetProjectName();
            Dictionary<string, string> AllProjects = GetAllProjects();
            try
            {
                foreach (KeyValuePair<string, string> entry in AllProjects)
                {
                    if (entry.Value.Equals(NameOfProject))
                    {
                        return entry.Key;
                    }

                }
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error(ex);
            }
            return "";
        }

        /// <summary>
        /// Method for getting all the Suites in respective Project
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAllSuites()
        {
            string ProjectId = GetProjectID();
            Gurock.TestRail.APIClient client = GetTestRailClientConfig();
            Dictionary<string, string> AllSuites = new Dictionary<string, string>();
            try
            {
                JArray response = (JArray)client.SendGet("get_suites/" + ProjectId + "");
                for (int i = 0; i < response.LongCount(); i++)
                {
                    JObject Suite = (JObject)response.ElementAt(i);
                    AllSuites.Add(Suite.GetValue("id").ToString(), Suite.GetValue("name").ToString());

                }
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error(ex);

            }
            return AllSuites;

        }

        /// <summary>
        /// Method for Getting the Respective Suite Id to be used in creation of TestRun
        /// </summary>
        /// <returns></returns>
        public string GetSuiteId()
        {
            string NameOfSuite = GetSuiteName();
            Dictionary<string, string> AllSuites = GetAllSuites();
            try
            {
                foreach (KeyValuePair<string, string> entry in AllSuites)
                {
                    if (entry.Value.Equals(NameOfSuite))
                    {
                        return entry.Key;
                    }

                }
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error(ex);
            }
            return "";
        }

        /// <summary>
        /// Method for getting the Run ID for the Test Rail Client ,We can re-use the existing Run ID 
        /// or Else We can create a new Run_ID if we want to Generate a fresh RunId
        /// </summary>
        /// <returns></returns>
        public string GetRunId()
        {
            string ReUseRun = WhetherUseExistingRun();
            if (ReUseRun.Equals(""))
            {
                return GetIdForGeneratedRun();
            }

            else
            {
                return ReUseRun;
            }
        }


        /// <summary>
        /// Method for Checking Whether we want to Re-use th existing Test Run or Create a new One
        /// </summary>
        /// <returns></returns>
        public string WhetherUseExistingRun()
        {
            string RunIDToUse = null;
            string valueForReUseRun = _autoutilities.GetKeyValue("TestRailConfig", "ReUseTestRun");
            try
            {
                if (valueForReUseRun.Equals("true"))
                {
                    RunIDToUse = GetRunIdForTestRail();
                }

                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error(ex);
            }
            return RunIDToUse;
        }


        /// <summary>
        /// Method for getting all the MileStones for a specific Project
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAllMilestones()
        {

            string ProjectId = GetProjectID();
            Gurock.TestRail.APIClient client = GetTestRailClientConfig();
            Dictionary<string, string> milestones = new Dictionary<string, string>();
            JArray response = null;
            try
            {
                response = (JArray)client.SendGet("get_milestones/" + ProjectId);
                for (int i = 0; i < response.LongCount(); i++)
                {
                    JObject suite = (JObject)response.ElementAt(i);
                    milestones.Add(suite.GetValue("id").ToString(), suite.GetValue("name").ToString());
                }
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error(ex);
            }
            return milestones;

        }

        /// <summary>
        /// Method for getting all the RunIDs for a specific Project and Specific to Milestone
        /// </summary>
        /// <param name="MilestoneId"></param>
        /// <returns></returns>
        public List<string> GetRunIdsForProjectAndMileStone(String MilestoneId)
        {

            string ProjectId = GetProjectID();
            Gurock.TestRail.APIClient client = GetTestRailClientConfig();
            JArray response = null;
            List<string> runs = new List<string>();

            try
            {
                response = (JArray)client.SendGet("get_runs/" + ProjectId);
                for (int i = 0; i < response.LongCount(); i++)
                {
                    JObject sections = (JObject)response.ElementAt(i);
                    Object milestone_id = sections.GetValue("milestone_id");
                    if (milestone_id != null)
                    {
                        if (sections.GetValue("milestone_id").ToString().Equals(MilestoneId))
                        {
                            runs.Add(sections.GetValue("id").ToString());
                            continue;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error(ex);
            }
            return runs;
        }

        /// <summary>
        /// Method for getting all the Test Case IDs for a particular Test Run
        /// </summary>
        /// <param name="RunId"></param>
        /// <returns></returns>
        public List<String> getCaseIdsForRun(String RunId)
        {

            Gurock.TestRail.APIClient client = GetTestRailClientConfig();
            List<String> caseIds = new List<String>();
            JArray response = null;
            try
            {
                response = (JArray)client.SendGet("get_tests/" + RunId);
                for (int i = 0; i < response.LongCount(); i++)
                {
                    JObject obj = (JObject)response.ElementAt(i);
                    caseIds.Add(obj.GetValue("case_id").ToString());
                }
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error(ex);
            }
            return caseIds;
        }


        /// <summary>
        /// Method for getting the suite names from the configuration file
        /// </summary>
        /// <returns></returns>
        public string GetSuiteNamesFromConfig()
        {
            try
            {
                string NamesOfSuites = _autoutilities.GetKeyValue("TestRailConfig", "SuiteName");
                return NamesOfSuites;
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error("Unable to get the Suite names from Config file "+ ex);
            }
            return null;
        }


        /// <summary>
        /// Method for getting all the suite names from config file and then put all the Suites in a List 
        /// to iterate over it 
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllSuiteIds()
        {

            List<string> SuiteNames = GetSuiteNamesFromConfig().Split(',').ToList<string>();
            Dictionary<string, string> allSuites = GetAllSuites();
            List<string> AllSuiteIds = new List<string>();
            try
            {
                foreach (string suites in SuiteNames)
                {
                    _testRailUtilsLog.Info("=================>getting id for suite:=>" + suites);
                    foreach (KeyValuePair<string, string> entry in allSuites)
                    {
                        if (entry.Value.Equals(suites))
                        {
                            _testRailUtilsLog.Info("====>matched for suite:=>" + suites);
                            AllSuiteIds.Add(entry.Key);

                        }


                    }

                }
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error(ex);
            }
            return AllSuiteIds;
        }

        /// <summary>
        /// Method for getting all the Test Cases for a Project for all the suites 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAllTestCasesFromAllSuites()
        {

            Gurock.TestRail.APIClient client = GetTestRailClientConfig();
            string ProjectId = GetProjectID();
            List<string> SuiteIds = GetAllSuiteIds();
            Dictionary<string, string> AllTestCases = new Dictionary<string, string>();
            try
            {
                foreach (string SuiteId in SuiteIds)
                {
                    _testRailUtilsLog.Info("===fetching test cases for suite:=>" + SuiteId);
                    JArray response = (JArray)client.SendGet("get_cases/" + ProjectId + "&suite_id=" + SuiteId);
                    for (int i = 0; i < response.LongCount(); i++)
                    {
                        JObject Case = (JObject)response.ElementAt(i);
                        AllTestCases.Add(Case.GetValue("id").ToString(), Case.GetValue("title").ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error(ex);
            }

            return AllTestCases;
        }

        /// <summary>
        /// Method for fetching the MileStoneID from the Config file which is to be re-used 
        /// </summary>
        /// <returns></returns>
        public string GetMileStoneId()
        {
            try
            {
                string MileStoneId = _autoutilities.GetKeyValue("TestRailConfig", "MileStoneId");
                return MileStoneId;
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error("Unable to get the Milestone ID : "+ ex);
            }
            return null;
        }

        /// <summary>
        /// Method for adding new MileStone to the project specfied in the Config File
        /// </summary>
        public string AddNewMileStone()
        {
            try
            {
                Gurock.TestRail.APIClient client = GetTestRailClientConfig();
                Dictionary<string, object> data = new Dictionary<string, object>();
                string MileStoneName = GenerateTestRunName("MileStone--");
                string Project_Id = GetProjectID();
                data.Add("name", MileStoneName);
                string SuiteId = GetSuiteId();
                data.Add("include_all", 1);
                data.Add("suite_id", SuiteId);
                client.SendPost("add_milestone/" + Project_Id, data);//2
                _testRailUtilsLog.Info("Added New Milestone for Project ID : " + Project_Id + " Named as : " + MileStoneName);
                return MileStoneName;
            }

            catch (Exception ex)
            {
                _testRailUtilsLog.Error("Unable to add new Milestone " + ex);
                return "";
            }
        }

        /// <summary>
        /// Method for Getting the dynamically generated MileStoneID
        /// </summary>
        /// <returns></returns>
        public string GetGeneratedMileStoneID()
        {
            string NameOfGeneratedMileStone = AddNewMileStone();
            Dictionary<string, string> AllMileStones = GetAllMilestones();
            try
            {
                foreach (KeyValuePair<string, string> entry in AllMileStones)
                {
                    if (entry.Value.Equals(NameOfGeneratedMileStone))
                    {
                        return entry.Key;
                    }

                }
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error(ex);
                _testRailUtilsLog.Error("Unable to get the Generated Milestone ID");
            }
            return "";
        }

        /// <summary>
        /// Method for fetching the Re-usblity status for existing MileStone
        /// </summary>
        /// <returns></returns>
        public string GetMileStoneReusablityStatus()
        {
            string MileStoneReusablity;
            try
            {
                MileStoneReusablity = _autoutilities.GetKeyValue("TestRailConfig", "ReUseMileStone");
                return MileStoneReusablity;
                
            }
            catch (Exception ex)
            {
                _testRailUtilsLog.Error("Unable to read the MileStone usage status from config file..So returning null value");
                _testRailUtilsLog.Error(ex);
                return null;
            }
            
        }

        /// <summary>
        /// Method for getting the Milestone ID (either for reusing MilestoneID or for getting newly 
        /// generated MileStoneID)
        /// </summary>
        /// <returns></returns>
        public string GetMileStone()
        {
            string MileStoneReusablity = GetMileStoneReusablityStatus();
            if (MileStoneReusablity.Equals("true"))
            {
                return GetMileStoneId();
            }
            else if (MileStoneReusablity.Equals("false"))
            {
                return GetGeneratedMileStoneID();
            }
            return "";
        }

    }
}
