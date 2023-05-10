using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.In;

namespace Pie.Areas.Config.Pages.QueuesIn
{
    public class IndexModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public IndexModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<QueueIn> QueueIn { get; set; } = default!;

        public async Task OnGetAsync()
        {
            QueueIn = await _context.QueuesIn.ToListAsync();
        }
    }
}
