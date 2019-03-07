using comic_dl.commonFunctionalities;
using comic_dl.interace;
using comic_dl.internalData;
using comic_dl.internalData.manga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace comic_dl._manga.downloader
{
    class Downloader
    {
        private string DownloadPercentage(double currentFileCount, double totalFiles)
        {
            return Convert.ToString(Convert.ToInt16((Convert.ToDouble(currentFileCount / totalFiles)) * 100));
        }
        private Task<string> GetDirectory(string mangaName, string chapterNumber)
        {
            string directoryPath = "/Manga/" + mangaName + "/" + chapterNumber;
            string baseDirectory = DependencyService.Get<IDirectory>().GetBasePath();
            return Task.FromResult(DependencyService.Get<IDirectory>().CreateDirectory(directoryPath));

        }

        public async Task<bool> FilesDownloader(List<string> listOfUrl, string mangaName, string chapterNumber, CommonInternalData.DownloadType downloadType, Xamarin.Forms.Button button)
        {
            int fileCount = 0;
            //CommonFunctions commonFunctions = new CommonFunctions();
            string baseDirectoryAddress = await GetDirectory(CommonFunctions.FileNameCleaner(mangaName), chapterNumber);

            foreach (string fileUrl in listOfUrl)
            {
                string downloadPercent = DownloadPercentage((double) fileCount, (double) listOfUrl.Count);
                button.Text = downloadPercent  + "%";

                if (!string.IsNullOrEmpty(baseDirectoryAddress)) // Make directory and it returns a boolean as a result.
                {
                    string filePath = baseDirectoryAddress + "/" + Convert.ToString(fileCount) + ".jpg";

                    bool isFilePresent = DependencyService.Get<IDirectory>().CheckExisingFile(filePath);
                    if (!isFilePresent) // File is not downloaded yet.
                    {
                        byte[] contents = null;
                        /*
                         * We can set the downloader and revert its contents to this byte[] contents and use it further.
                         */
                        if (downloadType == CommonInternalData.DownloadType.MangaRock)
                            contents = await MriFilesDownloader(baseDirectoryAddress, fileUrl, Convert.ToString(fileCount));

                        if (contents == null) // If there's nothing, just move onto the next loop.
                            continue;

                        bool isFileDownloaded = DependencyService.Get<IDirectory>().CreateFile(filePath, contents);


                        if (!isFileDownloaded)
                        {
                            CommonInternalData.failedDownloadLinks.Add(fileUrl); // If failed, add to failed download list.
                            CurrentData.response = "Download Failed";
                        }
                        else
                        {
                            CurrentData.response = "Download Completed.";
                            CurrentData.downloadPath = baseDirectoryAddress; // For opening the Explorer.
                        }
                    }
                    else
                    {
                        continue; // File is already downloaded.
                    }
                }
                else
                {
                    return false;
                }

                fileCount++;
            }

            return true;

        }
        public async Task<byte[]> MriFilesDownloader(string baseDirectoryAddress, string fileUrl, string fileCount)
        {
            #region WebClient() Implementation
            /*
             * Thanks to @Raxdiam : https://www.reddit.com/r/csharp/comments/9fs2t0/trying_to_convert_mri_files_to_a_valid_image/
             */
            Uri uri = new Uri(fileUrl);
            var client = new WebClient();
            var data = await client.DownloadDataTaskAsync(uri);

            var ln = data.Length + 7;
            var header = new byte[] {
                                82, 73, 70, 70,
                                (byte) (255 & ln),
                                (byte) (ln >> 8 & 255),
                                (byte) (ln >> 16 & 255),
                                (byte) (ln >> 24 & 255),
                                87, 69, 66, 80, 86, 80, 56
                            };

            var ndata = data.Select(x => (byte)((long)x ^ 101)).ToArray();
            var contents = new byte[header.Length + ndata.Length];

            Buffer.BlockCopy(header, 0, contents, 0, header.Length);
            Buffer.BlockCopy(ndata, 0, contents, header.Length, ndata.Length);
            
            #endregion

            return contents;
        }
    }
}
