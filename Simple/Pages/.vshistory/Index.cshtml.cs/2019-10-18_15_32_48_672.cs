using System.IO;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Simple.Pages.vshistory.Index.cshtml.cs
{
    public class IndexModel : PageModel
    {

        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            Environment = environment;
        }

        private readonly ILogger<IndexModel> _logger;
        public IWebHostEnvironment Environment { get; }

        public void OnGet()
        {

        }

        public IActionResult OnPost(IFormFile file)
        {
            string fileName = $"{Environment.WebRootPath}\\{file.FileName}";

            using (FileStream fs = System.IO.File.Create(fileName))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            return Page();
        }
    }
}
