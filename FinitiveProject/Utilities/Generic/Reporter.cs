using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using SeleniumAutomation.Utilities;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using System.Reflection;

namespace SpecFlow.Generic
{
    public class Reporter : AutomationUtilities
    {
        public static TextWriter writer;
        public static string FullPath;
        public static int counter = 1;

        /// <summary>
        /// Creates the custom report folder and returns the path
        /// </summary>
        /// <returns></returns>
        public string CreateReportsDirectory()
        {
            string date = DateTime.Now.ToString("MM-dd-yy");
            Uri uri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string path = Directory.GetParent(uri.LocalPath).Parent.Parent.Parent.FullName;
            path = Path.Combine(path, "Automation_Report");
            path = Path.Combine(path, "CustomReport");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (string dir in Directory.GetDirectories(path))
            {
                if (!dir.Contains(date))
                {
                    Directory.Delete(dir, true);
                }
            }
            path = Path.Combine(path, "Reports_On_" + date);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FullPath = path;
            log.Info("===>Created the customer reports path is:=>" + FullPath);
            return FullPath;
        }

        /// <summary>
        /// Creates the Html file with header
        /// </summary>
        public void CreateHtmlHeader(string FeatureName)
        {
            string path = CreateReportsDirectory();
            path = Path.Combine(path, "Report_For_" + FeatureName + ".html");
            FileInfo f = new FileInfo(path);
            if (f.Exists)
            {
                f.Delete();
            }
            writer = new StreamWriter(path, true);
            writer.Write("<html>");
            writer.Write("<head>");
            writer.Write("<title>Report_for_" + FeatureName + "</title>");
            writer.Write("<style>.datagrid table { border-collapse: collapse; text-align: left;} .datagrid {font: normal 12px/150% Arial, Helvetica, sans-serif; background: #fff; overflow: hidden; border: 1px solid #006699; -webkit-border-radius: 3px; -moz-border-radius: 3px; border-radius: 3px; }.datagrid table td, .datagrid table th { padding: 3px 10px; }.datagrid table thead th {background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #006699), color-stop(1, #00557F) );background:-moz-linear-gradient( center top, #006699 5%, #00557F 100% );filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#006699', endColorstr='#00557F');background-color:#006699; color:#ffffff; font-size: 15px; font-weight: bold; border-left: 1px solid #0070A8; } .datagrid table thead th:first-child { border: none; }.datagrid table tr td { color: #00496B; border-left: 1px solid #E1EEF4;font-size: 12px;font-weight: normal; }.datagrid table tbody .alt td { background: #E1EEF4; color: #00496B; }.datagrid table tbody td:first-child { border-left: none; }.datagrid table tr:last-child td { border-bottom: none; }.datagrid table tfoot td div { border-top: 1px solid #006699;background: #E1EEF4;} .datagrid table tfoot td { padding: 0; font-size: 12px } .datagrid table tfoot td div{ padding: 2px; }.datagrid table tfoot td ul { margin: 0; padding:0; list-style: none; text-align: right; }.datagrid table tfoot  li { display: inline; }.datagrid table tfoot li a { text-decoration: none; display: inline-block;  padding: 2px 8px; margin: 1px;color: #FFFFFF;border: 1px solid #006699;-webkit-border-radius: 3px; -moz-border-radius: 3px; border-radius: 3px; background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #006699), color-stop(1, #00557F) );background:-moz-linear-gradient( center top, #006699 5%, #00557F 100% );filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#006699', endColorstr='#00557F');background-color:#006699; }.datagrid table tfoot ul.active, .datagrid table tfoot ul a:hover { text-decoration: none;border-color: #006699; color: #FFFFFF; background: none; background-color:#00557F;}div.dhtmlx_window_active, div.dhx_modal_cover_dv { position: fixed !important; }</style>");
            writer.Write("<style type=" + "text/css" + ">body { font-family: Verdana, Arial, sans-serif; font-size: 12px; }#placeholder { width: 1400px; height: 300px; }.legend table, .legend > div { height: 82px !important; opacity: 1 !important; left: 170px; top: 10px; width: 116px !important; }.legend table { border: 1px solid #555; padding: 5px; }</style>");
            // writer.Write("<script type=" + "text/javascript" + " language=" + "javascript" + " src=" + flot1 + "></script><script type=" + "text/javascript" + " language=" + "javascript" + " src=" + flot2 + "></script><script type=" + "text/javascript" + " language=" + "javascript" + " src=" + flot3 + "></script>");

            writer.Write("</head>");
            writer.Write("<body>");
            writer.Write("<img  src=" + @"http://zenq.com/Portals/0/logo.png" + " align=middle>" + "<p align=center style=" + "color:#0b6690; font-size:30px;" + "><font size='5px'> RESULTS SUMMARY </font></p>");


            writer.Write("<div class=\"datagrid\">");

            writer.Write("<table cellpadding=1 cellspacing=1 align='center'>");
            writer.Write("<tr style='border: 1px solid black;width:100%'>");
            writer.Write("<thead><th align=center><b><font color='white'>Execution Environment Details</font></b></th></thead></tr>");
            writer.Write("</table>");
            writer.Write("<table cellpadding=1 cellspacing=1 align='center'>");
            writer.Write("<tr style='border: 1px solid black;width:50%'>");
            writer.Write("<td align=left style='width:25%'><b><font color='#3A5F89'>Platform:</font></b></td>");
            writer.Write("<td align=left style='width:25%'><b><font color='#3A5F89'>" + Environment.OSVersion + "</font></b></td>");
            writer.Write("</tr>");
            writer.Write("</table></div>");
            writer.Write("</br>");
            writer.Write("<div class=\"datagrid\">");
            writer.Write("<table cellpadding=1 cellspacing=1 width='100%' align='center'><tr>");
            writer.Write("<thead><tr bgcolor='#3A5F89' style='border: 1px solid black;'>");
            writer.Write("<th align=center  style='width:15%'><b><font color='white'>Test Suite</font></b></th>");
            writer.Write("<th align=center  style='width:20%''><b><font color='white'>Test Case</font></b></th>");
            writer.Write("<th align=center  style='width:10%'><b><font color='white'>Test Status</font></b></th>");
            writer.Write("<th align=center   style='width:2%'><b><font color='white'>Screenshot</font></b></th>");
            writer.Write("<th align=center  style='width:27%'><b><font color='white'>Error</font></b></th>");
            writer.Write("</tr></thead><tbody>");
            writer.Flush();
        }

