using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using WebAppHowTo.Data.Gateways;

namespace WebAppHowTo.Api
{
    [Route("api/ExcelExportController")]
    public class ExcelExportController : Controller
    {
        private readonly IContextGateway _context;

        public ExcelExportController(IContextGateway context)
        {
            _context = context;
        }

        [HttpGet("{excelName}")]
        public async Task<IActionResult> ExcelExport(string excelName)
        {
            var costumers = await _context.GetCostumers();

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(costumers, PrintHeaders: true);
                package.Save();
            }

            stream.Position = 0;

            // above I define the name of the file using the current datetime.
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64", excelName); // this will be the actual export.
        }
    }
}