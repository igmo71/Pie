using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;

namespace Pie.Areas.History.Pages.DocsOut.Products
{
    public class IndexModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public IndexModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<DocOutProductHistory> DocOutProductHistory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            DocOutProductHistory = await _context.DocOutProductsHistory
                .Include(d => d.ChangeReason)
                .Include(d => d.Doc)
                .Include(d => d.Product)
                .Include(d => d.User)
                .OrderBy(d => d.Doc.Name).ThenBy(d => d.LineNumber).ThenBy(d => d.DateTime)
                .Take(50)
                .ToListAsync();    
        }
    }
}
