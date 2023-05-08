using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Areas.Config.Pages.ChangeReasonsIn
{
    public class EditModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public EditModel(Pie.Data.ApplicationDbContext context)
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

            var changereasonin = await _context.ChangeReasonsIn.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);
            if (changereasonin == null)
            {
                return NotFound();
            }
            ChangeReasonIn = changereasonin;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ChangeReasonIn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChangeReasonInExists(ChangeReasonIn.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ChangeReasonInExists(Guid id)
        {
            return _context.ChangeReasonsIn.Any(e => e.Id == id);
        }
    }
}
