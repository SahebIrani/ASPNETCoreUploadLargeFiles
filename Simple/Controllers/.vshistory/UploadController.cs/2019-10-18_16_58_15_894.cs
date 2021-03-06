using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Simple.Controllers
{
    //[DisableRequestSizeLimit]
    //[RequestSizeLimit(300 * 1024)]
    [RequestSizeLimit(500000000)]  //30.000.000=28,6MB
    public class UploadController : Controller
    {
        [HttpPost]
        //To understand the problem, run the application and try to upload a file with larger size, say 100 MB.
        //The[RequestSizeLimit] attribute sets the maximum length of a request in bytes whereas[RequestFormLimits] sets the maximum length for multipart body length.The following code shows the Upload() action decorated with these attributes:
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        public async System.Threading.Tasks.Task<IActionResult> UploadAsync(
            IFormFile file,
            [FromServices] IWebHostEnvironment env)
        {
            string fileName = $"{env.WebRootPath}\\{file.FileName}";

            using (FileStream fs = System.IO.File.Create(fileName))
            {
                await file.CopyToAsync(fs);
                await fs.FlushAsync();
                await fs.DisposeAsync();
            }

            return Ok();
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync(CancellationToken cancellationToken)
        {
            // Microsoft.AspNetCore.Http.Extensions.HttpRequestMultipartExtensions
            //var boundary = Request.GetMultipartBoundary();

            //if (string.IsNullOrWhiteSpace(boundary))
            //    return BadRequest();

            //var reader = new MultipartReader(boundary, Request.Body);
            //var section = await reader.ReadNextSectionAsync();

            //if (section.GetContentDispositionHeader())
            //{
            //    var fileSection = section.AsFileSection();
            //    var fileName = fileSection.FileName;

            //    using (var stream = new FileStream(fileName, FileMode.Append))
            //        await fileSection.FileStream.CopyToAsync(stream);
            //}

            if (!Request.HasFormContentType)
                return BadRequest();

            var form = Request.Form;
            foreach (var formFile in form.Files)
            {
                using (var readStream = formFile.OpenReadStream())
                {
                    // Do something with the uploaded file
                }
            }


            return Ok();
        }
    }
}
