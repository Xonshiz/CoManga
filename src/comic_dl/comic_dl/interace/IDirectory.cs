using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace comic_dl.interace
{
    public interface IDirectory
    {
        string GetBasePath();
        string CreateDirectory(string directoryName);
        bool CheckExisingFile(string filePath);
        Task<bool> CreateFile(string directoryName, byte[] contentToWrite);
    }
}
