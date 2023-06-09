using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;

namespace Pie.Areas.History.Pages.DocsOut
{
    public class EditModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public EditModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DocOutHistory DocHistory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docHistory = await _context.DocsOutHistory.FirstOrDefaultAsync(m => m.Id == id);
            if (docHistory == null)
            {
                return NotFound();
            }
            DocHistory = docHistory;
            ViewData["DocId"] = new SelectList(_context.DocsOut, "Id", "Name");
            ViewData["StatusKey"] = new SelectList(_context.StatusesOut, "Key", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
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

            _context.Attach(DocHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocOutHistoryExists(DocHistory.Id))
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

        private bool DocOutHistoryExists(Guid id)
        {
            return _context.DocsOutHistory.Any(e => e.Id == id);
        }
    }
}
