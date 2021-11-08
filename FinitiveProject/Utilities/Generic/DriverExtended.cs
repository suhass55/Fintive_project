using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace SeleniumAutomation.Selenium
{
    public static class DriverExtended
    {
        private static readonly ILog Log = LogManager.GetLogger("DriverExtended");

        /// <summary>
        /// sets implicit wait
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="seconds"></param>
        public static void SetImplicitWait(this IWebDriver driver, int seconds)
        {
            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
                //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
            }
            catch (UnhandledAlertException e)
            {
                Log.Info("Alert displayed is " + e.AlertText);
                driver.SwitchTo().Alert().Accept();
            }
            catch (Exception e)
            {
                Log.Error("unable to set implicit wait due to " + e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <param name="waitInSeconds"></param>
        public static IWebElement FindElement(this IWebDriver driver, By locator, int waitInSeconds = 60)
        {
            driver.SetImplicitWait(0);
            try
            {
                Log.Info("Waiting for element: " + locator + " for " + waitInSeconds + " seconds");
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitInSeconds));
                return wait.Until(ExpectedConditions.ElementExists((locator)));
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Element - " + locator + " was not found within time " + waitInSeconds + " seconds");
            }
            catch (UnhandledAlertException e)
            {
                Log.Info("Alert displayed is " + e.AlertText);
                driver.SwitchTo().Alert().Accept();
            }
            driver.SetImplicitWait(TimeOuts.IMPLICITWAIT);
            return null;
        }



    }
}
