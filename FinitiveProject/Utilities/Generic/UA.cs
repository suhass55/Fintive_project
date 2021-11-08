using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using Gallio.Framework;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumAutomation.Utilities;
using SeleniumAutomation.Base;
using java.awt;
using java.awt.@event;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace SeleniumAutomation.Selenium
{
    public class UA : Sync
    {
        private log4net.ILog UALog = log4net.LogManager.GetLogger("UA");
       // private IWebDriver Driver;
       // private IJavaScriptExecutor JsDriver;
        private string testName = TestContext.CurrentContext.Test.Name.ToString();
        AutomationUtilities _autoUtils;
        public BaseClass _baseclass;
        public string status = "Failed";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="timeOutInSeconds"></param>
        public UA() { }
        public UA(IWebDriver driver)
            : base(driver)
        {
            this.Driver = driver;
            JsDriver = driver as IJavaScriptExecutor;
            _autoUtils = new AutomationUtilities();
        }
        public void JavaScriptSafeClick(By locator, int timeOutInSeconds = 100, string element_name = "")
        {
            try
            {
                SetHighlight(locator);
                ScrollIntoView(locator);
                JsDriver.ExecuteScript("arguments[0].click();", Driver.FindElement(locator, timeOutInSeconds));
                UALog.Info("Clicked on element " + locator);
            }
            catch (NoSuchElementException e)
            {
                //TestLog.AttachImage("Click here for Screenshot", _autoUtils.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error("Given element " + locator + ". is not Exist - Follow the below exception.. \n" +
                          e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail("Given element " + element_name + "[" + locator + "]. is not Exist - Follow the below exception.. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");


            }
            catch (Exception e)
            {
                //TestLog.AttachImage("Click here for Screenshot", _autoUtils.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error("Unable to click on " + locator + ". element - Follow the below exception.. \n" +
                          e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail("Unable  to click  on " + element_name + "[" + locator.ToString() + "]. Element - Follow the below exception.. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");

            }
        }

        /// <summary>
        /// Prepares and returns webelement with dynamic xpath
        /// </summary>
        /// <param name="xpathValue">Dynamic xpath</param>
        /// <param name="substitutionValue">String to be replaced in xpath</param>


        public By GetNewLocator(By locator, String dynamicText)
        {
            String locatorType = locator.ToString().Split(':')[0].TrimStart().Split('.')[1];
            String newLocatorString = String.Format(locator.ToString().Split(':')[1], dynamicText);
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

        public void JavaScriptSafeType(By locator, string text, int timeOutInSeconds = 60, string element_name = "")
        {
            try
            {
                SetHighlight(locator);
                ScrollIntoView(locator);
                JsDriver.ExecuteScript("arguments[0].innerHTML='" + text + "';", Driver.FindElement(locator, timeOutInSeconds));
                UALog.Info("Entered text " + text + " in the field " + locator);
            }

            catch (Exception e)
            {
                //TestLog.AttachImage("Click here for Screenshot", _autoUtils.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error("Unable to enter text" + locator + ". - Follow the below exception.. \n" +
                          e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail("Unable  to enter text " + element_name + "[" + locator.ToString() + "] - Follow the below exception.. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");

            }
        }

        public void SafeNormalClick(By locator, int timeOutInSeconds = 60, string element_name = "")
        {
            try
            {
                SetHighlight(locator);
                ScrollIntoView(locator);
                Driver.FindElement(locator, timeOutInSeconds).Click();
                UALog.Info("Clicked on element " + locator);



            }
            catch (StaleElementReferenceException)
            {
                SetHighlight(locator);
                ScrollIntoView(locator);
                Driver.FindElement(locator, timeOutInSeconds).Click();
                UALog.Info("Clicked on element " + locator);
            }
            catch (ElementClickInterceptedException)
            {
                ScrollIntoView(locator);
                JsDriver.ExecuteScript("arguments[0].click();", Driver.FindElement(locator, timeOutInSeconds));



            }
            catch (ElementNotInteractableException)
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));
                IWebElement passwordElement = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                Driver.FindElement(locator, timeOutInSeconds).Click();
            }
            catch (NoSuchElementException e)
            {
                //TestLog.AttachImage("Click here for Screenshots", _autoUtils.getScreenshot(Driver));
                _autoUtils.getScreenshot(Driver);
                Assert.Fail("Given element " + element_name + "[" + locator + "]. does not Exist - Follow the below exception.. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");



            }
            catch (Exception e)
            {
                UALog.Error("Unable to click on " + locator + ". element - Follow the below exception.. \n" +
                          e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                _autoUtils.getScreenshot(Driver);
                //TestLog.AttachImage("Click here for Screenshot", _autoUtils.getScreenshot(Driver));
                Assert.Fail("Unable  to click  on " + element_name + "[" + locator.ToString() + "]. Element - Follow the below exception.. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");



            }
        }

        /*
         * Method : Synchronization purpose - Waits until element is loaded before entering any details
         * Params : Driver, locator element & text
         * Returns: null
         */
        public void SafeType(By locator, string text, bool clear = false, int timeOutInSeconds = 30, string element_name = "")
        {
            try
            {
                SetHighlight(locator);
                if (clear)
                    Driver.FindElement(locator, timeOutInSeconds).Clear();
                Driver.FindElement(locator, timeOutInSeconds).SendKeys(text);
                UALog.Info("Entered text " + text + " in the field " + locator);
            }
            catch (NoSuchElementException e)
            {
                _autoutilities.getScreenshot(Driver);
                //TestLog.AttachImage("Click here for Screenshot",_autoutilities.getScreenshot(Driver));
                UALog.Error("Given element " + locator + ". does not Exist - Follow the below exception.. \n" +
                          e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail("Given element " + element_name + "[" + locator + "]. does not Exist - Follow the below exception.. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");


            }
            catch (Exception e)
            {
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error("Unable to enter text" + locator + ". - Follow the below exception.. \n" +
                          e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail("Unable  to enter text " + element_name + "[" + locator.ToString() + "] - Follow the below exception.. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");

            }
        }


        /// <summary>
        ///Method to select a value from the Drop down by specified index value
        /// </summary>
        /// <params>Index of element to be selected in dropdown,FriendlyName,Locator,index and TimeUnit</params>
        /// <return>none</returns>

        public void SafeSelectDropdownByVisibleText(By locator, string ElementNameAndPageName, string VisibleText, string SuiteName, string TestName, int timeOutInSeconds = 60)
        {
            try
            {
                IsElementPresent(locator, timeOutInSeconds);
                //SetHighlight(locator, SuiteName, TestName);
                SelectElement select = new SelectElement(Driver.FindElement(locator));
                select.SelectByText(VisibleText);
            }
            catch (StaleElementReferenceException e)
            {

                UALog.Error("Given element " + ElementNameAndPageName + "with locator:" + locator + " is not attached to page document -Follow the below exception.. \n" +
                          e.Message.Replace('{', '[').Replace('}', ']'));
                Assert.Fail("The test has been Failed due to :" + e.Message);
            }
            catch (ElementNotVisibleException e)
            {

                UALog.Error("Given element " + ElementNameAndPageName + "with locator:" + locator + " is not visible -Follow the below exception.. \n" +
                          e.Message.Replace('{', '[').Replace('}', ']'));
                Assert.Fail("The test has been Failed due to :" + e.Message);
            }
            catch (NoSuchElementException e)
            {
                UALog.Error("Given element :" + ElementNameAndPageName + " with locator:" + locator + " does not Exist - Follow the below exception.. \n" +
                          e.Message.Replace('{', '[').Replace('}', ']'));
                Assert.Fail("The test has been Failed due to :" + e.Message);
            }
            catch (Exception e)
            {
                UALog.Error("Unable to select option with index " + VisibleText + " from dropdown " + ElementNameAndPageName + " with locator: " + locator + " - Follow the below exception.. \n" +
                          e.Message.Replace('{', '[').Replace('}', ']'));
                Assert.Fail("The test has been Failed due to :" + e.Message);
            }
        }




        public void SafeType2(By locator, string text, bool clear = false, int timeOutInSeconds = 30)
        {
            SetHighlight(locator);
            if (clear)
                Driver.FindElement(locator, timeOutInSeconds).Clear();
            Driver.FindElement(locator, timeOutInSeconds).SendKeys(text);
            UALog.Info("Entered text " + text + " in the field " + locator);

        }

        public void SafeTypeByClipboard(By locator, string text, bool clear = false, int timeOutInSeconds = 30, string element_name = "")
        {
            try
            {
                SetHighlight(locator);
                if (clear)
                    Driver.FindElement(locator, timeOutInSeconds).Clear();

                Clipboard.SetText(text);
                Driver.FindElement(locator, timeOutInSeconds).SendKeys(OpenQA.Selenium.Keys.Control + "v");

                UALog.Info("Entered text " + text + " in the field " + locator);
            }
            catch (NoSuchElementException e)
            {
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error("Given element " + locator + ". does not Exist - Follow the below exception.. \n" +
                          e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail("Given element " + element_name + "[" + locator + "]. does not Exist - Follow the below exception.. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");


            }
            catch (Exception e)
            {
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error("Unable to enter text" + locator + ". - Follow the below exception.. \n" +
                          e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail("Unable  to enter text " + element_name + "[" + locator.ToString() + "] - Follow the below exception.. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");

            }
        }


        /*
         * Method : Synchronization purpose - Waits until element is loaded before enabling the checkbox
         * Params : Driver, locator element & text
         * Returns: True when element is located within time constrain
         */
        public void SafeCheck(By locator, int timeOutInSeconds = 30, string element_name = "")
        {
            try
            {
                SetHighlight(locator);
                if (!Driver.FindElement(locator).Selected)
                    Driver.FindElement(locator).Click();
                UALog.Info("Enabled " + locator + " checkbox");
            }
            catch (Exception e)
            {
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error("Unable to select " + element_name + "[" + locator + "]checkbox  - Follow the ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail("Unable to select " + element_name + "[" + locator + "]checkbox  - Follow the below exception.. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");

            }
        }

        /*
        * Method : IsTextPresentOnPageSource
        * Purpose: Checks whether the particular string exists in the page source
        * Params : Driver & String text
        * Returns: True when particular string exists, if not False
        */

        public bool IsTextPresentOnPageSource(string text)
        {
            try
            {
                return Driver.PageSource.Contains(text);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /*
         * Method : SafeSelectDropdown
         * Purpose: Selects a value from the Drop down
         * Params : WebDriver Instance, Index of element to be selected in dropdown
         * Returns: Void
         */

        public void SafeSelectDropdown(By locator, int index, int timeOutInSeconds = 200)
        {
            try
            {
                WebDriverWaitElementExists(locator, 20);
                SetHighlight(locator);
                SelectElement select = new SelectElement(Driver.FindElement(locator));
                waitForTime(2);
                select.SelectByIndex(index);
                UALog.Info("Selected option with index " + index + "from dropdown " + locator);
            }
            catch (Exception e)
            {
                // //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error("Unable to select option with index " + index + " from dropdown " + locator + " - Follow the below exception.. \n" +
                          e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail("Unable to select option with index " + index + " from dropdown " + locator + "  - Follow the below exception.. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");
            }
        }

        /*
        * Method : SafeSelectDropdown
        * Purpose:Select an option from dropdown using label
        * Params : by locator, label to be selected 
        * Returns: bool
        */
        public bool SafeSelectDropdown(By locator, string label, int timeOutInSeconds = 200)
        {
            try
            {
                IsElementPresent(locator, timeOutInSeconds);
                IWebElement element = Driver.FindElement(locator);

                SelectElement select = new SelectElement(element);
                select.SelectByText(label);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /*
         * 
         */
        public string SafeGetText(By locator, int timeOutInSeconds = 15, string element_name = "")
        {
            try
            {

                ScrollIntoView(locator);
                SetHighlightToVerify(locator);
                IWebElement ele = Driver.FindElement(locator);
                string text = ele.Text.Trim();
                UALog.Info("Taken " + element_name + "[" + locator + "] locater text");

                return text;

            }
            catch (Exception e)
            {

                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error(e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail(e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");
            }

            return null;
        }


        /*
         * 
         */
        public void VerifyElemntText(By locator, string expectedText = null, int timeOutInSeconds = 15, string element_name = "")
        {
            try
            {

                ScrollIntoView(locator);
                SetHighlightToVerify(locator);
                IWebElement ele = Driver.FindElement(locator, timeOutInSeconds);
                string text = ele.Text;
                UALog.Info("Taken " + element_name + "[" + locator + "] locater text");

                if (text != expectedText && expectedText != null)
                    throw new Exception("Verify Elemnt Text is differ to Expected value i.e., \n Actual Value: " + text + "\n Expected Value: " + expectedText);

            }
            catch (Exception e)
            {

                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error(e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail(e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");
            }


        }


        /*
         * 
         */
        public string SafeGetAttributeValue(By locator, string attributeName, int timeOutInSeconds = 60, string element_name = "")
        {
            try
            {

                ScrollIntoView(locator);
                SetHighlightToVerify(locator);
                IWebElement ele = Driver.FindElement(locator, timeOutInSeconds);
                string ele_value = ele.GetAttribute(attributeName);
                UALog.Info("Took " + element_name + "[" + locator + "] locater value");

                return ele_value;

            }
            catch (Exception e)
            {

                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error("Unable to get attribute value. Follow the below exception \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail("Unable to get attribute value. Follow the below exception \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");
            }

            return null;
        }



        /*
         * 
         */
        public void VerifyAttributeValue(By locator, string attributeName, string expectedValue = null, int timeOutInSeconds = 60, string element_name = "")
        {
            string actual_value = SafeGetAttributeValue(locator, attributeName);
            try
            {

                UALog.Info("Took " + element_name + "[" + locator + "] locater value");

                if (actual_value != expectedValue && expectedValue != null)
                    throw new Exception("Attribute value is differ to Expected value i.e., \n Actual Value: " + actual_value + "\n Expected Value: " + expectedValue);


            }
            catch (Exception e)
            {

                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error(e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail(e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");
            }


        }


        public void SafeSetAttribute(By locator, string attributeName, string attributeValue, int timeOutInSeconds = 15)
        {
            if (IsElementPresent(locator, timeOutInSeconds))
            {
                try
                {
                    IWebElement element = Driver.FindElement(locator);
                    JsDriver.ExecuteScript("arguments[0].setAttribute('" + attributeName + "', '" + attributeValue + "')", element);
                    UALog.Info("Added'" + attributeName + "' attribute with '" + attributeValue + "value");
                }
                catch (Exception e)
                {
                    UALog.Error("Exception occured while setting Attribute.. '" + attributeName + "' to '" + attributeValue + "'" + e.Message);
                }
            }
        }

        /*
        * Method : wait untill an elemet contains expected text 
        * Params : Locator, String to be verified , timeout(optional)
        * Returns: true or false
        */

        public void SafeWaitUntilTextContains(By locator, string textString, int timeOutInSeconds = 200)
        {
            try
            {
                string elementText = Driver.FindElement(locator, timeOutInSeconds).Text;
                for (int i = 0; i < timeOutInSeconds; i++)
                {
                    Thread.Sleep(1000);
                    if (elementText.Contains(textString))
                        return;
                }
            }
            catch (Exception e)
            {
                UALog.Error(e.Message);
                throw new Exception(e.Message);
            }
        }

        public bool IsTextPresentInPage(string textString)
        {
            string pageText = Driver.PageSource.ToString();

            if (pageText.Contains(textString))
                return true;
            else
                return false;
        }

        /*
       * Method : Get an element from list of same type of elements
       * Params : by locator, string to be verified 
       * Returns: IWebElement
       */
        public IWebElement GetElementFromList(By locator, string value, int timeOutInSeconds = 200)
        {
            try
            {
                IsElementPresent(locator, timeOutInSeconds);
                ReadOnlyCollection<IWebElement> list = Driver.FindElements(locator);

                foreach (IWebElement element in list)
                {
                    if (element.Text.Trim().Contains(value.Trim()))
                    {
                        return element;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public int GetRowCountOfELementFromList(By locator, string value, int timeOutInSeconds = 60)
        {
            try
            {
                //IsElementPresent(locator, timeOutInSeconds);
                ReadOnlyCollection<IWebElement> list = Driver.FindElements(locator);
                int i = 0;
                foreach (IWebElement element in list)
                {
                    i = i + 1;
                    if (element.Text.Trim().Contains(value.Trim()))
                    {
                        return i;
                    }
                }
                UALog.Info("Given name '" + value + "' is not present in the list of Elements.");
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                throw new Exception("Given name '" + value + "' is not present in the list of Elements. Please follow the below reason. \n"
                            + "\n Please refer to the above [Screenshot] for more information...");

            }
            catch (Exception e)
            {

              //  TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
              //  _autoutilities.getScreenshot(Driver);
                UALog.Error("Unexpected Exception occured in GetRowCountOfELementFromList method: Please follow the below reason. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from .../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail("Unexpected Exception occured in GetRowCountOfELementFromList method: Please follow the below reason. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");
            }
            return 0;

        }


        /*
       * Method : Safe Wait for an alert present
       * Params : NULL
       * Returns: bool
       */
        public void SafeAcceptAlert(int timeOutInSeconds = 20)
        {
            IAlert alert = null;
            try
            {
                alert = WaitForAlert(timeOutInSeconds);
                alert.Accept();
            }
            catch (NullReferenceException) { }
        }

        public IAlert WaitForAlert(int waitTimeInSeconds = 5)
        {
            UALog.Info("Waiting for an Alert for " + waitTimeInSeconds + " seconds..");
            IAlert alert = null;
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(waitTimeInSeconds));
            try
            {
                alert = wait.Until(d =>
                {
                    try
                    {
                        return Driver.SwitchTo().Alert();
                    }
                    catch (NoAlertPresentException)
                    {
                        UALog.Info("Waiting for Alert...");
                        // Alert not present yet
                        return null;
                    }
                    catch (Exception e)
                    {
                        UALog.Info("Waiting for Alert..." + e.Message + e.StackTrace);
                        // Alert not present yet
                        return null;
                    }
                });
            }
            catch (WebDriverTimeoutException e)
            { UALog.Error("No Alert was found in " + waitTimeInSeconds + " seconds..\n" + e.Message); alert = null; }
            return alert;
        }

        public string GetAlertText(int timeOutInSeconds = 200)
        {
            IAlert alert = null;
            string alertText = "";
            try
            {
                alert = WaitForAlert(timeOutInSeconds);
                alertText = alert.Text;
                alert.Accept();
            }
            catch (NullReferenceException) { }
            return alertText;
        }

        /*
       * Method : Close current window
       * Params : webDriver instance
       * Returns: void
       */
        public void CloseCurrentWindow()
        {
            //Close the graph report window
            Driver.Close();
            Thread.Sleep(500);
            SelectLastWindow();
        }

        /*
       * Method : Select new popup window
       * Params : webDriver instance
       * Returns: void
       */
        public void SelectNewPopupWindow()
        {
            Thread.Sleep(500);
            //list out all the windows of the current browser
            ReadOnlyCollection<string> WindowsList = Driver.WindowHandles;
            //get the total number of windows
            int i = WindowsList.Count;
            //select the new popup window
            Driver.SwitchTo().Window(WindowsList[i - 1]);
            WaitForPageToLoad();
            Driver.Manage().Window.Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width + 10, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height + 10);

        }
        /*
        * Method : Select last window
        * Params : webDriver instance
        * Returns: void
        */
        public void SelectLastWindow()
        {
            Thread.Sleep(500);
            //list out all the windows of the current browser
            ReadOnlyCollection<string> WindowsList = Driver.WindowHandles;
            //get the total number of windows
            int i = WindowsList.Count;
            //select the new popup window
            Driver.SwitchTo().Window(WindowsList[i - 1]);
        }


        /*
       * Method : Select main window
       * Params : webDriver instance
       * Returns: void
       */
        public void SelectMainWindow()
        {
            Thread.Sleep(500);
            //list out all the windows of the current browser
            ReadOnlyCollection<string> WindowsList = Driver.WindowHandles;
            //select the new popup window
            Driver.SwitchTo().Window(WindowsList[0]);
            //SafeClick(By.XPath("html/body"));
            //Driver.SwitchTo().DefaultContent();

        }

        public void DrawOnCanvas(By locator)
        {
            try
            {
                {
                    Actions actionBuilder = new Actions(Driver);
                    OpenQA.Selenium.Interactions.IAction drawOnCanvas = actionBuilder.ClickAndHold(Driver.FindElement(locator))
                        .MoveByOffset(15, 15)
                        .MoveByOffset(4, -4)
                        .MoveByOffset(-7, 10)
                        .Release(Driver.FindElement(locator))
                        .Build();
                    drawOnCanvas.Perform();
                }
            }
            catch (Exception e)
            {
                UALog.Error("Exception occured when drawing on element: " + locator + " Exception is " + e.Message + e.StackTrace);
            }
        }

        public void DrawOnCanvasiPad(string jqueryToElement)
        {
            MyTouchActions touch = new MyTouchActions(Driver);
            //touch.tap(jqueryToElement, 3000);
            touch.drag(jqueryToElement, 12, 12);
        }

        public void SelectAllAction(By locator)
        {
            try
            {
                char c = '\u0001'; // ASCII code 1 for Ctrl-A
                Driver.FindElement(locator).SendKeys(Convert.ToString(c));
            }
            catch (Exception e)
            {
                UALog.Error("Exception occured when selecting all on element: " + locator + " Exception is " + e.Message + e.StackTrace);
            }
        }

        public void PressEscape(By locator)
        {
            try
            {
                Actions actionBuilder = new Actions(Driver);
                OpenQA.Selenium.Interactions.IAction Escape = actionBuilder.SendKeys(Driver.FindElement(locator), OpenQA.Selenium.Keys.Escape)
                    .Build();

                Escape.Perform();
            }
            catch (Exception e)
            {
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));	               
                _autoutilities.getScreenshot(Driver);

                UALog.Error("Exception occured when pressing escape on element: " + locator + " Exception is " + e.Message + e.StackTrace);
            }
        }

        public void PressDownArrow(By locator)
        {
            try
            {
                Actions actionBuilder = new Actions(Driver);
                // OpenQA.Selenium.Interactions.IAction Escape = actionBuilder.SendKeys(Driver.FindElement(locator),OpenQA.Selenium.Keys.ArrowDown)
                //    .Build();

                // Escape.Perform();
                actionBuilder.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
            }
            catch (Exception e)
            {
                // //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));	               
                _autoutilities.getScreenshot(Driver);
                UALog.Error("Exception occured when pressing escape on element: " + locator + " Exception is " + e.Message + e.StackTrace);
            }
        }

        public void PressEnter()
        {
            try
            {
                Actions actionBuilder = new Actions(Driver);
                // OpenQA.Selenium.Interactions.IAction Escape = actionBuilder.SendKeys(Driver.FindElement(locator),OpenQA.Selenium.Keys.Enter).Build();

                actionBuilder.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                // Escape.Perform();
            }
            catch (Exception e)
            {
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));	               
                _autoutilities.getScreenshot(Driver);
                UALog.Error("Exception occured when pressing escape on element:  Exception is " + e.Message + e.StackTrace);
            }
        }   

        public void ScrollIntoView(By locator)
        {
            try
            {
                JsDriver.ExecuteScript("arguments[0].scrollIntoView(true);window.scrollBy(0," + (-200) + ");", Driver.FindElement(locator));
            }
            catch (Exception)
            {

            }
        }

        public int GetNumberOfElements(By locator)
        {
            try
            {
                Driver.SetImplicitWait(90);
                return Driver.FindElements(locator).Count;
            }
            catch (Exception)
            {
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));	               
                _autoutilities.getScreenshot(Driver);
                return 0;
            }

        }

        public void SetHighlight(By locator)
        {
            try
            {
                if (_autoUtils.GetKeyValue("appSettings", "HighlightElements").Equals("true"))
                {
                    string sAttributeValue = "border:3px solid red;";
                    string sAttribute = Driver.FindElement(locator).GetAttribute("style");
                    JsDriver.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", Driver.FindElement(locator), sAttributeValue);
                    Thread.Sleep(100);
                    JsDriver.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", Driver.FindElement(locator), sAttribute);
                }
            }
            catch (Exception)
            {


            }
        }

        public void SetHighlightToVerify(By locator)
        {
            try
            {
                if (_autoUtils.GetKeyValue("appSettings", "HighlightElements").Equals("true"))
                {
                    string sAttributeValue = "border:3px solid green;";
                    string sAttribute = Driver.FindElement(locator).GetAttribute("style");
                    JsDriver.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", Driver.FindElement(locator), sAttributeValue);
                    Thread.Sleep(100);
                    JsDriver.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", Driver.FindElement(locator), sAttribute);
                }
            }
            catch
            {
                // //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));	               
                _autoutilities.getScreenshot(Driver);
            }
        }

        public void SetHighlightToGet(By locator)
        {
            try
            {
                if (_autoUtils.GetKeyValue("appSettings", "HighlightElements").Equals("true"))
                {
                    string sAttributeValue = "border:3px solid blue;";
                    string sAttribute = Driver.FindElement(locator).GetAttribute("style");
                    JsDriver.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", Driver.FindElement(locator), sAttributeValue);
                    Thread.Sleep(100);
                    JsDriver.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", Driver.FindElement(locator), sAttribute);
                }
            }
            catch (Exception)
            {
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));	               
                _autoutilities.getScreenshot(Driver);
            }
        }

        /*
        * Method : Click on specific element from list of elements
        * Params : Driver, & locator element, element value to be clicked
        * Returns: True or false
        */
        public void SafeClickFromListOfElements(By bylocator, string value)
        {
            try
            {
                IWebElement element = GetElementFromList(bylocator, value);
                if (element != null)
                {
                    if (_autoUtils.GetKeyValue("appSettings", "HighlightElements").Equals("true"))
                        SetHighlight(bylocator);
                    element.Click();

                    UALog.Info("Clicked on element " + bylocator);

                }
                else
                {
                    UALog.Error("Unable to find give " + value + " value in the List");
                    throw new Exception("Unable to find give " + value + " value in the List");
                }
            }
            catch (Exception e)
            {
                UALog.Error("Unable to click on " + bylocator + ". Following is the exception -" + e.Message);
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));	               
                _autoutilities.getScreenshot(Driver);
                throw new Exception("Unable  to  click  on [" + bylocator + "].  Following  is  the  exception -" + e.Message);

            }
        }

        public void SafeVerifyFromListOfElements(By bylocator, string value)
        {
            try
            {
                IWebElement element = GetElementFromList(bylocator, value);
                if (element != null)
                {
                    if (_autoUtils.GetKeyValue("appSettings", "HighlightElements").Equals("true"))
                        SetHighlight(bylocator);

                    UALog.Info("Verified element " + bylocator);

                }
                else
                {
                    UALog.Error("Unable to find give " + value + " value in the List");
                    //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));	               
                    _autoutilities.getScreenshot(Driver);
                    throw new Exception("Unable to find give " + value + " value in the List");
                }
            }
            catch (Exception e)
            {
                UALog.Error("Unable to click the element " + bylocator + ". Following is the exception -" + e.Message);
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));	               
                _autoutilities.getScreenshot(Driver);
                throw new Exception("Unable  to  click   the  element [" + bylocator + "].  Following  is  the  exception -" + e.Message);

            }
        }
        /*
        * Method : Synchronization purpose - Waits until element is loaded before verifying attribute value
        * Params : Driver, & locator element, attribute, attribute value to be verified
        * Returns: True or false
        */
        public bool SafeCheckAttributeValuePresent(By bylocator, string attribute, string value)
        {
            IWebElement element = WebDriverWaitElementExists(bylocator, Waits.getsShortWait());
            try
            {
                if (element.GetAttribute(attribute).Trim() == value.Trim() && element != null)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));	               
                _autoutilities.getScreenshot(Driver);
                return false;
            }

        }
        /*
        * Method : Synchronization purpose - Waits until element is loaded and click and hold on the element
        * Params : Driver, & locator element to click and hold
        * Returns: bool
        */
        public bool SafeActionClickAndHold(By bylocator)
        {
            IWebElement element = WebDriverWaitElementExists(bylocator, Waits.getsShortWait());
            if (element != null)
            {
                try
                {
                    if (_autoUtils.GetKeyValue("appSettings", "HighlightElements").Equals("true"))
                        SetHighlight(bylocator);

                    Actions ac = new Actions(Driver);
                    ac.MoveToElement(element).ClickAndHold().Perform();
                    return true;
                }
                catch (Exception)
                {
                    //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));	               
                    _autoutilities.getScreenshot(Driver);
                    return false;
                }
            }
            else { return false; }
        }
        /*
         * Method : Synchronization purpose - Waits until element is loaded and perform action click on element
         * Params : Driver, & locator element to be clicked
         * Returns: bool
         */
        public void SafeActionClick(By locator, int timeOutInSeconds = 60, string element_name = "")
        {
            IWebElement element = WebDriverWaitElementExists(locator, Waits.getsShortWait());
            if (element != null)
            {
                try
                {
                    if (_autoUtils.GetKeyValue("appSettings", "HighlightElements").Equals("true"))
                        SetHighlight(locator);

                    Actions ac = new Actions(Driver);
                    ac.MoveToElement(element).Click().Perform();

                }
                catch (Exception e)
                {

                    UALog.Error("Unable to click on " + locator + ". element - Follow the below exception.. \n" +
                              e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                    //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));	               
                    _autoutilities.getScreenshot(Driver);
                    Assert.Fail("Unable  to click  on " + element_name + "[" + locator.ToString() + "]. Element - Follow the below exception.. \n" +
                                e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");

                }
            }
            else
            {

                UALog.Error("Given element " + locator + ". does not Exist - Follow the below exception.. \n" +
                          "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                Assert.Fail("Given element " + element_name + "[" + locator + "]. does not Exist - Follow the below exception.. \n" +
                            "\n Please refer to the above [Screenshot] for more information...");
            }
        }
        /*
         * Method : Synchronization purpose - Waits until element is loaded and double click on the element
         * Params : Driver, & locator element to be double clicked
         * Returns: bool
         */
        public void SafeDoubleClick(By locator, int timeOutInSeconds = 60, string element_name = "")
        {
            IWebElement element = WebDriverWaitElementExists(locator, Waits.getsShortWait());
            if (element != null)
            {
                try
                {
                    if (_autoUtils.GetKeyValue("appSettings", "HighlightElements").Equals("true"))
                        SetHighlight(locator);
                    ScrollIntoView(locator);
                    Actions ac = new Actions(Driver);
                    ac.MoveToElement(element).DoubleClick().Perform();

                }
                catch (Exception e)
                {

                    UALog.Error("Unable to double click on " + locator + ". element - Follow the below exception.. \n" +
                              e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                    //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));	               
                    _autoutilities.getScreenshot(Driver);
                    Assert.Fail("Unable  to double click  on " + element_name + "[" + locator.ToString() + "]. Element - Follow the below exception.. \n" +
                                e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");

                }
            }
            else
            {

                UALog.Error("Given element " + locator + ". does not Exist - Follow the below exception.. \n" +
                          "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                Assert.Fail("Given element " + element_name + "[" + locator + "]. does not Exist - Follow the below exception.. \n" +
                            "\n Please refer to the above [Screenshot] for more information...");
            }
        }
        /*
        * Method : Synchronization purpose - Waits until element is loaded and right click on the element
        * Params : Driver, & locator element to right click, indexOption to select from right click options
        * Returns: bool
        */
        public bool SafeMouseRightClick(By bylocator, int indexOption)
        {
            IWebElement element = WebDriverWaitElementExists(bylocator, Waits.getsShortWait());
            if (element != null)
            {
                try
                {
                    if (_autoUtils.GetKeyValue("appSettings", "HighlightElements").Equals("true"))
                        SetHighlight(bylocator);

                    Actions ac = new Actions(Driver);
                    ac.ContextClick(element).Perform();
                    for (int i = 1; i <= indexOption; i++)
                        ac.SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    ac.SendKeys(OpenQA.Selenium.Keys.Enter);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else { return false; }
        }


        /*
         * Method : Synchronization purpose - Waits until element is loaded before verifying it 
         * Params : Driver, & locator element to be verified
         * Returns: bool
         */
        public void SafeVerification(By locator, int timeOutInSeconds = 60, string element_name = "")
        {
            try
            {
                ScrollIntoView(locator);
                if (_autoUtils.GetKeyValue("appSettings", "HighlightElements").Equals("true"))
                    SetHighlightToVerify(locator);
                Driver.FindElement(locator, timeOutInSeconds);

            }
            catch (Exception e)
            {

                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error("verify element " + locator + " is not present. Follow the below Exception.. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail("verify element " + element_name + "[" + locator + "] is not present. Follow the below Exception.. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");

            }

        }

        /*
          * Method : Drag and drop an element 
          * Params : Driver, & sourceElement element to drag and drop on targetElement
          * Returns: bool
          */
        public bool DragAndDropElement(IWebElement sourceElement, IWebElement targetElement)
        {

            try
            {

                Actions ac = new Actions(Driver);
                ac.DragAndDrop(sourceElement, targetElement).Build().Perform();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /*
      * Method : Synchronization purpose - Waits until element is loaded before Getting/returing an element
      * Params : Driver, & locator element
      * Returns: IWebElement
      */
        public IWebElement SafeGetElement(By locator)
        {
            IWebElement element = WebDriverWaitElementExists(locator, Waits.getsShortWait());
            if (element != null)
                return element;
            else
                return null;
        }

        public void waitExplicit(int TimeUnits, By Locator)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TimeUnits));
            wait.Until(drv => drv.FindElement(Locator));
        }
        /*
       * Method : Synchronization purpose - Waits until element is loaded before Hovering the mouse on element
       * Params : Driver, & locator element to mouse hover on specified element
       * Returns: void
       */
        public void SafeMouseHover(By locator, int timeOutInSeconds = 60, string element_name = "")
        {
            IWebElement element = WebDriverWaitElementExists(locator, timeOutInSeconds);
            if (element != null)
            {
                try
                {
                    if (_autoUtils.GetKeyValue("appSettings", "HighlightElements").Equals("true"))
                        SetHighlight(locator);

                    Actions ac = new Actions(Driver);
                    ac.MoveToElement(element).Build().Perform();

                }
                catch (Exception e)
                {
                    //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                    _autoutilities.getScreenshot(Driver);
                    UALog.Error("Unable to Mouse Hover on the element " + locator + ". Follow the below Exception.. \n" +
                                e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                    Assert.Fail("Unable to Mouse Hover on the element " + locator + ". Follow the below Exception.. \n" +
                                e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");
                }
            }
            else
            {
                UALog.Error("Given element " + locator + ". does not Exist - Follow the below exception.. \n" +
                          "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                Assert.Fail("Given element " + element_name + "[" + locator + "]. does not Exist - Follow the below exception.. \n" +
                            "\n Please refer to the above [Screenshot] for more information...");

            }
        }

        /*
         * Params - URL
         * Return Type - Void
         * Description - This method launches the URL given in the Config file
         */
        public void NavigateToAppURL(string URL)
        {
            try
            {

                Driver.Navigate().GoToUrl(URL);
                //Driver.Navigate().Refresh();


            }
            catch (WebDriverException e)
            {
                UALog.Error("Follow the below exception.. \n" +
                        e.Message.Replace('{', '[').Replace('}', ']') + ". Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                Assert.Fail("Follow the below exception.. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + " Please refer to the above [Screenshot] for more information...");

            }
            catch (Exception e)
            {
                UALog.Error("Follow the below exception.. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + " Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                Assert.Fail("Follow the below exception.. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + " Please refer to the above [Screenshot] for more information...");

            }
            WaitForPageToLoad();

        }

        public void SwitchToFrame(By locator, int timeOutInSeconds = 20, string element_name = "")
        {
            try
            {
                WaitForPageToLoad(10);
                Driver.SwitchTo().Frame(Driver.FindElement(locator, timeOutInSeconds));
                Thread.Sleep(1000);
                Driver.SetImplicitWait(5);
            }
            catch (NoSuchElementException e)
            {

                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error("Unable to find " + element_name + "[" + locator + "]. Please follow the below reason. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail("Unable to find " + element_name + "[" + locator + "]. Please follow the below reason. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");

            }
            catch (NoSuchFrameException e)
            {
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error("Unable to switch " + element_name + "[" + locator + "] frame. Please follow the below reason. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail("Unable to switch " + element_name + "[" + locator + "] frame. Please follow the below reason. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");

            }
            catch (Exception e)
            {
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error("Unable to switch " + element_name + "[" + locator + "] frame. Please follow the below reason. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail("Unable to switch " + element_name + "[" + locator + "] frame. Please follow the below reason. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");

            }
        }


        public void RunAutoItFile(string autoitFile, string arg)
        {

            try
            {

                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();

                startInfo.FileName = autoitFile;
                startInfo.Arguments = arg;
                process.StartInfo = startInfo;
                process.Start();


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int SafeGetNumberOfValuesInDropDown(By locator)
        {
            int size = 0;
            try
            {
                SelectElement el = new SelectElement(Driver.FindElement(locator));
                IList<IWebElement> l = el.Options;
                size = l.Count;
            }
            catch (NoSuchElementException)
            {
                UALog.Error("Unable to find the element " + locator);
            }
            return size;
        }

        /// <summary>
        /// Switches to window with the given title
        /// </summary>
        /// <param name="title">need to provide the title</param>
        public void SwitchToWindow(string title)
        {
            bool flag = true;
            foreach (string handle in Driver.WindowHandles)
            {
                IWebDriver popup = Driver.SwitchTo().Window(handle);

                if (popup.Title.Contains(title))
                {
                    flag = false;
                    break;
                }

            }
            if (flag)
            {
                Assert.Fail("Requested window is not available");
            }
            UALog.Info("Switched to window " + title);
        }

        /// <summary>
        /// Switches to window with the given title
        /// </summary>
        /// <param name="title">need to provide the title</param>
        public void SwitchToAnotherWindow(string title)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(driver => Driver.WindowHandles.Count > 1);
            bool flag = true;
            foreach (string handle in Driver.WindowHandles)
            {
                IWebDriver popup = Driver.SwitchTo().Window(handle);

                if (popup.Title.Contains(title))
                {
                    flag = false;
                    WaitForPageToLoad();
                    WaitForJQueryProcessing();
                    break;
                }

            }
            if (flag)
            {
                Assert.Fail("Requested window is not available");
            }
            UALog.Info("Switched to window " + title);
        }

        /// <summary>
        /// Switches to a window by URL or partial URL text
        /// </summary>
        /// <param name="url">url text should be passed as argument</param>
        /// <returns>returns void</returns>
        public void SwitchToWindowByURL(string url)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(driver => Driver.WindowHandles.Count > 1);

            bool flag = true;
            foreach (string handle in Driver.WindowHandles)
            {
                IWebDriver popup = Driver.SwitchTo().Window(handle);

                if (popup.Url.Contains(url))
                {
                    WaitForPageToLoad();
                    WaitForJQueryProcessing();
                    flag = false;
                    break;
                }

            }
            if (flag)
            {
                Assert.Fail("Requested window is not available");
            }

        }

        /// <summary>
        /// Method for verifying Downloaded file
        /// @return:None
        /// @parameter:String
        /// </summary>

        public void verifyDownloadedFile(String fileName)
        {
            Assert.IsTrue(checkFileExists(fileName), fileName + " file is not downloaded to downloads path");
        }


        /// <summary>
        /// Method for Checking the existence of the downloaded file
        /// @return:Boolean
        /// @parameter:String
        /// </summary>

        public Boolean checkFileExists(String fileName)
        {
            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDownload = Path.Combine(pathUser, "Downloads");
            DirectoryInfo dir = new DirectoryInfo(pathDownload);
            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Name.StartsWith(fileName))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        ///Synchronization purpose - Waits until element is loaded before disabling the checkbox and uncheck the checkbox
        /// </summary>
        /// <params> locator element ,TimeUnit,Element Name</params>
        /// <return>None</returns>

        public void SafeUnCheck(By locator, int timeOutInSeconds = 30, string element_name = "")
        {
            try
            {
                SetHighlight(locator);
                if (Driver.FindElement(locator).Selected)
                    Driver.FindElement(locator).Click();
                UALog.Info("Enabled " + locator + " checkbox");
            }
            catch (Exception e)
            {
                UALog.Error("Unable to select " + element_name + "[" + locator + "]checkbox  - Follow the below exception.. \n" +
                          e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../../Selenium_Automation/OutPut/screenshots/" + testName + ".png");
                _autoutilities.getScreenshot(Driver);
                Assert.Fail("Unable to select " + element_name + "[" + locator + "]checkbox  - Follow the below exception.. \n" +
                           e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");

            }
        }
        /// <summary>
        ///Click on first element from list of elements
        /// </summary>
        /// <params>locator element</params>
        /// <return>None</returns>

        public void SafeClickFirstElementFromListOfElements(By bylocator)
        {
            try
            {
                ReadOnlyCollection<IWebElement> elements = Driver.FindElements(bylocator);
                if (elements != null)
                {
                    if (_autoUtils.GetKeyValue("appSettings", "HighlightElements").Equals("true"))
                        SetHighlight(bylocator);
                    foreach (IWebElement e in elements)
                    {
                        e.Click();
                        break;
                    }
                    UALog.Info("Clicked on element " + bylocator);
                }
                else
                {
                    UALog.Error("Unable to get the elements from List");
                    throw new Exception("Unable to get the elements from List");
                }
            }
            catch (Exception e)
            {
                UALog.Error("Unable to click on " + bylocator + ". Following is the exception -" + e.Message);
                throw new Exception("Unable  to  click  on [" + bylocator + "].  Following  is  the  exception -" + e.Message);
            }

        }

        public void SafeSendKeys(By locator,string key,int TimeOutInSeconds=30,string element_name="")
        {
            try
            {
                Actions action = new Actions(Driver);
                action.SendKeys(Driver.FindElement(locator, TimeOutInSeconds), key).Build().Perform();
            }
            catch (NoSuchElementException e)
            {

               // TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error("Unable to find " + element_name + "[" + locator + "]. Please follow the below reason. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail("Unable to find " + element_name + "[" + locator + "]. Please follow the below reason. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");

            }

            catch (Exception e)
            {
                //TestLog.AttachImage("Click here for Screenshot", _autoutilities.getScreenshot(Driver));
                _autoutilities.getScreenshot(Driver);
                UALog.Error("Unable to perform action: "+key+" on " + element_name + "[" + locator + "] . Please follow the below reason. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "Please refer the screenshot from ../Automation_Report/OutPut/screenshots/" + testName + ".png");
                Assert.Fail("Unable to perform action: " + key + " on " + element_name + "[" + locator + "] . Please follow the below reason. \n" +
                            e.Message.Replace('{', '[').Replace('}', ']') + "\n Please refer to the above [Screenshot] for more information...");

            }
        }

        public void RobotType(By locator, string friendlyname, string text)
        {
            IWebElement element = Driver.FindElement(locator);
            element.SendKeys(text);
            waitForTime(1);
            Robot r = new Robot();
            r.keyPress(KeyEvent.VK_A);
            waitForTime(1);
            r.keyPress(KeyEvent.VK_BACK_SPACE);
            waitForTime(1);
           // UALog.Info("Entering keys with keyboard actions " + TestCaseName() + " : Entered text " + text + " in the field " + locator);
        }
        /*
     * Method : Synchronization purpose - Waits until element is loaded and Control + click on the element
     * Params : Driver, & locator element to Control click
     * Returns: bool
     */
        public void SafeMouseControlClick(By bylocator)
        {
            IWebElement element = WebDriverWaitElementExists(bylocator, Waits.getsShortWait());
            if (element != null)
            {
                try
                {
                    if (_autoUtils.GetKeyValue("appSettings", "HighlightElements").Equals("true"))
                        SetHighlight(bylocator);



                    Actions ac = new Actions(Driver);
                    ac.KeyDown(OpenQA.Selenium.Keys.Control).MoveToElement(element).Click().KeyUp(OpenQA.Selenium.Keys.Control).Perform();
                }
                catch (Exception e)
                {
                    log4N.Info(e.Message);
                }
            }
        }
        public void RightClickOntheLinkAndValidate(By LinkLocator, By ValidationLocator)
        {
            SafeMouseControlClick(LinkLocator);
            SwitchToLastTab();
            waitForTime(TimeOuts.VERYSHORTWAIT);
            WaitForPageToLoad(TimeOuts.MEDIUMWAIT);
            WaitUntilElementIsDisplayed(ValidationLocator, TimeOuts.SHORTWAIT);
            Assert.IsTrue(IsElementPresent(ValidationLocator, 6));
            SwitchToFirstTabAndCloseRemaninig();
            WaitForPageToLoad(TimeOuts.MEDIUMWAIT);
        }

    }

}
