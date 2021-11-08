using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using log4net;
using NUnit.Framework;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections;
using LumenWorks.Framework.IO.Csv;
namespace SeleniumAutomation.DataProviders
{

    public class CSVManager
    {

        static ILog Log = LogManager.GetLogger("CSVManager");


        /// <summary>
        /// Method for getting the data from CSV file
        /// </summary>
        /// <params>CSV file path </params>
        /// <returns>String list</returns>
        public static List<Dictionary<string, string>> ReadCsv(string CSVFile)
        {
            Log.Info(CSVFile);
            if (!File.Exists(CSVFile))
            {
                Log.Error("CSV File is not present - " + CSVFile);
                Assert.Fail("Input CSV file is not present - " + CSVFile);
            }

            List<Dictionary<string, string>> csvlist = new List<Dictionary<string, string>>();
            // open the file "data.csv" which is a CSV file with headers
            using (CsvReader csv =
                   new CsvReader(new StreamReader(CSVFile), true))
            {
                //csv.Delimiter = ';';
                int fieldCount = csv.FieldCount;
                Log.Info(fieldCount);
                string[] headers = csv.GetFieldHeaders();
                Log.Info(csv.HasHeaders);

                while (csv.ReadNextRecord())
                {

                    Dictionary<string, string> csvdic = new Dictionary<string, string>();
                    for (int i = 0; i < fieldCount; i++)
                    {
                        csvdic.Add(headers[i], csv[i]);
                        Log.Info(headers[i] + "," + csv[i] + ";");
                        
                    }


                    Log.Info("");
                    csvlist.Add(csvdic);
                }
            }
            return csvlist;
        }



        /// <summary>
        /// Method for replacing the text on specifying the required text to replace 
        /// </summary>
        /// <params>filepath,searchtext,replace text </params>
        /// <return>None</returns>
        public static void ReplaceInCSVFile(string filePath, string searchText, string replaceText)
        {
            StreamReader reader = new StreamReader(filePath);
            string content = reader.ReadToEnd();
            reader.Close();

            content = Regex.Replace(content, searchText, replaceText);

            StreamWriter writer = new StreamWriter(filePath);
            writer.Write(content);
            writer.Close();
        }


        /// <summary>
        /// Method for retrieve the particular value from the csv file 
        /// </summary>
        /// <params>filepath,coloumn number,row no </params>
        /// <returns>string</returns>

        public static string ReadCellValuefromCSV(string filepath, int coloumnno, int rowno)
        {
            var list = new List<string>();

            using (var rd = new StreamReader(filepath))
            {
                while (!rd.EndOfStream)
                {
                    string[] splits = rd.ReadLine().Split(',');

                    list.Add(splits[coloumnno]);


                }
                string[] arr = list.ToArray();
                Log.Info(arr[rowno]);
                return arr[rowno];
            }

        }



        void csv_ParseError(object sender, ParseErrorEventArgs e)
        {
            // if the error is that a field is missing, then skip to next line
            if (e.Error is MissingFieldCsvException)
            {
                Log.Info("--MISSING FIELD ERROR OCCURRED");
                e.Action = ParseErrorAction.AdvanceToNextLine;
            }
        }
    }
}
