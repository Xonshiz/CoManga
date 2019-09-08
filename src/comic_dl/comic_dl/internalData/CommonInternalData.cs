using System.Collections.Generic;

namespace comic_dl.internalData
{
    class CommonInternalData
    {
        public static bool isMangaSelected = true;
        public static bool isComicSelected = false;
        public static bool isSettingSelected = false;   
        public static List<string> failedDownloadLinks = new List<string>() { };
        public enum DownloadType { MangaRock, RCO }
        public enum RefreshType { HotLatestItems, LatestItems}

        /*
         * HomeResultPreference = Which page does the user want to watch when they open the app, i.e., Manga or Comic.
         */
        public enum UserSettingkey { HomeResultPreference }

        public string googleMangaAdsId = "";
    }
}