        /// <summary>
        /// Writes the data into html file
        /// </summary>
        /// <param name="TestSuite"></param>
        /// <param name="TestCase"></param>
        /// <param name="TestStatus"></param>
        /// <param name="Error"></param>
        public void ReporterHtmlData(string TestSuite, string TestCase, string TestStatus, string Error, string ScreenshotPath, string VideoPath)
        {
            log.Info("writing data.................");
            if (Reporter.counter % 2 == 0)
            {
                writer.Write("<tr>");
                writer.Write("<td align=center><b><font color='#3A5F89'>" + TestSuite + "</font></b></td>");
                writer.Write("<td align=center><b><font color='#3A5F89'>" + TestCase + "</font></b></td>");

            }
            else
            {
                writer.Write("<tr class=\"alt\">");
                writer.Write("<td align=center><b><font color='#3A5F89'>" + TestSuite + "</font></b></td>");
                writer.Write("<td align=center><b><font color='#3A5F89'>" + TestCase + "</font></b></td>");


            }
            if (TestStatus == "Failed")
            {
                writer.Write("<td align=center><b><font color='#CC0000'> Failed </font></b></td>");
                writer.Write("<td align=center><b><font color='#3A5F89'><a href='file:///" + ScreenshotPath + "'>Screenshot</a></font></b></td>");
                writer.Write("<td  align=left><b><font color=blue><details><summary>" + "StackTrace" + "</summary><p>" + Error + "</p></details></td>");

            }
            else if (TestStatus == "Success")
            {
                writer.Write("<td align=center><b><font color='#1d9d01'> Passed </font></b></td>");
                writer.Write("<td align=center><b><font color='#3A5F89'>" + ScreenshotPath + "</font></b>");
                writer.Write("<td align=center><b><font color='#3A5F89'>" + " " + "</font></b></td>");
            }
            writer.Write("</tr>");
            writer.Flush();
            Reporter.counter += 1;
        }

