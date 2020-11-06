using System.Collections.Generic;
using System.IO;

namespace WebAppHowTo.Services
{
    public interface IGetFiles
    {
        string TempFolder { get; }
        IEnumerable<FileInfo> GetFilesInfo(string path);

        FileInfo GetFileInfo(string path);
        string GetFullPath(string path);
    }
}