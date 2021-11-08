using System;
using System.Configuration;
using OpenQA.Selenium;
using Selenium;

namespace SeleniumAutomation.Utilities
{	
	public class BackedSelenium
	{
		public IWebDriver driver;
		public BackedSelenium(IWebDriver driver)
        
		{
			this.driver = driver;
		}
		/*
         * Params - Webdriver
         * Return Type - Selenium RC instance
         * Description - This method will take Webdriver instance as parameter and returns Selenium RC as return type. This 
         * 				 Selenium RC instance can later be used to work with Selenium RC methods and properties. 
         */
		public ISelenium Initial_Setup_SeleniumOne()
        {
			ISelenium sel = new WebDriverBackedSelenium(driver,ConfigurationManager.AppSettings["URL"] );
            sel.Start();
            return sel;
        }
	}
}
