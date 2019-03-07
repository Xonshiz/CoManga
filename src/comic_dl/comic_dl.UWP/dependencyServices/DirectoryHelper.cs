using comic_dl.interace;
using comic_dl.UWP.dependencyServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(DirectoryHelper))]
namespace comic_dl.UWP.dependencyServices
{
    class DirectoryHelper : IDirectory
    {
        public string documentBasePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "\\CoManga";


        public string GetBasePath()
        {
            Console.WriteLine("Base Path : " + documentBasePath);
            return documentBasePath;
        }

        public string CreateDirectory(string directoryName)
        {
            var directoryPath = documentBasePath + directoryName;
            if (!Directory.Exists(directoryPath))
            {
                try
                {
                    Directory.CreateDirectory(directoryPath);
                    return directoryPath;
                }
                catch (Exception DirectoryCreateException)
                {
                    Console.WriteLine("DirectoryCreateException : " + DirectoryCreateException);
                    return "";
                }
            }
            return directoryName;
        }

        public bool CheckExisingFile(string fileAddress)
        {
            if (File.Exists(fileAddress))
                return true;
            return false;
        }

        public bool CreateFile(string directoryName, byte[] contentToWrite)
        {
            //var filePath = Path.Combine(documentBasePath, directoryName);
            var filePath = Windows.Storage.DownloadsFolder.CreateFileAsync(directoryName);
            if (!File.Exists(Convert.ToString(filePath)))
            {
                try
                {
                    File.WriteAllBytes(directoryName, contentToWrite);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
