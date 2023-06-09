using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;

namespace Pie.Areas.History.Pages.DocsOut.Products
{
    public class DetailsModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DetailsModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public DocOutProductHistory DocOutProductHistory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docProductHistory = await _context.DocOutProductsHistory.AsNoTracking()
                .Include(e => e.Product)
                .Include(e => e.ChangeReason)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (docProductHistory == null)
            {
                return NotFound();
            }
            else
            {
                DocOutProductHistory = docProductHistory;
            }
            return Page();
        }
    }
}
