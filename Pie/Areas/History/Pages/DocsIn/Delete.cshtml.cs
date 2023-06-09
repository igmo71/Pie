using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.In;

namespace Pie.Areas.History.Pages.DocsIn
{
    public class DeleteModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DeleteModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DocInHistory DocHistory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docHistory = await _context.DocsInHistory.FirstOrDefaultAsync(m => m.Id == id);

            if (docHistory == null)
            {
                return NotFound();
            }
            else
            {
                DocHistory = docHistory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docHistory = await _context.DocsInHistory.FindAsync(id);
            if (docHistory != null)
            {
                DocHistory = docHistory;
                _context.DocsInHistory.Remove(DocHistory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