        public void report_tests_count(float total_tests, float failed_tests, string log_path)
        {

            if (Reporter.counter % 2 == 0)
            {
                //writer.Write("<html>");
                //writer.Write("<head>");
                //writer.Write("<title>Overall Test Status</title>");
                //writer.Write("<body>");

                //writer.Write("<style>.datagrid table { border-collapse: collapse; text-align: left;} .datagrid {font: normal 12px/150% Arial, Helvetica, sans-serif; background: #fff; overflow: hidden; border: 1px solid #006699; -webkit-border-radius: 3px; -moz-border-radius: 3px; border-radius: 3px; }.datagrid table td, .datagrid table th { padding: 3px 10px; }.datagrid table thead th {background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #006699), color-stop(1, #00557F) );background:-moz-linear-gradient( center top, #006699 5%, #00557F 100% );filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#006699', endColorstr='#00557F');background-color:#006699; color:#ffffff; font-size: 15px; font-weight: bold; border-left: 1px solid #0070A8; } .datagrid table thead th:first-child { border: none; }.datagrid table tr td { color: #00496B; border-left: 1px solid #E1EEF4;font-size: 12px;font-weight: normal; }.datagrid table tbody .alt td { background: #E1EEF4; color: #00496B; }.datagrid table tbody td:first-child { border-left: none; }.datagrid table tr:last-child td { border-bottom: none; }.datagrid table tfoot td div { border-top: 1px solid #006699;background: #E1EEF4;} .datagrid table tfoot td { padding: 0; font-size: 12px } .datagrid table tfoot td div{ padding: 2px; }.datagrid table tfoot td ul { margin: 0; padding:0; list-style: none; text-align: right; }.datagrid table tfoot  li { display: inline; }.datagrid table tfoot li a { text-decoration: none; display: inline-block;  padding: 2px 8px; margin: 1px;color: #FFFFFF;border: 1px solid #006699;-webkit-border-radius: 3px; -moz-border-radius: 3px; border-radius: 3px; background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #006699), color-stop(1, #00557F) );background:-moz-linear-gradient( center top, #006699 5%, #00557F 100% );filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#006699', endColorstr='#00557F');background-color:#006699; }.datagrid table tfoot ul.active, .datagrid table tfoot ul a:hover { text-decoration: none;border-color: #006699; color: #FFFFFF; background: none; background-color:#00557F;}div.dhtmlx_window_active, div.dhx_modal_cover_dv { position: fixed !important; }</style>");

                writer.Write("<table>");
                writer.Write("<tr>");
                writer.Write("<td>");

                // writer.Write("<button style='background-color:#0b6690;width:150%;padding:5px;' ><a href=" + @"file:///C:\Users\Test\Desktop\CodedUIApril12\TestResults\AutomationReports\Log\log_03-05-2016.log" + " ><font color='white'>Log Output</font></a></button>");
                writer.Write("<button style='background-color:#0b6690;width:150%;padding:5px;' ><a href=" + log_path + " ><font color='white'>Log Output</font></a></button>");
                writer.Write("</td>");
                writer.Write("</tr>");
                writer.Write("</table>");



                writer.Write("</div>");
                writer.Write("<h2>Overall Test Report<h2>");
                writer.Write("<div class=\"datagrid\">");
                // writer.Write("<th align=center bgcolor='#0b6690' style='color:white;'>Overall Test Status</th>");
                writer.Write("<table cellpadding=1 cellspacing=1 width='50%' align='center' border='2px'>");

                writer.Write("<tr>");
                writer.Write("<thead><tr bgcolor='#3A5F89' style='border: 1px solid black;'>");
                writer.Write("<th align=center  style='width:5%'><b><font color='white'>Total Tests</font></b></th>");
                writer.Write("<th align=center  style='width:5%''><b><font color='white'>Passed Tests</font></b></th>");
                writer.Write("<th align=center  style='width:5%''><b><font color='white'>Failed Tests</font></b></th>");
                writer.Write("<th align=center  style='width:5%''><b><font color='white'>Pass Rate</font></b></th>");
                writer.Write("<th align=center  style='width:5%''><b><font color='white'>Fail Rate</font></b></th>");
                writer.Write("<tr>");
                writer.Write("<tr>");
                writer.Write("<td align=center bgcolor ='#0b6690'><b><font color='black'>  " + total_tests + "</font></b></td>");
                writer.Write("<td align=center  bgcolor ='#88ee88'><b><font color='black'> " + (total_tests - failed_tests) + "</font></b></td>");
                writer.Write("<td align=center  bgcolor ='#ff8888'><b><font color='black'> " + failed_tests + "</font></b></td>");
                writer.Write("<td align=center  bgcolor ='white'><b><font color='black'> " + string.Format("{0:0.00}", (((total_tests - failed_tests) / total_tests) * 100)) + "%" + "</font></b></td>");
                writer.Write("<td align=center  bgcolor ='white'><b><font color='black'> " + string.Format("{0:0.00}", ((failed_tests / total_tests) * 100)) + "%" + "</font></b></td>");
                writer.Write("</tr>");
                writer.Write("</table>");
                writer.Write("</div>");
                writer.Write("<div id=\"placeholder\"></div>");
                //writer.Write("</body>");
                //writer.Write("</head>");
                //writer.Write("</html>");
            }
            else
            {
                writer.Write("<table>");
                writer.Write("<tr>");
                writer.Write("<td>");
                writer.Write("<button style='background-color:#0b6690;width:150%;padding:5px;' ><a href=" + log_path + " ><font color='white'>Log Output</font></a></button>");
                writer.Write("</td>");
                writer.Write("</tr>");
                writer.Write("</table>");
                writer.Write("</div>");
                writer.Write("<h2>Overall Test Report<h2>");
                writer.Write("<div class=\"datagrid\">");
                writer.Write("<table cellpadding=1 cellspacing=1 width='50%' align='center' border='2px'>");
                writer.Write("<tr>");
                writer.Write("<thead><tr bgcolor='#3A5F89' style='border: 1px solid black;'>");
                writer.Write("<th align=center  style='width:5%'><b><font color='white'>Total Tests</font></b></th>");
                writer.Write("<th align=center  style='width:5%''><b><font color='white'>Passed Tests</font></b></th>");
                writer.Write("<th align=center  style='width:5%''><b><font color='white'>Failed Tests</font></b></th>");
                writer.Write("<th align=center  style='width:5%''><b><font color='white'>Pass Rate</font></b></th>");
                writer.Write("<th align=center  style='width:5%''><b><font color='white'>Fail Rate</font></b></th>");
                writer.Write("<tr>");
                writer.Write("<td align=center bgcolor ='#0b6690'><b><font color='black'> " + total_tests + "</font></b></td>");
                writer.Write("<td align=center bgcolor ='#88ee88'><b><font color='black'>  " + (total_tests - failed_tests) + "</font></b></td>");
                writer.Write("<td align=center bgcolor ='#ff8888'><b><font color='black'> " + failed_tests + "</font></b></td>");
                writer.Write("<td align=center  bgcolor ='white'><b><font color='black'> " + string.Format("{0:0.00}", (((total_tests - failed_tests) / total_tests) * 100)) + "%" + "</font></b></td>");
                writer.Write("<td align=center  bgcolor ='white'><b><font color='black'> " + string.Format("{0:0.00}", ((failed_tests / total_tests) * 100)) + "%" + "</font></b></td>");
                writer.Write("</tr>");
                writer.Write("</table>");
                writer.Write("</div>");
                writer.Write("<div id=\"placeholder\"></div>");
            }
            writer.Flush();
        }

