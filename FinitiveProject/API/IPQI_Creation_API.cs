using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Finitive.API
{
    public interface IPQI_Creation_API
    {

        /// <summary>
        /// If a 401 is received then the token has expired. Another createSession needs to be called.
        /// This flow can be upgraded to use refresh tokens with salesforce.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="username"></param>
        /// <param name="Ace7373!!!!!"></param>
        /// <returns></returns>
        bool createSession(string clientId, string clientSecret, string username, string password);
        HttpResponseMessage querySObjects(string urlQuery);
        HttpResponseMessage getSObject(string sObjectApiName, string id);
        HttpResponseMessage createSObject(string sObjectApiName, JObject sObject);
        /// <summary>
        /// Limit of 200 records per request
        /// </summary>
        /// <param name="sObjectApiName"></param>
        /// <param name="sObjectList"></param>
        /// <returns></returns>
        HttpResponseMessage createSObjects(string sObjectApiName, JArray sObjectList);
        HttpResponseMessage updateSObject(string sObjectApiName, string id, JObject sObject);
        HttpResponseMessage deleteSObject(string sObjectApiName, string id);
        HttpResponseMessage addAttachment(string filePath, string parentId);
    }
}
