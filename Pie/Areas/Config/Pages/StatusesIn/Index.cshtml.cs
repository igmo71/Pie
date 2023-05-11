using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.In;

namespace Pie.Areas.Config.Pages.StatusesIn
{
    public class IndexModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public IndexModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<StatusIn> StatusIn { get; set; } = default!;

        public async Task OnGetAsync()
        {
            StatusIn = await _context.StatusesIn.ToListAsync();
        }
    }
}
