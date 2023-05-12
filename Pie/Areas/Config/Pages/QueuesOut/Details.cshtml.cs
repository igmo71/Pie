using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data;
using Pie.Data.Models.Out;

namespace Pie.Areas.Config.Pages.QueuesOut
{
    public class DetailsModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DetailsModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
