using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;

namespace Finitive.API
{
    public class AzureDevopsAPI
    {
        public string DevopsURL;
        public string ProjectName;
        public string PAT;
        public AzureDevopsAPI(string _devopsURL,string _projectName, string _patToken)
        {
            this.DevopsURL = _devopsURL;
            this.ProjectName = _projectName;
            this.PAT = _patToken;
        }

        public void UpdateTheOutcomeInTestPlan(string planID, string testSuiteID, string testcaseID, string outcome)
        {
            try
            {
                string testPointID = GetTheTestPoint(planID, testSuiteID, testcaseID);
                RestClient restClient = new RestClient();
                restClient.BaseUrl = new Uri(DevopsURL + "/" + ProjectName + "/_apis/test/Plans/" + planID + "/Suites/" + testSuiteID + "/points/" + testPointID + "?api-version=6.0");
                string locationJSON = "{\"outcome\": \"" + outcome + "\"}";
                RestRequest restRequest = new RestRequest(Method.PATCH);
                restRequest.AddParameter("application/json", locationJSON, ParameterType.RequestBody);
                restClient.Authenticator = new HttpBasicAuthenticator("RAMESH UPPULURI", PAT);
                IRestResponse restResponse = restClient.Execute(restRequest);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public string GetTheTestPoint(string planID, string testSuiteID, string testcaseID)
        {
            try
            {
                RestClient restClient = new RestClient();
                restClient.BaseUrl = new Uri(DevopsURL + "/"+ ProjectName + "/_apis/test/plans/" + planID + "/suites/" + testSuiteID + "/points/?api-version=6.0&testCaseId=" + testcaseID + " ");
                RestRequest restRequest = new RestRequest(Method.GET);
                restClient.Authenticator = new HttpBasicAuthenticator("RAMESH UPPULURI", PAT);
                IRestResponse restResponse = restClient.Execute(restRequest);
                var jObject = JObject.Parse(restResponse.Content);
                string id = jObject.SelectToken("value[0].id").ToString();
                return id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


    }
}
