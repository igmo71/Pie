using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Areas.Config.Pages.ChangeReasonsOut
{
    public class IndexModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public IndexModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ChangeReasonOut> ChangeReasonOut { get; set; } = default!;

        public async Task OnGetAsync()
        {
            ChangeReasonOut = await _context.ChangeReasonsOut.IgnoreQueryFilters().ToListAsync();
        }
    }
}
