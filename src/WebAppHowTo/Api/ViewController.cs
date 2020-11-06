using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeTypes;

namespace WebAppHowTo.Api
{
    [Route("api/ViewController")]
    public class ViewController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public ViewController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        //GET api/ViewController/12345abc
        [HttpGet("{folder}/{filename}")]
        public async Task<IActionResult> Download(string folder, string filename)
        {
            var path = Path.Combine(_environment.ContentRootPath, folder, filename);
            var stream = System.IO.File.OpenRead(path);
            return  new FileStreamResult(stream, MimeTypeMap.GetMimeType(Path.GetExtension(path))); 
        }  
    }
}