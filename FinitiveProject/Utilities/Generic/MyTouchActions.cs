using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SeleniumAutomation.Selenium
{
    public class MyTouchActions
    {
        public static String YUI_PATH = "http://yui.yahooapis.com/3.7.3/build/yui/yui.js";
        private IWebDriver driver;
        private IJavaScriptExecutor js;
        public MyTouchActions(IWebDriver driver)
        {
            this.driver = driver;
            js = (IJavaScriptExecutor)driver;
        }
        public void includeYUIlibrary(){  
           var importedScripts = driver.FindElements(By.TagName("script"));  
           bool yuiIncluded = false;  
           foreach(IWebElement e in importedScripts)
           {  
                if(e.GetAttribute("src").Contains(YUI_PATH))  
                {  
                     yuiIncluded = true;  
                     break;  
                }  
           }  
           if(!yuiIncluded)  
           {  
                /*  
                 *      script = document.createElement('script');    
                     script.type = 'text/javascript';    
                     script.async = true;    
                     script.onload = function(){   
                         // remote script has loaded    
                    };    
                     script.src = 'http://yui.yahooapis.com/3.7.3/build/yui/yui.js';   
                      document.getElementsByTagName('head')[0].appendChild(script);  
                 */  
                String IncludeYUI = "script = document.createElement('script');script.type = 'text/javascript';script.async = true;script.onload = function(){};script.src = '"+YUI_PATH+"';document.getElementsByTagName('head')[0].appendChild(script);";    
                js.ExecuteScript(IncludeYUI);  
           }  
      }
        public void tap(String jqueryToElement)
        {
            includeYUIlibrary();
            /*  
             //simulate tap gesture  
          node.simulateGesture("tap");  
             */
            String JavascriptToTap = "YUI().use('node-event-simulate', function(Y){var node = Y.one('" + jqueryToElement + "');node.simulateGesture('tap');});";
            js.ExecuteScript(JavascriptToTap);
        }

        public void tap(String jqueryToElement, long milliseconds)
        {
            includeYUIlibrary();
            /*  
             //simulate tap gesture  
          node.simulateGesture("tap");  
             */
            String JavascriptToTap = "YUI().use('node-event-simulate', function(Y){var node = Y.one('" + jqueryToElement + "');node.simulateGesture('tap'{,hold:" + milliseconds + "});});";
            js.ExecuteScript(JavascriptToTap);
        }

        public void tap(String jqueryToElement, Point p)
        {
            includeYUIlibrary();
            /*  
             * // simulate tap with options and callback  
              node.simulateGesture("tap", {  
                point: [30, 30], // tap (30, 30) relative to the top/left of the node  
             });  
             */
            String pointlocation = "point:[" + p.X + "," + p.Y + "]";
            String JavascriptToTapatPoint = "YUI().use('node-event-simulate', function(Y){var node = Y.one('" + jqueryToElement + "');node.simulateGesture('tap',{" + pointlocation + "});});";
            js.ExecuteScript(JavascriptToTapatPoint);
            System.Threading.Thread.Sleep(1000);

        }
        public void tap(String jqueryToElement, Point p, long milliseconds)
        {
            includeYUIlibrary();
            /*  
             * // simulate tap with options and callback  
              node.simulateGesture("tap", {  
                point: [30, 30], // tap (30, 30) relative to the top/left of the node  
                hold: 3000,   // hold for 3sec in a tap  
           });  
             */
            String pointlocation = "point:[" + p.X + "," + p.Y + "]";
            String JavascriptToTaplongatPoint = "YUI().use('node-event-simulate', function(Y){var node = Y.one('" + jqueryToElement + "');node.simulateGesture('press',{" + pointlocation + ",hold:" + milliseconds + "});});";
            js.ExecuteScript(JavascriptToTaplongatPoint);
            System.Threading.Thread.Sleep(1000);
        }
        /*  
         * xdistance : +ve integer to move right and -ve integer to move left  
         * ydistance : +ve integer to move down and -ve integer to move up  
         * milliseconds : drag is performed for specified time  
         */
        public void drag(String jqueryToElement, int xdistance, int ydistance, long milliseconds)
        {
            includeYUIlibrary();
            /*  
             node.simulateGesture("move", {  
      path: {  
        xdist: 1500,  
        ydist: 0  
      } ,  
      duration: [milliseconds]  
    });  
        */
            //String JavascriptTodrag = "YUI().use('node-event-simulate', function(Y){var node = Y.one('"+jqueryToElement+"');node.simulateGesture('move',{path:{xdist:"+xdistance+",ydist:"+ydistance+"},duration:"+milliseconds+"});});";  
            String JavascriptTodrag = "YUI().use('node-event-simulate', function(Y){var node = Y.one('" + jqueryToElement + "');node.simulateGesture('move',{path:{xdist:" + xdistance + ",ydist:" + ydistance + "},duration:" + milliseconds + "}, function() {Y.log('I was called.');});});";
            //js.executeAsyncScript("var callback = arguments[arguments.length - 1];"+"callback("+JavascriptTodrag+");");  
            js.ExecuteScript(JavascriptTodrag);
            System.Threading.Thread.Sleep(1000);
        }
        /*  
         * xdistance : +ve integer to move right and -ve integer to move left  
         * ydistance : +ve integer to move down and -ve integer to move up  
         */
        public void drag(String jqueryToElement, int xdistance, int ydistance)
        {
            includeYUIlibrary();
            /*  
             node.simulateGesture("move", {  
      path: {  
        xdist: 1500,  
        ydist: 0  
      } ,  
      duration: 1000  
    });  
        */
            //String JavascriptTodrag = "YUI().use('node-event-simulate', function(Y){var node = Y.one('"+jqueryToElement+"');node.simulateGesture('move',{path:{xdist:"+xdistance+",ydist:"+ydistance+"},duration:2000});});";  
            String JavascriptTodrag = "YUI().use('node-event-simulate', function(Y){var node = Y.one('" + jqueryToElement + "');node.simulateGesture('move',{path:{xdist:" + xdistance + ",ydist:" + ydistance + "},duration:2000}, function() {Y.log('I was called.');});});";
            //js.executeAsyncScript("var callback = arguments[arguments.length - 1];"+"callback("+JavascriptTodrag+");");  
            js.ExecuteScript(JavascriptTodrag);
            System.Threading.Thread.Sleep(1000);
        }
        /**  
         *   
         * @param jqueryToElement - String   
         * @param axis - Character X or Y in caps  
         * @param distance  
         * @param milliseconds  
         */
        public void flick(String jqueryToElement, char axis, int distance, long milliseconds)
        {
            includeYUIlibrary();
            /*  
             node.simulateGesture("flick", {  
      axis: y  
      distance: -100   
      duration: [seconds]  
         });  
            */
            String direction = ("" + axis).ToUpper();
            String JavascriptToflick = "YUI().use('node-event-simulate', function(Y){var node = Y.one('" + jqueryToElement + "');node.simulateGesture('flick',{axis:" + direction + ",distance:" + distance + ",duration:" + milliseconds + "});});";
            js.ExecuteScript(JavascriptToflick);
            System.Threading.Thread.Sleep(1000);
        }
        /**  
         *   
         * @param jqueryToElement  
         * @param axis - axis of movement X or Y  
         * @param distance  
         */
        public void flick(String jqueryToElement, char axis, int distance)
        {
            includeYUIlibrary();
            /*  
             node.simulateGesture("flick", {  
      axis: y  
      distance: -100   
      duration: 50  
         });  
            */
            String direction = ("" + axis).ToUpper();
            String JavascriptToflick = "YUI().use('node-event-simulate', function(Y){var node = Y.one('" + jqueryToElement + "');node.simulateGesture('flick',{axis:" + direction + ",distance:" + distance + ",duration:2000});});";
            js.ExecuteScript(JavascriptToflick);
            System.Threading.Thread.Sleep(1000);
        }
        /**  
         *   
         * @param jqueryToElement  
         * @param startCircleRadius   
         * @param endCircleRadius  
         * (startCircleRadius < endCircleRadius) - spread(zoom in)  
         * (startCircleRadius > endCircleRadius) - pinch(zoom out)  
         * @param milliseconds  
         */
        public void pinch(String jqueryToElement, int startCircleRadius, int endCircleRadius, long milliseconds)
        {
            includeYUIlibrary();
            /*  
             //simulate a pinch: "r1" and "r2" are required  
         node.simulateGesture("pinch", {  
      r1: 100, // start circle radius at the center of the node  
      r2: 50,  // end circle radius at the center of the node  
      duration : [milliseconds]  
         });  
         //simulate a spread: same as "pinch" gesture but r2>r1  
         node.simulateGesture("pinch", {  
      r1: 50,  
      r2: 100,duration : [milliseconds]  
         });  
            */
            int r1 = startCircleRadius;
            int r2 = endCircleRadius;
            String JavascriptTopinch = "YUI().use('node-event-simulate', function(Y){var node = Y.one('" + jqueryToElement + "');node.simulateGesture('pinch',{r1:" + r1 + ",r2:" + r2 + ",duration:" + milliseconds + "});});";
            js.ExecuteScript(JavascriptTopinch);
            System.Threading.Thread.Sleep(1000);
        }
        /**  
         *   
         * @param jqueryToElement  
         * @param startCircleRadius   
         * @param endCircleRadius  
         * (startCircleRadius < endCircleRadius) - spread(zoom in)  
         * (startCircleRadius > endCircleRadius) - pinch(zoom out)  
         */
        public void pinch(String jqueryToElement, int startCircleRadius, int endCircleRadius)
        {
            includeYUIlibrary();
            /*  
             //simulate a pinch: "r1" and "r2" are required  
         node.simulateGesture("pinch", {  
      r1: 100, // start circle radius at the center of the node  
      r2: 50  // end circle radius at the center of the node  
         });  
         //simulate a spread: same as "pinch" gesture but r2>r1  
         node.simulateGesture("pinch", {  
      r1: 50,  
      r2: 100  
         });  
            */
            int r1 = startCircleRadius;
            int r2 = endCircleRadius;
            String JavascriptTopinch = "YUI().use('node-event-simulate', function(Y){var node = Y.one('" + jqueryToElement + "');node.simulateGesture('pinch',{r1:" + r1 + ",r2:" + r2 + "});});";
            js.ExecuteScript(JavascriptTopinch);
            System.Threading.Thread.Sleep(1000);
        }
        public static void main(String[] args)
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://www.applicationundertest.com");
            MyTouchActions touchAction = new MyTouchActions(driver);
            touchAction.tap("#element");
            //tap and hold for 3 secs  
            touchAction.tap("#element", 3000);
            //drag 100pt right  
            touchAction.drag("#Canvas", 100, 0);
            //drag 200pt towards top over period of 3 secs   
            touchAction.drag("#Canvas", 0, -200, 3000);
            //flick in horizontal direction  
            touchAction.flick("#canvas", 'X', 100);
            //pinch(r1>r2) pinch action  
            touchAction.pinch("#canvas", 100, 50);
            //pinch(r1<r2) spread action  
            touchAction.pinch("#canvas", 50, 100);
        }
    }  
}
