using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data;
using Pie.Data.Models;

namespace Pie.Areas.Config.Pages.StatusesIn
{
    public class DeleteModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DeleteModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StatusIn StatusIn { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusin = await _context.StatusesIn.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);

            if (statusin == null)
            {
                return NotFound();
            }
            else
            {
                StatusIn = statusin;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusin = await _context.StatusesIn.FindAsync(id);
            if (statusin != null)
            {
                StatusIn = statusin;
                _context.StatusesIn.Remove(StatusIn);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
