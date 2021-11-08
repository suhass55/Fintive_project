using log4net;
using Newtonsoft.Json.Linq;
using Finitive.API;
using SeleniumAutomation.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OneAtmosphere.Utilities.Generic
{
    public class CommonUtilities
    {
        /// <summary>
        /// Returns system Downloads folder
        /// </summary>
        /// <returns></returns>
        public static string GetDownloadsDirectory()
        {
            string downloads;
            SHGetKnownFolderPath(KnownFolder.Downloads, 0, IntPtr.Zero, out downloads);
            return downloads;
        }

        /// <summary>
        /// This method is for creating new directory by providing directory path as a parameter
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="subdir"></param>
        public static void CreateDirectory(string dirpath)
        {
            if (!Directory.Exists(dirpath))
            {
                Directory.CreateDirectory(dirpath);
                Console.WriteLine("Dtrectory " + dirpath + " created ...");
            }
            else
            {
                CleanDirectory(dirpath);
            }
        }
        /// <summary>
        /// This method is to clearing files and folders from directory
        /// </summary>
        /// <param name="path"></param>
        public static void CleanDirectory(string path)
        {

            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        /// <summary>
        /// This method is to delete the files in directory
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileSubString"></param>
        /// <param name="filetype"></param>
        public static void DeleteFileInDirectory(string path, string fileSubString, string filetype)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileSystemInfo file in di.GetFileSystemInfos())
            {
                if (file.Name.Contains(fileSubString) && file.Name.EndsWith(filetype))
                {
                    file.Delete();
                    Console.WriteLine("File " + fileSubString + filetype + " is deleted from Directory " + path);
                }
            }
        }
        /// <summary>
        /// sourcepath: Path where file exists
        /// taggetpath: Path where file needs to be moved
        /// filename: the file which needs to be moved
        /// </summary>
        /// <param name="sourcepath"></param>
        /// <param name="targetpath"></param>
        /// <param name="filename"></param>

        public static void MoveFileToSubfolder(string sourcepath, string targetpath, string filename)
        {


            string sourceFile = System.IO.Path.Combine(sourcepath, filename);
            string destFile = System.IO.Path.Combine(targetpath, filename);

            Console.WriteLine("sourceFile : " + sourceFile);
            Console.WriteLine("destFile : " + destFile);

            System.IO.File.Move(sourceFile, destFile);
            Console.WriteLine("File: " + filename + "moved from " + sourcepath + "to " + targetpath);

        }

        /// <summary>
        /// This method is to renaming the file name in directory
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        /// <param name="newname"></param>
        public static void RenameFileInDirectory(string path, string filename, string newname)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            string filepath = System.IO.Path.Combine(path, filename);

            if (!File.Exists(filepath))
            {
                throw new System.Exception("File does not exist. Cannot Rename");
            }

            FileInfo[] infos = di.GetFiles();
            foreach (FileInfo f in infos)
            {
                File.Move(f.FullName, f.FullName.Replace(filename, newname));
            }

        }

        public static class KnownFolder
        {
            public static readonly Guid Downloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");
        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out string pszPath);

       
    }    
}
