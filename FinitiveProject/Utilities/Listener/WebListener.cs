using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Internal;
using Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Events;
using log4net;
using OpenQA.Selenium.Remote;

namespace SeleniumAutomation.Listener
{
    public class WebListener : EventFiringWebDriver
    {
        //private IWebDriver driver;
        private readonly ILog webLog = LogManager.GetLogger("WebListener");
        private EventFiringWebDriver firingDriver;

        public WebListener(IWebDriver driver) : base(driver)
        {
            firingDriver = new EventFiringWebDriver(driver);
            firingDriver.Navigating += new EventHandler<WebDriverNavigationEventArgs>(WhenNavigating);
            firingDriver.Navigated += new EventHandler<WebDriverNavigationEventArgs>(AfterNavigation);
            firingDriver.NavigatingBack += new EventHandler<WebDriverNavigationEventArgs>(WhenNavigatingBack);
            firingDriver.NavigatedBack += new EventHandler<WebDriverNavigationEventArgs>(AfterNavigatedBack);
            firingDriver.NavigatingForward += new EventHandler<WebDriverNavigationEventArgs>(WhenNavigatingForward);
            firingDriver.NavigatedForward += new EventHandler<WebDriverNavigationEventArgs>(AfterNavigatedForward);
            firingDriver.FindingElement += new EventHandler<FindElementEventArgs>(WhenFindingElement);
            firingDriver.FindElementCompleted += new EventHandler<FindElementEventArgs>(AfterElementIsFound);
            firingDriver.ElementClicking += new EventHandler<WebElementEventArgs>(WhenClickingElement);
            firingDriver.ElementClicked += new EventHandler<WebElementEventArgs>(WhenElementIsClicked);
            //firingDriver.ElementValueChanging += new EventHandler<WebElementEventArgs>(WhenElementValueisChanging);
            //firingDriver.ElementValueChanged += new EventHandler<WebElementEventArgs>(AfterElementValueChanged);
           // firingDriver.ExceptionThrown += new EventHandler<WebDriverExceptionEventArgs>(WhenExceptionIsThrown);
            //firingDriver.ScriptExecuted
            //firingDriver.ScriptExecuting
        }


        public IWebDriver Driver
        {
            get { return firingDriver; }
        }

        public void WhenNavigating(object sender, WebDriverNavigationEventArgs e)
        {
            webLog.Info("Navigating to url " + e.Url);
        }

        public void AfterNavigation(object sender, WebDriverNavigationEventArgs e)
        {
            webLog.Info("Navigated to url " + e.Url);
        }

        public void WhenNavigatingBack(object sender, WebDriverNavigationEventArgs e)
        {
            webLog.Info("Navigating back");
        }

        public void AfterNavigatedBack(object sender, WebDriverNavigationEventArgs e)
        {
            webLog.Info("Navigated back");
        }

        public void WhenNavigatingForward(object sender, WebDriverNavigationEventArgs e)
        {
            webLog.Info("Navigating forward");
        }

        public void AfterNavigatedForward(object sender, WebDriverNavigationEventArgs e)
        {
            webLog.Info("Navigated forward");
        }

        public void WhenFindingElement(object sender, FindElementEventArgs e)
        {
            // webLog.Info("Finding Element");
        }

        public void AfterElementIsFound(object sender, FindElementEventArgs e)
        {
            //webLog.Info("Found Element " + e.FindMethod.ToString());
        }

        public void WhenClickingElement(object sender, WebElementEventArgs e)
        {
            webLog.Info("Clicking element");
        }

        public void WhenElementIsClicked(object sender, WebElementEventArgs e)
        {
            webLog.Info("Clicked element");
        }

        public void WhenElementValueisChanging(object sender, WebElementEventArgs e)
        {
            webLog.Info("Changed value of the element");
        }

        public void AfterElementValueChanged(object sender, WebElementEventArgs e)
        {
            webLog.Info("Changed value of the element");
        }

        public void WhenExceptionIsThrown(object sender, WebDriverExceptionEventArgs e)
        {
            if (e.ThrownException is NoSuchElementException || e.ThrownException is NoAlertPresentException)
            {
            }
            else
            {
                webLog.Error(e.ThrownException.Message + e.ThrownException.StackTrace);
            }
        }
    }
}