        /// <summary>
        /// Closes the file stream
        /// </summary>
        public void CloseFileStream()
        {
            writer.Write("</tbody></table>");
            writer.Write("</div>");
            writer.Write("</body>");
            writer.Write("</html>");
            writer.Close();
        }

        /// <summary>
        /// Creates videos directory if not exist and stores the videos in the folder
        /// </summary>
        /// <returns></returns>
        public string CreateVideoDirectory()
        {
            string date = DateTime.Now.ToString("MM-dd-yy");
            string path = new AutomationUtilities().GetProjectLocation();
            path = Path.Combine(path, "Videos");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (string dir in Directory.GetDirectories(path))
            {
                if (!dir.Contains(date))
                {
                    Directory.Delete(dir, true);
                }
            }
            path = Path.Combine(path, "Videos_On_" + date);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                log.Info("===>Directory created:=>" + path);
            }
            return path;
        }

        /// <summary>
        /// Creates the screenshots folder and stores the screenshots
        /// </summary>
        /// <returns></returns>
        public string CreateScreenShotsFolder()
        {
            string date = DateTime.Now.ToString("MM-dd-yy");
            Uri uri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string path = Directory.GetParent(uri.LocalPath).Parent.Parent.Parent.FullName;
            path = Path.Combine(path, "Automation_Report");
            path = Path.Combine(path, "OutPut");
            path = Path.Combine(path, "screenshots");
            log.Info("screenshots folder path:=>" + path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (string dir in Directory.GetDirectories(path))
            {
                if (!dir.Contains(date))
                {
                    Directory.Delete(dir, true);
                }
            }
            //path = Path.Combine(path, "screenshots_On_" + date);
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //    log.Info("===>Directory created:=>" + path);
            //}
            return path;
        }

        /// <summary>
        /// Takes screenshot
        /// </summary>
        /// <param name="newpath"></param>
        /// <param name="Driver"></param>
        public void TakeScreenShot(string newpath, IWebDriver Driver)
        {
            FileInfo f = new FileInfo(newpath);
            if (f.Exists)
            {
                f.Delete();
            }
            try
            {
                Screenshot ss = ((ITakesScreenshot)Driver).GetScreenshot();
                ss.SaveAsFile(newpath, ScreenshotImageFormat.Png);
            }
            catch (Exception)
            {

            }
            log.Info("Screen shot is saved in Path:=>" + newpath);
        }

        /// <summary>
        /// Deletes the unwanted file
        /// </summary>
        public static void DeleteUnwantedFile()
        {
            DirectoryInfo dir = new DirectoryInfo(FullPath);
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (file.Name.Contains("ReportCreation"))
                {
                    file.Delete();
                }
            }
        }

