using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.In;

namespace Pie.Areas.Config.Pages.ChangeReasonsIn
{
    public class DeleteModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DeleteModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ChangeReasonIn ChangeReasonIn { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changeReasonin = await _context.ChangeReasonsIn.FirstOrDefaultAsync(m => m.Id == id);

            if (changeReasonin == null)
            {
                return NotFound();
            }
            else
            {
                ChangeReasonIn = changeReasonin;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changeReason = await _context.ChangeReasonsIn.FirstOrDefaultAsync(e => e.Id == id);
            if (changeReason != null)
            {
                ChangeReasonIn = changeReason;
                _context.ChangeReasonsIn.Remove(ChangeReasonIn);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
