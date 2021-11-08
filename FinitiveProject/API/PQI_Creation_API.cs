using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Finitive.API
{
    public class PQI_Creation_API : IPQI_Creation_API
    {

        public const string tokenUrl = "https://uat.finitive.com/services/oauth2/token";
        private string serviceUrl;
        private string token;
        HttpClient sfClient;

        public PQI_Creation_API(string clientId, string clientSecret, string username, string password)
        {
            sfClient = new HttpClient();
            createSession(clientId, clientSecret, username, password);
        }

        public bool createSession(string clientId, string clientSecret, string username, string password)
        {

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                 {"grant_type", "password"},
                        {"client_id", clientId},
                        {"client_secret", clientSecret},
                        {"username", username},
                        {"password", password}
            });

            // string requestMessage = tokenObject.ToString();
            //HttpRequestMessage request = new HttpRequestMessage();
            // HttpContent content = new StringContent(requestMessage, Encoding.UTF8, "application/json");
            //request.Content = content;
            HttpResponseMessage message = sfClient.PostAsync(tokenUrl, content).Result;
            string responseString = message.Content.ReadAsStringAsync().Result;
            if (message.IsSuccessStatusCode)
            {
                JObject obj = JObject.Parse(responseString);
                token = (string)obj["access_token"];
                serviceUrl = (string)obj["instance_url"] + "/services/data/v34.0/";
                //serviceUrl = (string)obj["instance_url"];
                return true;
            }
            else
            {
                return false;
            }
        }

        public HttpResponseMessage createSObject(string sObjectApiName, JObject sObject)
        {
            return createSObjects(sObjectApiName, new JArray { sObject });
        }

        public HttpResponseMessage createSObjects(string sObjectApiName, JArray sObjectList)
        {
            JObject object123 = new JObject
            {
                {   "records", sObjectList },

            };
            string requestMessage = object123.ToString();
            string uri = serviceUrl + "composite/tree/" + sObjectApiName;
            return apiRequest("POST", uri, requestMessage);
        }

        public HttpResponseMessage addAttachment(string filePath, string parentId)
        {
            string fileString = File.ReadAllText(filePath);
            string fileName = Path.GetFileName(filePath);
            JObject requestObject = new JObject {
                { "Name", fileName },
                { "Body", fileString },
                { "parentId", parentId }
            };
            string requestMessage = requestObject.ToString();
            string uri = serviceUrl + "sobjects/attachment";
            return apiRequest("POST", uri, requestMessage);
        }

        public HttpResponseMessage getSObject(string sObjectApiName, string id)
        {
            string uri = serviceUrl + string.Format("sobjects/{0}/{1}", sObjectApiName, id);
            return apiRequest("GET", uri);
        }

        public HttpResponseMessage querySObjects(string urlQuery)
        {
            string restQuery = serviceUrl + string.Format("query?q={0}", Uri.EscapeDataString(urlQuery));
            return apiRequest("PATCH", restQuery);
        }

        public HttpResponseMessage updateSObject(string sObjectApiName, string id, JObject sObject)
        {
            string requestMessage = sObject.ToString();
            string uri = serviceUrl + string.Format("sobjects/{0}/{1}", sObjectApiName, id);
            return apiRequest("PATCH", uri, requestMessage);
        }
        public HttpResponseMessage deleteSObject(string sObjectApiName, string id)
        {
            string uri = serviceUrl + string.Format("sobjects/{0}/{1}", sObjectApiName, id);
            return apiRequest("DELETE", uri);
        }

        private HttpResponseMessage apiRequest(string httpMethod, string uri, string contentString = null)
        {
            sfClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod(httpMethod), uri);
            request.Headers.Add("Authorization", "Bearer " + token);
            if (!String.IsNullOrEmpty(contentString))
            {
                request.Content = new StringContent(contentString, Encoding.UTF8, "application/json");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            sfClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            sfClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = sfClient.SendAsync(request).Result;

            var responseObj = response.Content.ReadAsStringAsync();

            return response;

        }
    }
}
