using comic_dl.commonFunctionalities;
using comic_dl.interace;
using comic_dl.internalData;
using comic_dl.internalData.comic;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace comic_dl._comic.downloader
{
    class ComicDownloader
    {
        private HttpClient _httpClient = new HttpClient();

        private string DownloadPercentage(double currentFileCount, double totalFiles)
        {
            return Convert.ToString(Convert.ToInt16((Convert.ToDouble(currentFileCount / totalFiles)) * 100));
        }
        private Task<string> GetDirectory(string comicName, string chapterNumber)
        {
            string directoryPath = "/Comics/" + comicName + "/" + chapterNumber;
            string baseDirectory = DependencyService.Get<IDirectory>().GetBasePath();
            return Task.FromResult(DependencyService.Get<IDirectory>().CreateDirectory(directoryPath));

        }

        public async Task<bool> FilesDownloader(List<string> listOfUrl, string comicName, string chapterNumber, CommonInternalData.DownloadType downloadType, Xamarin.Forms.Button button)
        {
            int fileCount = 0;
            //CommonFunctions commonFunctions = new CommonFunctions();
            string baseDirectoryAddress = await GetDirectory(CommonFunctions.FileNameCleaner(comicName), chapterNumber);

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
                        if (downloadType == CommonInternalData.DownloadType.RCO)
                            contents = await ImageDownloader(baseDirectoryAddress, fileUrl, Convert.ToString(fileCount));

                        if (contents == null) // If there's nothing, just move onto the next loop.
                            continue;

                        bool isFileDownloaded = DependencyService.Get<IDirectory>().CreateFile(filePath, contents);


                        if (!isFileDownloaded)
                        {
                            CommonInternalData.failedDownloadLinks.Add(fileUrl); // If failed, add to failed download list.
                            CurrentData.comic_response = "Download Failed";
                        }
                        else
                        {
                            CurrentData.comic_response = "Download Completed.";
                            CurrentData.comic_downloadPath = baseDirectoryAddress; // For opening the Explorer.
                        }
                    }
                    else
                    {
                        fileCount++;
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
        public async Task<byte[]> ImageDownloader(string baseDirectoryAddress, string fileUrl, string fileCount)
        {
            /*
             * Thanks to @Raxdiam : https://www.reddit.com/r/csharp/comments/9fs2t0/trying_to_convert_mri_files_to_a_valid_image/
             */
            Uri uri = new Uri(fileUrl);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.109 Safari/537.36");
            _httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            try
            {
                using (var httpResponse = await _httpClient.GetAsync(fileUrl))
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        return await httpResponse.Content.ReadAsByteArrayAsync();
                    }
                    else
                    {
                        //Url is Invalid
                        //return null;
                        try
                        {
                            return await ImageDownloaderCFByPass(baseDirectoryAddress, fileUrl, fileCount);
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<byte[]> ImageDownloaderCFByPass(string baseDirectoryAddress, string fileUrl, string fileCount)
        {
            /*
             * Thanks to @Raxdiam : https://www.reddit.com/r/csharp/comments/9fs2t0/trying_to_convert_mri_files_to_a_valid_image/
             */
            Uri uri = new Uri(fileUrl);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.109 Safari/537.36");
            _httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            _httpClient.DefaultRequestHeaders.Add("referer", fileUrl);
            try
            {
                using (var httpResponse = await _httpClient.GetAsync(fileUrl))
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        return await httpResponse.Content.ReadAsByteArrayAsync();
                    }
                    else
                    {
                        //Url is Invalid
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
