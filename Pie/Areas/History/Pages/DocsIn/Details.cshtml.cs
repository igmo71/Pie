using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.In;

namespace Pie.Areas.History.Pages.DocsIn
{
    public class DetailsModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DetailsModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public DocInHistory DocHistory { get; set; } = default!;
        public Guid? DocId { get; set; }
        public Guid? Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? docId, Guid? id )
        {
            if (docId == null || id == null)
            {
                return NotFound();
            }

            DocId = docId;
            Id = id;

            var docHistory = await _context.DocsInHistory.AsNoTracking()
                .Include(d => d.Status)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (docHistory == null)
            {
                return NotFound();
            }
            else
            {
                DocHistory = docHistory;
            }
            return Page();
        }
    }
}
