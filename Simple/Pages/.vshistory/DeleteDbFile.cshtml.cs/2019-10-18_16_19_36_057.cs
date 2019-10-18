using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Simple.Data;

namespace Simple.Pages
{
    public class DeleteDbFileModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteDbFileModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AppFile RemoveFile { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Index");
            }

            RemoveFile = await _context.File.SingleOrDefaultAsync(m => m.Id == id);

            if (RemoveFile == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Index");
            }

            RemoveFile = await _context.File.FindAsync(id);

            if (RemoveFile != null)
            {
                _context.File.Remove(RemoveFile);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }
    }
}
