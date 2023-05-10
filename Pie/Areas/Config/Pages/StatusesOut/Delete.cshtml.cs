using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;

namespace Pie.Areas.Config.Pages.StatusesOut
{
    public class DeleteModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DeleteModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StatusOut StatusOut { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusout = await _context.StatusesOut.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);

            if (statusout == null)
            {
                return NotFound();
            }
            else
            {
                StatusOut = statusout;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusout = await _context.StatusesOut.FindAsync(id);
            if (statusout != null)
            {
                StatusOut = statusout;
                _context.StatusesOut.Remove(StatusOut);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
