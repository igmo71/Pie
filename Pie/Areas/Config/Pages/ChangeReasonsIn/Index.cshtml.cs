using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.In;

namespace Pie.Areas.Config.Pages.ChangeReasonsIn
{
    public class IndexModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public IndexModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ChangeReasonIn> ChangeReasonIn { get; set; } = default!;

        public async Task OnGetAsync()
        {
            ChangeReasonIn = await _context.ChangeReasonsIn.IgnoreQueryFilters().ToListAsync();
        }
    }
}
