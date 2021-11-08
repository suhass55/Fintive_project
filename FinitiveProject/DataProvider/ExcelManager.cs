using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Microsoft.Office.Interop.Excel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using SeleniumAutomation.Utilities;
using excel = Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;
using workbook = Microsoft.Office.Interop.Excel.Workbook;
using worksheet = Microsoft.Office.Interop.Excel.Worksheet;
using log4net;
using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenQA.Selenium;
using SeleniumAutomation.Selenium;
using NPOI.XSSF.UserModel;

namespace SeleniumAutomation.DataProvider
{
    public class ExcelManager 
    {
        private Workbook MyBook = null;
        // private Workbook MyBook1 = null;
        private Application ExcelApp = null;
        public static ILog log4Net;
        public BalloonPopuUp balloon;
        private Worksheet MySheet = null;
        string excelPath;
        string fPath;
        AutomationUtilities _autoutilities = new AutomationUtilities();
        ILog log = LogManager.GetLogger("Gmail_Parts");

        /// <summary>
        /// Method for getting the Path of data file
        /// </summary>
        /// <params>None</params>
        /// <return>String</returns>

        public string getPath()
        {
            fPath = _autoutilities.GetTestDataLocation() + "\\Selenium_Automation\\TestData\\GmailCredentials.xlsx";
            return fPath;
        }

        public ExcelManager()
          

        {
           
        }

        /// <summary>
        /// Parameterized Constructor with the SheetName 
        /// </summary>
        /// <params>Sheetname as String</params>
        /// <return>none</returns>

        public ExcelManager(string sheetName)
        {
            _autoutilities = new AutomationUtilities();
            excelPath = _autoutilities.GetKeyValue("INPUTDATAPATH", "ExcelDataPath");
            ExcelApp = new Application();
            ExcelApp.Visible = true;
            MyBook = ExcelApp.Workbooks.Open(excelPath);
            MySheet = (Worksheet)MyBook.Worksheets.get_Item(sheetName); // Explicit cast is not required here

        }




        /// <summary>
        /// Parameterized Constructor with SheetName and ExcelDataPath
        /// </summary>
        /// <params>Sheetname as String and Excel Path as String</params>
        /// <return>none</returns>

        public ExcelManager(string excelPath, string sheetName)
        {
            ExcelApp = new Application();
            ExcelApp.Visible = true;
            ExcelApp.DisplayAlerts = true;
            ExcelApp.EnableEvents = true;
            ExcelApp.IgnoreRemoteRequests = true;
            ExcelApp.ScreenUpdating = true;
            MyBook = ExcelApp.Workbooks.Open(excelPath, Microsoft.Office.Interop.Excel.XlCorruptLoad.xlExtractData);
            MyBook.RefreshAll();
            var xlsxFile = excelPath + "x";
            MyBook.SaveAs(Filename: xlsxFile, FileFormat: Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel7);
            MySheet = (Worksheet)MyBook.Worksheets.get_Item(sheetName); // Explicit cast is not required here                  
        }
        public string CovertExcellFromat(string excelPath)
        {
            var xlsxFile = excelPath + "x";
            MyBook.SaveAs(Filename: xlsxFile, FileFormat: Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel7);
            return xlsxFile;
        }


        /// <summary>
        ///Set up for Row count
        /// </summary>
        /// <return>type Int</returns>

        public Int32 RowCount
        {
            get { return MySheet.UsedRange.Rows.Count; }
        }



        /// <summary>
        ///Set up for column count
        /// </summary>
        /// <return>type Int</returns>

        public Int32 ColumnCount
        {
            get { return MySheet.UsedRange.Columns.Count; }
        }



        /// <summary>
        /// Method for geting the data 
        /// </summary>
        /// <params>Row number and Coloumn name</params>
        /// <return>String</returns>
        public string GetData(int row, string columnName)
        {
            int column = -1;
            for (int i = 1; i <= ColumnCount; i++)
            {
                if (((Range)MySheet.UsedRange.Cells[1, i]).Value2.ToString().Trim().Equals(columnName))
                {
                    column = i;
                    break;
                }
            }
            if (column == -1)
            {
                Assert.Fail(MyBook.Name + " - could not find text '" + columnName + "' in the excel sheet " + MySheet.Name);
            }
            return ((Range)MySheet.Cells[row, column]).Value2.ToString().Trim();
        }



        /// <summary>
        /// Method for geting the data  
        /// </summary>
        /// <params>Row number and Coloumn number</params>
        /// <return>String</returns>
        public string GetData(int row, int column)
        {
            return ((Range)MySheet.UsedRange.Cells[row, column]).Value2.ToString().Trim();
        }



        /// <summary>
        /// Method for setting the value in Excel with the provided data
        /// </summary>
        /// <params>Rownumber,Column Name and the value which is to be set </params>
        /// <return>none</returns>
        public void SetData(int row, string columnName, Object value)
        {
            int column = -1;
            for (int i = 1; i <= ColumnCount; i++)
            {
                if (((Range)MySheet.UsedRange.Cells[1, i]).Value2.ToString().Trim().Equals(columnName))
                {
                    column = i;
                    break;
                }
            }
            if (column == -1)
            {
                Assert.Fail(MyBook.Name + " - could not find text '" + columnName + "' in the excel sheet " + MySheet.Name);
            }
            ((Range)MySheet.Cells[row, column]).Value2 = value;
            MyBook.Save();
        }



