using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.In;

namespace Pie.Areas.Config.Pages.ChangeReasonsIn
{
    public class DetailsModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DetailsModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ChangeReasonIn ChangeReasonIn { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changeReason = await _context.ChangeReasonsIn.FirstOrDefaultAsync(m => m.Id == id);
            if (changeReason == null)
            {
                return NotFound();
            }
            else
            {
                ChangeReasonIn = changeReason;
            }
            return Page();
        }
    }
}
