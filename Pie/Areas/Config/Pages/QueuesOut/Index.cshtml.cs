using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;

namespace Pie.Areas.Config.Pages.QueuesOut
{
    public class IndexModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public IndexModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<QueueOut> QueueOut { get; set; } = default!;

        public async Task OnGetAsync()
        {
            QueueOut = await _context.QueuesOut.ToListAsync();
        }
    }
}
