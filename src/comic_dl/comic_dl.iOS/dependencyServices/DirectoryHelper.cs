using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using comic_dl.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using comic_dl.interace;
using System.Threading.Tasks;

[assembly: Dependency(typeof(DirectoryHelper))]
namespace comic_dl.iOS
{
    class DirectoryHelper : IDirectory
    {
        public string documentBasePath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "/CoManga"; // Try MyPictures
        
        public string GetBasePath()
        {
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
            return directoryPath;
        }

        public bool CheckExisingFile(string fileAddress)
        {
            if (File.Exists(fileAddress))
                return true;
            return false;
        }

        public Task<bool> CreateFile(string directoryName, byte[] contentToWrite)
        {
            var filePath = documentBasePath + directoryName;
            if (!File.Exists(filePath))
            {
                try
                {
                    File.WriteAllBytes(directoryName, contentToWrite);
                    return Task.FromResult(true);
                }
                catch (Exception)
                {
                    return Task.FromResult(false);
                }
            }
            return Task.FromResult(false);
        }
    }
}