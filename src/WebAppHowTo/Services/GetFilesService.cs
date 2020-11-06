using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace WebAppHowTo.Services
{
    public class GetFilesService : IGetFiles
    {
        private readonly IWebHostEnvironment _environment;
        
        public GetFilesService(IWebHostEnvironment environment)
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
    }
}