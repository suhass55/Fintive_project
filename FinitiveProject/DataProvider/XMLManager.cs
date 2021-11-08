using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SeleniumAutomation.Utilities;

namespace SeleniumAutomation.DataProvider
{
    class XMLManager
    {
        public static void WriteSetting(string key, string value)
        {
            
             /// load config document for current assembly 
             
            XmlDocument doc = loadConfigDocument();

           
             /// retrieve appSettings node 
             
            XmlNode node = doc.SelectSingleNode("//appSettings");
            if (node == null)
            {
                throw new InvalidOperationException("appSettings section not found in config file.");
            }
            try
            {
                
                 /// select the 'add' element that contains the key 
                
                XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));
                if (elem != null)
                {
                    
                     /// add value for key 
                     
                    elem.SetAttribute("value", value);
                }
                else
                {
                    /// key was not found so create the 'add' element 
                    ///  and set it's key/value attributes 
                     
                    elem = doc.CreateElement("add");
                    elem.SetAttribute("key", key);
                    elem.SetAttribute("value", value);
                    node.AppendChild(elem);
                }
                doc.Save(getConfigFilePath());
            }
            catch
            {
                throw;
            }
        }

         /// <summary>
        /// Method for loading Config Document for current assembly
        /// </summary>
        /// <params>None</params>
        /// <return>XMLDocument reference</returns>
   
        public static XmlDocument loadConfigDocument()
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(getConfigFilePath());
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
        }
        
         /// <summary>
        ///  Method for getting the path of the config file ie. app.config
        /// </summary>
        /// <params>None</params>
        /// <return>String</returns>
    
        public static string getConfigFilePath()
        {
            return new AutomationUtilities().getConfigFilePath();
        }
    }
}
