using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using comic_dl.Droid.dependencyServices;
using comic_dl.interace;
using Xamarin.Forms;

[assembly: Dependency(typeof(DirectoryHelper))]
namespace comic_dl.Droid.dependencyServices
{
    class DirectoryHelper : IDirectory
    {
        public string documentBasePath = Android.OS.Environment.ExternalStorageDirectory + Java.IO.File.Separator + "CoManga";

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
            else
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
            var filePath = Path.Combine(documentBasePath, directoryName);
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