        /// <summary>
        ///  Method for setting the value in the excel 
        /// </summary>
        /// <params>Rownumber,ColumnNumber,Value which is to be added in the excel</params>
        /// <return>none</returns>

        public void SetData(int row, int column, Object value)
        {
            ((Range)MySheet.Cells[row, column]).Value2 = value;
            MyBook.Save();
        }



        /// <summary>
        ///  Method for quiting the Excel 
        /// </summary>
        /// <params>none </params>
        /// <return>none</returns>

        public void Quit()
        {
            ExcelApp.Quit();
            ExcelApp = null;
        }



        /// <summary>
        /// Method for getting the data by Row
        /// </summary>
        /// <params>SheetName,RowNumber,String FilePath=null </params>
        /// <return>String array</returns>

        public string[] GetRowDataFromExcel(string sheetName, int rowNum, string filePath = null)
        {
            try
            {
                if (filePath == null) filePath = getPath();

                log4Net.Info(filePath);
                List<string> al = new List<string>();
                string data;
                FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                file.Close();
                ISheet sheet = hssfworkbook.GetSheet(sheetName);

                IRow r = sheet.GetRow(rowNum);
                for (int i = 0; i < r.LastCellNum; i++)
                {
                    data = r.GetCell(i).ToString();
                    al.Add(data);
                }

                return al.ToArray();

            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException();
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// <summary>
        /// Method for writing data to the excel by providing the SheetName,Header Name ,Value to be set,Row number and filePath=null
        /// </summary>
        /// <params>SheetName,HeaderName,Value to be written,rowNumber,Path of file </params>
        /// <return>void</returns>

        public void WriteDataToExcel(string sheetName, string headerName, string setValue, int rowNum, string filePath = null)
        {

            try
            {
                if (filePath == null || filePath == "")
                    filePath = getPath();
                FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);

                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                file.Close();
                ISheet sheet = hssfworkbook.GetSheet(sheetName);
                IRow actualRow = sheet.GetRow(rowNum);
                IRow headerRow = sheet.GetRow(0);

                int i = 0;
                bool flag = false;
                for (i = 0; i < headerRow.LastCellNum; i++)
                {
                    if (headerRow.GetCell(i).ToString().ToLower() == headerName.ToLower())
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                    throw new Exception(headerName + " header name not present in " + filePath + " file");
                actualRow.GetCell(i).SetCellValue(setValue);

                sheet.ForceFormulaRecalculation = true;
                file = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                hssfworkbook.Write(file);
                file.Close();

            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException();
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// <summary>
        /// method for getting the data from the excel sheet by providing the Sheetname,HeaderName,RowNumber,and filePath=null
        /// </summary>
        /// <params>SheetName,headerName,Rownumber,Filepath=null</params>
        /// <return>String </returns>

        public string GetDataFromExcelSheet(string sheetName, string headerName, int rowNum, string filePath)
        {
            string result;
            try
            {
                FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                XSSFWorkbook hssfworkbook = new XSSFWorkbook(file);
                file.Close();
                ISheet sheet = hssfworkbook.GetSheet(sheetName);
                IRow actualRow = sheet.GetRow(rowNum);
                IRow headerRow = sheet.GetRow(0);
                int i;
                bool flag = false;
                for (i = 0; i < headerRow.LastCellNum; i++)
                {
                    if (headerRow.GetCell(i).ToString().ToLower() == headerName.ToLower())
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                    throw new Exception(headerName + " header not present in " + sheetName + " sheet");
                result = actualRow.GetCell(i).ToString();
                sheet.ForceFormulaRecalculation = true;
                return result;
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException();
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Read the specified property row from Excel  (searches the property name in the first column and gets the entire row of the property)
        /// </summary>
        /// <params>sheetName, property, filePath(optional)</params>
        /// <return>String array which contains the data of specified property row </returns>
        public string[] GetRowDataFromExcel(string sheetName, string property, string filePath = null)
        {
            try
            {
                if (filePath == null) filePath = getPath();
                List<string> al = new List<string>();
                string data;
                FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                file.Close();
                ISheet sheet = hssfworkbook.GetSheet(sheetName);
                var x = sheet.GetRowEnumerator();
                int row = 0;
                while (x.MoveNext())
                {
                    IRow r = (IRow)x.Current;
                    if (r.GetCell(0).ToString().Trim().ToLower() == property.Trim().ToLower())
                    {
                        for (int i = 0; i < r.LastCellNum; i++)
                        {
                            data = r.GetCell(i).ToString();
                            al.Add(data);

                        }
                        break;
                    }
                    row++;
                }

                return al.ToArray();

            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException();
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
