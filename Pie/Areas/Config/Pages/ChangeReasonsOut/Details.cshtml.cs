using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Areas.Config.Pages.ChangeReasonsOut
{
    public class DetailsModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DetailsModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ChangeReasonOut ChangeReasonOut { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changereasonout = await _context.ChangeReasonsOut.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);
            if (changereasonout == null)
            {
                return NotFound();
            }
            else
            {
                ChangeReasonOut = changereasonout;
            }
            return Page();
        }
    }
}
