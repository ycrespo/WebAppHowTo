using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BlazorInputFile;

namespace WebAppHowTo.Services
{
    public interface IFilesService
    {
        string TempFolder { get; }
        IEnumerable<FileInfo> GetFilesInfo(string path);
        FileInfo GetFileInfo(string path);
        string GetFullPath(string path);
        string GetWebRootPath(string path);
        Task UploadAsync(IFileListEntry file); 
        string GetNewFullName(string fullName, string newFilename, string addText = "");
        void DeleteTmpFolder();
        void RenameFile(string newFileName, string fullName);

        Task SaveFileAsync(string fullPath, string file);
    }
}