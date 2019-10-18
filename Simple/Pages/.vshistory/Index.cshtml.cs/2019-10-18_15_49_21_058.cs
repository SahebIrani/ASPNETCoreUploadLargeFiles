using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        //A single IFormFile.
        //Any of the following collections that represent several files:
        //IFormFileCollection
        //IEnumerable<IFormFile>
        //List<IFormFile>
        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            string fileName = $"{Environment.WebRootPath}\\{file.FileName}";

            //To understand the problem, run the application and try to upload a file with larger size, say 100 MB.
            using (FileStream fs = System.IO.File.Create(fileName))
            {
                await file.CopyToAsync(fs);
                await fs.FlushAsync();
                await fs.DisposeAsync();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    //var filePath = Path.Combine(_config["StoredFilesPath"],Path.GetRandomFileName());

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return new JsonResult(new { count = files.Count, size, filePath });
        }
    }
}
