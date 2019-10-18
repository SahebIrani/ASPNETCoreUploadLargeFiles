using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Simple.Controllers
{
    public class UploadController : Controller
    {
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        public IActionResult Upload(IFormFile file[FromServices] IWebHostEnvironment env)
        {
        }
    }
}
