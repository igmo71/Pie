using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;

namespace Pie.Areas.History.Pages.DocsOut.Products
{
    public class EditModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public EditModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DocOutProductHistory DocOutProductHistory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docoutproducthistory =  await _context.DocOutProductsHistory.FirstOrDefaultAsync(m => m.Id == id);
            if (docoutproducthistory == null)
            {
                return NotFound();
            }
            DocOutProductHistory = docoutproducthistory;
           ViewData["ChangeReasonId"] = new SelectList(_context.ChangeReasonsOut, "Id", "Name");
           ViewData["DocId"] = new SelectList(_context.DocsOut, "Id", "Name");
           ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName");
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

            _context.Attach(DocOutProductHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocOutProductHistoryExists(DocOutProductHistory.Id))
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

        private bool DocOutProductHistoryExists(Guid id)
        {
            return _context.DocOutProductsHistory.Any(e => e.Id == id);
        }
    }
}
