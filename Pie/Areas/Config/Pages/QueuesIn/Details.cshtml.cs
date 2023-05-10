using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.In;

namespace Pie.Areas.Config.Pages.QueuesIn
{
    public class DetailsModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DetailsModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public QueueIn QueueIn { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queuein = await _context.QueuesIn.FirstOrDefaultAsync(m => m.Id == id);
            if (queuein == null)
            {
                return NotFound();
            }
            else
            {
                QueueIn = queuein;
            }
            return Page();
        }
    }
}
