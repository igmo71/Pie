using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;

namespace Pie.Areas.Config.Pages.StatusesOut
{
    public class DetailsModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DetailsModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
