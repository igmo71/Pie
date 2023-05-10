using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;

namespace Pie.Areas.Config.Pages.QueuesOut
{
    public class DeleteModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DeleteModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public QueueOut QueueOut { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queueout = await _context.QueuesOut.FirstOrDefaultAsync(m => m.Id == id);

            if (queueout == null)
            {
                return NotFound();
            }
            else
            {
                QueueOut = queueout;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queueout = await _context.QueuesOut.FindAsync(id);
            if (queueout != null)
            {
                QueueOut = queueout;
                _context.QueuesOut.Remove(QueueOut);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
