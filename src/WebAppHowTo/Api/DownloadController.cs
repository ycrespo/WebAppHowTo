using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

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

        //GET api/download/12345abc
        [HttpGet("{filename}")]
        public async Task<IActionResult> Download(string filename)
        {
            var path = Path.Combine(_environment.ContentRootPath, "Uploads", filename);
            var stream = System.IO.File.OpenRead(path);
            return  new FileStreamResult(stream, "application/octet-stream"); // returns a FileStreamResult
        }    
    }
}