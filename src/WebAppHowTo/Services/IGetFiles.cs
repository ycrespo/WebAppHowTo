using System.Collections.Generic;
using System.IO;

namespace WebAppHowTo.Services
{
    public interface IGetFiles
    {
        IEnumerable<FileInfo> GetFilesInfo(string path);
        string GetFullPath(string path);
    }
}