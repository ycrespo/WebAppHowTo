using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeTypes;

namespace WebAppHowTo.Api
{
    [Route("api/DownloadController")]
    public class DownloadController : Controller 
    {
        private readonly IWebHostEnvironment _environment;

        public DownloadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet("{folder}/{filename}")]
        public async Task<IActionResult> Download(string folder, string filename)
        {
            var path = Path.Combine(_environment.ContentRootPath, folder, filename);
            var stream = System.IO.File.OpenRead(path);
            return  new FileStreamResult(stream, MimeTypeMap.GetMimeType(Path.GetExtension(path))); 
        }  
    }
}