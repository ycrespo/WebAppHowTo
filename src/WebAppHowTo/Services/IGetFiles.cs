using System.Collections.Generic;
using System.IO;

namespace WebAppHowTo.Services
{
    public interface IGetFiles
    {
        public IEnumerable<FileInfo> GetFilesInfo(string path);
    }
}