using System;
using System.Linq;
using System.Threading;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAutomation.Base;
using SeleniumAutomation.Selenium;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace SeleniumAutomation.Utilities
{
    public class Sync:BaseClass
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger("Sync");
		//public IWebDriver Driver;
		 public IJavaScriptExecutor JsDriver;
        /*
         * Method : Synchronization purpose - Waits until element is loaded or time exprire (checks every 250 milliseconds for the element to appear)
         * Params : Driver, Element & Time(in seconds)
         * Returns: True when element is located within time constrain
         */
        public Sync(){
        	
        }
          public Sync(IWebDriver Driver){
        	this.Driver=Driver;
        	this.JsDriver=Driver as IJavaScriptExecutor;
        }
        
        public bool IsElementPresent(By locator, int waitInSeconds = 10)
        {
            Driver.SetImplicitWait(0);
            try
            {
                Log.Info("Waiting for element: "+locator+" for "+waitInSeconds+" seconds");
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(waitInSeconds));
                IWebElement webelement = wait.Until(ExpectedConditions.ElementExists((locator)));
                Driver.SetImplicitWait(TimeOuts.IMPLICITWAIT); 
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                Driver.SetImplicitWait(TimeOuts.IMPLICITWAIT); 
                return false;   
            }
            catch (UnhandledAlertException e)
            {
                Log.Info("Alert displayed is " + e.AlertText);
                Driver.SwitchTo().Alert().Accept();
                Driver.SetImplicitWait(TimeOuts.IMPLICITWAIT); 
                return false;
            }
            catch (Exception)
            {
                Driver.SetImplicitWait(TimeOuts.IMPLICITWAIT); 
                return false;
            }                      
        }


        /*
         * Method : Synchronization purpose - Waits until element is loaded or time exprire
         * Params : Driver, Element & Time(in seconds)
         * Returns: Element when element is located within time constrain
         */

        public bool IsElementPresentIW(By locator)
        {
            try
            {
                Driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        public IWebElement WaitUntilElementIsDisplayed(By locator, int timeoutInSeconds = 120)
        {
            Driver.SetImplicitWait(0);
            try
            {
                Log.Info("Waiting until element: " + locator + " is displayed.. timeout is " + timeoutInSeconds + " seconds");
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                IWebElement webelement = wait.Until(ExpectedConditions.ElementIsVisible((locator)));
                Driver.SetImplicitWait(TimeOuts.IMPLICITWAIT);
                return webelement;
            }
            catch (WebDriverTimeoutException)
            {
                Log.Error("Timed out waiting for element - " + locator + " time waited was - "+timeoutInSeconds);
                Driver.SetImplicitWait(TimeOuts.IMPLICITWAIT);
                return null;
            }
            catch (UnhandledAlertException e)
            {
                Log.Info("Alert displayed is " + e.AlertText);
                Driver.SetImplicitWait(TimeOuts.IMPLICITWAIT);
                return null;
                //(new UA(Driver)).safeAcceptAlertPresent();
            }
                        
        }
        
         public IWebElement WaitUntilElementIsExist(By locator, int timeoutInSeconds = 200)
        {
            Driver.SetImplicitWait(0);
            try
            {
                Log.Info("Waiting until element: " + locator + " is Exist.. timeout is " + timeoutInSeconds + " seconds");
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                IWebElement webelement = wait.Until(ExpectedConditions.ElementExists((locator)));
                Driver.SetImplicitWait(TimeOuts.IMPLICITWAIT);
                return webelement;
            }
            catch (WebDriverTimeoutException)
            {
                Log.Error("Timed out waiting for element - " + locator + " time waited was - "+timeoutInSeconds);
                Driver.SetImplicitWait(TimeOuts.IMPLICITWAIT);
                return null;
            }
            catch (UnhandledAlertException e)
            {
                Log.Info("Alert displayed is " + e.AlertText);
                Driver.SetImplicitWait(TimeOuts.IMPLICITWAIT);
                return null;
                //(new UA(Driver)).safeAcceptAlertPresent();
            }                        
        }


        /*
         * Method : Verifies whether the element is currently visible on the screen or not
         * Params : WebDriver instance, locator of the element to be verified
         * Returns: true or false
         */

        public bool IsElementDisplayed(By bylocator,int TimeOutInSeconds=30)
        {
            try
            {
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TimeOutInSeconds);
                IWebElement element = Driver.FindElement(bylocator);
                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }



        public void WaitUntilElementDisappears(By locator, int timeoutInSeconds = 200)
        {
            Driver.SetImplicitWait(0);
            Log.Info("Waiting until element: " + locator + " is not visible on screen.. timeout is " + timeoutInSeconds + " seconds");
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
            try
            {
                wait.Until(d =>
                {
                    try
                    {
                        if (IsElementPresent(locator, TimeOuts.VERYSHORTWAIT))
                            return !(Driver.FindElement(locator).Displayed);
                        else
                            return true;
                    }
                    catch (NoSuchElementException) // Element not found
                    {              
                        return true;
                    }
                });
            }
            catch (WebDriverTimeoutException) 
            { 
                Assert.Fail("Element did not disappear after timeout"); 
            }
            catch (UnhandledAlertException e)
            {
                Log.Info("Alert displayed is " + e.AlertText);
                Driver.SwitchTo().Alert().Accept();
            }
            catch (WebDriverException e)
            {
                Log.Error("Exception.. " + e.Message + e.StackTrace);
            }
            Driver.SetImplicitWait(TimeOuts.IMPLICITWAIT);
        }


        /*
         * Method : Verifies whether the element is currently enable on the screen or not
         * Params : WebDriver instance, locator of the element to be verified
         * Returns: true or false
         */

        public bool IsElementEnabled(By bylocator)
        {
            try
            {
                return Driver.FindElement(bylocator).Enabled;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        /*
         * Method : Wait until the element get disable if element exist
         * Params : WebDriver instance, locator of the element to be verified
         * Returns: true or false
         */
        public void WaitUntilElementIsDisabled(By locator, int timeoutInSeconds = 200)
        {
            var wait = timeoutInSeconds * 1000;
            var y = (wait / 250);
            var sw = new System.Windows.Forms.Timer();
            sw.Start();

            for (var x = 0; x < y; x++)
            {
                if (sw.Interval > wait) Assert.Fail("Timeout: Element still enable at: " + locator);
                try
                {
                    if (!IsElementEnabled(locator)) break;
                }
                catch (NoSuchElementException)
                {
                    break;
                }
                Thread.Sleep(250);
            }
        }


        /*
         * Method : Wait until the element get enable if element exist
         * Params : WebDriver instance, locator of the element to be verified
         * Returns: true or false
         */
        public void WaitUntilElementIsEnabled(By locator, int timeoutInSeconds = 200)
        {
            var wait = timeoutInSeconds * 1000;
            var y = (wait / 250);
            var sw = new System.Windows.Forms.Timer();
            sw.Start();

            for (var x = 0; x < y; x++)
            {
                if (sw.Interval > wait) Assert.Fail("Timeout: Element not enable at: " + locator);
                try
                {
                    if (!IsElementEnabled(locator)) break;
                }
                catch (NoSuchElementException)
                {
                    break;
                }
                Thread.Sleep(250);
            }
        }


        /*
         * Method : Synchronization purpose - Waits until page gets loaded
         * Params : Driver
         * Returns: Void
         */
        public void WaitForPageToLoad(int timeoutInSeconds = 200)
        {
            Log.Info("Waiting until page load is complete..");
            TimeSpan timeout = new TimeSpan(0, 0, timeoutInSeconds);
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, timeout);
                if (JsDriver.Equals(null))
                    throw new ArgumentException("Driver", "Driver must support javascript execution");

                wait.Until((d) =>
                {
                    try
                    {
                        string sReadyState = JsDriver.ExecuteScript("if (document.readyState) return document.readyState;").ToString();
                        return sReadyState.ToLower() == "complete";
                    }
                    catch (InvalidOperationException e)
                    {
                        return e.Message.ToLower().Contains("unable to get browser");
                    }
                    catch (UnhandledAlertException e)
                    {
                        Log.Info("Alert displayed is " + e.AlertText);
                        Driver.SwitchTo().Alert().Accept();
                        return false;
                    }
                    catch (WebDriverException e)
                    {
                        return e.Message.ToLower().Contains("unable to connect");
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });
            }
            catch (WebDriverTimeoutException e)
            {
                Log.Error("Page load timed out.. " + e.Message + e.StackTrace);
                Assert.Fail("Page load timed out.. " + e.Message + e.StackTrace);
            }
        }


        public bool WaitForJQueryProcessing(int timeOutInSeconds = 200)
        {
            Log.Info("Waiting ajax processing to complete..");
            Driver.SetImplicitWait(0);
            bool jQcondition = false;
            try
            {
                Thread.Sleep(500);
                TimeSpan timeout = new TimeSpan(0, 0, timeOutInSeconds);
                WebDriverWait wait = new WebDriverWait(Driver, timeout);
                IJavaScriptExecutor javascript = Driver as IJavaScriptExecutor;
                wait.Until((d) =>
                {
                    return (Boolean)(javascript).ExecuteScript("return jQuery.active == 0");
                });
                jQcondition = (Boolean)(javascript).ExecuteScript("return window.jQuery != undefined && jQuery.active === 0");
                Driver.SetImplicitWait(TimeOuts.IMPLICITWAIT);
                return jQcondition;            
            }
            catch (UnhandledAlertException e)
            {
                Log.Info("Alert displayed is " + e.AlertText);
                //Driver.SwitchTo().Alert().Accept();
            }
            catch (Exception e)
            {
                Log.Info("Waiting stopped due to exception.."+e.Message);
            }
            Driver.SetImplicitWait(TimeOuts.IMPLICITWAIT);
            return jQcondition;
        }
         /*
		 * Method : Wait until the element is exist
		 * Params : Webdriver instance, locator of the element to be verified
		 * Returns: IWebElement or null
		 */
		public IWebElement WebDriverWaitElementExists( By locator, int waitInSeconds){
			try{
				WebDriverWait wait=new WebDriverWait(Driver,TimeSpan.FromSeconds(waitInSeconds));
				IWebElement element=wait.Until(ExpectedConditions.ElementExists((locator)));
				return element;
			}catch(WebDriverTimeoutException){
				return null;
			}
		}
		/*
		 * Used to induce forced wait 
		 * */
		public void waitForTime(int waitInSeconds){
			int wait = waitInSeconds * 1000;
			try{
				Thread.Sleep(wait);
			}
			catch(ThreadInterruptedException e){
				Log.Info("Thread interrupted" + e.Message);
			}
		}
		
		/*
		 * Method : Wait until the element is visible if elemnet exist
		 * Params : Webdriver instance, locator of the element to be verified
		 * Returns: IWebElement or null
		 */
		public IWebElement WebDriverWaitElementIsVisible(By locator, int waitInSeconds){
			try{
				WebDriverWait wait=new WebDriverWait(Driver,TimeSpan.FromSeconds(waitInSeconds));
				IWebElement element=wait.Until(ExpectedConditions.ElementIsVisible((locator)));
				return element;
			}catch(WebDriverTimeoutException){
				return null;
			}
		}
        public void SwitchToLastTab()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            Driver.SwitchTo().DefaultContent();
            WaitForPageToLoad();
        }

        public void SwitchToFirstTabAndCloseRemaninig()
        {
            var tabs = Driver.WindowHandles; // 

            foreach (var tab in tabs)
            {
                // "tab" is a string like "CDwindow-6E793DA3E15E2AB5D6AE36A05344C68"
                if (tabs[0] != tab)
                {
                    Driver.SwitchTo().Window(tab);
                    Driver.Close();
                }
            }

            Driver.SwitchTo().Window(tabs[0]); // Switch to first tab
            Driver.SwitchTo().DefaultContent(); // Switch to default frame

        }
        

    }

}
