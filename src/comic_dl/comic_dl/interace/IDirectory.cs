using System;
using System.Collections.Generic;
using System.Text;

namespace comic_dl.interace
{
    public interface IDirectory
    {
        string GetBasePath();
        string CreateDirectory(string directoryName);
        bool CheckExisingFile(string filePath);
        bool CreateFile(string directoryName, byte[] contentToWrite);
    }
}
