using comic_dl.internalData.comic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace comic_dl.commonFunctionalities
{
    class UWPFunc
    {
        public static void OpenExplorer(string pathToOpen)
        {
            ProcessStartInfo StartInformation = new ProcessStartInfo();

            StartInformation.FileName = pathToOpen;

            Process process = Process.Start(StartInformation);

            process.EnableRaisingEvents = true;
        }
    }
}
