using System;
using System.IO;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using log4net;
using MbUnit.Framework;
namespace SeleniumAutomation.DataProvider
{
	/// <summary>
	/// Description of PDFManager.
	/// </summary>
	public class PDFManager
	{
	
		ILog _log;
		public PDFManager(){
			_log = LogManager.GetLogger("PDFManager");
		}

        /// <summary>
        /// Method for extracting PDF data
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
		public string ExtractTextFromPdf(string filename){
			String text = "";
			try{
				if(checkFileExists(filename)){
					_log.Info(filename + "exists in the download folder");
					PDDocument doc = null;
					try{
						doc = PDDocument.load(getPDFFilePath(filename));
						PDFTextStripper stripper = new PDFTextStripper();
						text = stripper.getText(doc);
						
					}
					catch(Exception e){
						_log.Info("Exception in Extracting data from file "+ filename + ".pdf" + e.StackTrace);
						_log.Info("Exception in Extracting data from file "+ filename + ".pdf" + e.StackTrace);
					}
					finally{
						if(doc != null){
							doc.close();
						}
					}
				}
				else{
					Assert.Fail("PDF file not found in 'Downloads' folder.");
				}
			}
			catch(Exception e){
				_log.Info("Exception in extracting text from PDF: " + e.Message);
			}
			return text;
		}
		
		/**
		 * Returns the path of default download folder for the user
		 */ 
		public String getDownloadFolderPath(){
			string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDownload = Path.Combine(pathUser, "Downloads");
            return pathDownload;
		}
		
        /// <summary>
        /// Megthod for Getting the PDF FilePath
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
		public String getPDFFilePath(String filename){
            DirectoryInfo dir = new DirectoryInfo(getDownloadFolderPath());
            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Name.StartsWith(filename))
                {
                    filename = filename + file.Extension;
                    _log.Info("full file name is:=>" + filename);
                }
            }
			
			string filepath = Path.Combine(getDownloadFolderPath(),filename);
			return filepath;
		}
		
        /// <summary>
        /// Checks whether the given filename exists in the Download folder
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
		public bool checkFileExists(String filename){
            DirectoryInfo dir = new DirectoryInfo(getDownloadFolderPath());
            foreach (FileInfo file in dir.GetFiles())
            {   
                if (file.Name.StartsWith(filename))
                {
                	_log.Info(filename + " exists in the download folder");
                    return true;
                }
            }
            return false;
		}
	}
	
	
}
