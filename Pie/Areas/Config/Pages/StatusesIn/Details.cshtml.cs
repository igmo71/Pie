using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Areas.Config.Pages.StatusesIn
{
    public class DetailsModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DetailsModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public StatusIn StatusIn { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusin = await _context.StatusesIn.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);
            if (statusin == null)
            {
                return NotFound();
            }
            else
            {
                StatusIn = statusin;
            }
            return Page();
        }
    }
}
