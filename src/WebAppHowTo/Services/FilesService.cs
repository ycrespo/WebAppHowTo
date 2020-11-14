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
        
        public string GetNewFullName(string fullName, string newFilename, string addText = "")
        {
            var name = Path.GetFileNameWithoutExtension(newFilename);
            var ext = Path.GetExtension(fullName);
            var dir = Path.GetDirectoryName(fullName) ?? "./";
            return Path.Combine(dir, $"{name}{addText}{ext}");
        }

        public async Task SaveFileAsync(string fullPath, string file)
        {
            await File.WriteAllTextAsync(fullPath, file, Encoding.UTF8);
        }

        public void DeleteTmpFolder()
        {
            foreach (var file in Directory.GetFiles(TempFolder))
            {
                File.Delete(file);
            }
        }

        public void RenameFile(string newFileName, string fullName)
        {
            if(Path.GetFileName(fullName).Contains(newFileName))
                return;
            
            var newFullName = GetNewFullName(fullName, newFileName);
            File.Move(fullName, newFullName);
        }
    }
}