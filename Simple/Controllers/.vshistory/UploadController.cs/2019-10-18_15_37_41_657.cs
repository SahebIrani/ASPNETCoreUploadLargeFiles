using System.IO;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Simple.Controllers
{
    public class UploadController : Controller
    {
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        public async System.Threading.Tasks.Task<IActionResult> UploadAsync(IFormFile file[FromServices] IWebHostEnvironment env)
        {
            string fileName = $"{env.WebRootPath}\\{file.FileName}";

            //To understand the problem, run the application and try to upload a file with larger size, say 100 MB.
            using (FileStream fs = System.IO.File.Create(fileName))
            {
                await file.CopyToAsync(fs);
                await fs.FlushAsync();
                await fs.DisposeAsync();
            }

            return Ok();
        }
    }
}
