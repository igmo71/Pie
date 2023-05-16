using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;

namespace Pie.Areas.History.Pages.DocsOut
{
    public class DetailsModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DetailsModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public DocOutHistory DocOutHistory { get; set; } = default!;
        public Guid? DocId { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? docId)
        {
            if (docId == null)
            {
                return NotFound();
            }

            DocId = docId;

            var docHistory = await _context.DocsOutHistory.AsNoTracking()
                .Include(d => d.Doc)
                .Include(d => d.Status)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.DocId == docId);

            if (docHistory == null)
            {
                return NotFound();
            }
            else
            {
                DocOutHistory = docHistory;
            }
            return Page();
        }
    }
}
