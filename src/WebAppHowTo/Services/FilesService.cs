using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorInputFile;
using Microsoft.AspNetCore.Hosting;

namespace WebAppHowTo.Services
{
    public class FilesService : IFilesService
    {
        private readonly IWebHostEnvironment _environment;
        
        public FilesService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public string TempFolder => Path.Combine(_environment.ContentRootPath, "Temp");

        public IEnumerable<FileInfo> GetFilesInfo(string path)
        {
            var files = Directory.GetFiles(Path.Combine(_environment.ContentRootPath, path));
            return files.Select(f => new FileInfo(f));
        }
        
        public FileInfo GetFileInfo(string path) => new FileInfo(Path.Combine(_environment.ContentRootPath, path));
          
        public string GetFullPath(string path)
        {
            return Path.Combine(_environment.ContentRootPath, path);
        }
        
        public string GetWebRootPath(string path)
        {
            return Path.Combine(_environment.WebRootPath, path);
        }
        
        public async Task UploadAsync(IFileListEntry file)
        {
            var path = Path.Combine(_environment.ContentRootPath, "Uploads", file.Name);
            await using var stream = new FileStream(path, FileMode.Create, FileAccess.Write);
            await file.Data.CopyToAsync(stream);
        }
        
        public async Task<string> GetFullPathAsync(FileSystemInfo fileInfo, string newFile)
        {
            var name = Path.GetFileNameWithoutExtension(fileInfo.FullName);
            var ext = Path.GetExtension(fileInfo.FullName);
            var filename = $"{name}_Edited{ext}";
            var fullPath = Path.Combine(Path.GetDirectoryName(fileInfo.FullName), filename);
            await File.WriteAllTextAsync(fullPath, newFile, Encoding.UTF8);
            return fullPath;
        }

        public void DeleteTmpFolder()
        {
            foreach (var file in Directory.GetFiles(TempFolder))
            {
                File.Delete(file);
            }
        }
    }
}