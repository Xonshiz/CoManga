using comic_dl.interace;
using comic_dl.UWP.dependencyServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
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

        public async Task<bool> CreateFile(string directoryName, byte[] contentToWrite)
        {
            List<string> fileNames = directoryName.Split('/').ToList();
            string downloadType = fileNames[1];
            string comicName = fileNames[2];
            string comicChapter = fileNames[3];
            string comicFileName = fileNames[4];
            StorageFile destinationFile;
            StorageFolder downloadsFolder;
            try
            {
                downloadsFolder = await DownloadsFolder.CreateFolderAsync(downloadType + @"\" + comicName + @"\" + comicChapter + @"\");
                string folderToken = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(downloadsFolder);
                ApplicationData.Current.LocalSettings.Values["folderToken"] = folderToken;
                destinationFile = await downloadsFolder.CreateFileAsync(comicFileName, CreationCollisionOption.ReplaceExisting);

                await Windows.Storage.FileIO.WriteBytesAsync(destinationFile, contentToWrite);
                return true;
                // File.WriteAllBytes(destinationFile.Path, contentToWrite);
            }
            catch (Exception)
            {
                if (ApplicationData.Current.LocalSettings.Values["folderToken"] != null)
                {
                    string token = ApplicationData.Current.LocalSettings.Values["folderToken"].ToString();
                    downloadsFolder = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
                    destinationFile = await downloadsFolder.CreateFileAsync(comicFileName, CreationCollisionOption.ReplaceExisting);
                    //File.WriteAllBytes(destinationFile.Path, contentToWrite);
                    await Windows.Storage.FileIO.WriteBytesAsync(destinationFile, contentToWrite);
                    return true;
                }
                return false;
            }
        }
    }
}
