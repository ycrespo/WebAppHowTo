using System.IO;
using System.Threading.Tasks;
using BlazorInputFile;
using Microsoft.AspNetCore.Hosting;

namespace WebAppHowTo.Services
{
    public class FileUploadService : IFileUpload
    {
        private readonly IWebHostEnvironment _environment;

        public FileUploadService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task UploadAsync(IFileListEntry file)
        {
            var path = Path.Combine(_environment.ContentRootPath, "Uploads", file.Name);
            await using var stream = new FileStream(path, FileMode.Create, FileAccess.Write);
            await file.Data.CopyToAsync(stream);
        }
    }
}