        /// <summary>
        /// Gets the key from the properties file
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        //public static string getProperty(string Key,string FilePath)
        //{
        //    // string propertiesfilename= @"readFile.properties";          
        //    string[] lines = System.IO.File.ReadAllLines(FilePath);
        //    for (int i = 0; i < lines.Length; i++)
        //    {
        //        string FileKey = Regex.Split(lines[i], "=")[0].Trim();
        //        if (FileKey == Key)
        //            return Regex.Split(lines[i], "=")[1].Trim();
        //    }
        //    return "-";
        //}

        /// <summary>
        /// Sets the value for required key in the given file
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <param name="FilePath"></param>
        public static void SetProperty(string Key, string Value, string FilePath)
        {
            bool FoundFlag = false;
            string[] LineArray = System.IO.File.ReadAllLines(FilePath);
            List<string> Lines = LineArray.ToList<string>();
            string Line = "";
            for (int i = 0; i < Lines.Count; i++)
            {
                Line = Lines[i].Trim();
                string FileKey = Regex.Split(Line, "=")[0].Trim();
                if (FileKey.Equals(Key))
                {
                    log.Info("===>Key Found:=>" + FileKey);
                    int index = Line.IndexOf('=');
                    string PropertyValue = Line.Substring(index + 1).Trim();
                    Line = Line.Replace(PropertyValue, Value);
                    FoundFlag = true;
                    Lines.RemoveAt(i);
                    Lines.Insert(i, Line);
                    break;
                }
            }
            if (FoundFlag)
            {
                System.IO.File.WriteAllLines(FilePath, Lines);
            }
            if (!FoundFlag)
            {
                log.Info("===>Key not found in properties" + Key);
                StreamWriter Writer = new StreamWriter(FilePath, true);
                Writer.Write("\n");
                Writer.WriteLine(Key + "=" + Value);
                Writer.Close();
            }
        }

        /// <summary>
        /// Creates properties file for all the suites
        /// </summary>
        public static string CreatePropertiesFile()
        {
            string path = new AutomationUtilities().GetProjectLocation();
            path = Path.Combine(path, "Resources");
            path = Path.Combine(path, "Reporter.properties");
            FileInfo f = new FileInfo(path);
            if (!f.Exists)
            {
                f.Create();
            }
            return path;
        }
    }
}
