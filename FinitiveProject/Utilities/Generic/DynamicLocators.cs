using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAtmosphere.Utilities.Generic
{
    class DynamicLocators
    {
        /**
         * Get dynamic locator resolved to normal one
         * @param locator - locator that needs to be replaced
         * @param dynamicText - text that is dynamic in the locator
         * @return By - new locator after placing required text in the locator string
         */

        public static By getNewLocator(By locator, string dynamicText)
        {
            string locatorType = locator.ToString().Split(new[] { ":" }, StringSplitOptions.None)[0].Split("\\.".ToCharArray())[1];
            string[] c = locator.ToString().Split(new[] { ":" }, StringSplitOptions.None);
            string newLocatorString = string.Format(locator.ToString().Split(new[] { ":" }, StringSplitOptions.None)[1], dynamicText);
            switch (locatorType)
            {
                case "XPath":
                    locator = By.XPath(newLocatorString);
                    break;
                case "CssSelector":
                    locator = By.CssSelector(newLocatorString);
                    break;
                case "Id":
                    locator = By.Id(newLocatorString);
                    break;
                case "ClassName":
                    locator = By.ClassName(newLocatorString);
                    break;
                case "Name":
                    locator = By.Name(newLocatorString);
                    break;
                case "LinkText":
                    locator = By.LinkText(newLocatorString);
                    break;
                case "PartialLinkText":
                    locator = By.PartialLinkText(newLocatorString);
                    break;
                case "TagName":
                    locator = By.TagName(newLocatorString);
                    break;
            }
            return locator;
        }

        /**
        * Get dynamic locator resolved to normal one
        * @param locator - locator that needs to be replaced
        * @param dynamicText - text that is dynamic in the locator
        * @return By - new locator after placing required text in the locator string
        */

        public static By getNewLocator(By locator, string[] dynamicText)
        {
            string locatorType = locator.ToString().Split(new[] { ":" }, StringSplitOptions.None)[0].Split("\\.".ToCharArray())[1];
            string[] c = locator.ToString().Split(new[] { ":" }, StringSplitOptions.None);

            string newLocatorString = locator.ToString().Split(new[] { ":" }, StringSplitOptions.None)[1];

            //for(int i=0;i<dynamicText.Length;i++)
            newLocatorString = string.Format(newLocatorString, dynamicText);
          
            switch (locatorType)
            {
                case "XPath":
                    locator = By.XPath(newLocatorString);
                    break;
                case "CssSelector":
                    locator = By.CssSelector(newLocatorString);
                    break;
                case "Id":
                    locator = By.Id(newLocatorString);
                    break;
                case "ClassName":
                    locator = By.ClassName(newLocatorString);
                    break;
                case "Name":
                    locator = By.Name(newLocatorString);
                    break;
                case "LinkText":
                    locator = By.LinkText(newLocatorString);
                    break;
                case "PartialLinkText":
                    locator = By.PartialLinkText(newLocatorString);
                    break;
                case "TagName":
                    locator = By.TagName(newLocatorString);
                    break;
            }
            return locator;
        }
	
    }
}
