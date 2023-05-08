using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data;
using Pie.Data.Models;

namespace Pie.Areas.Config.Pages.ChangeReasonsOut
{
    public class DeleteModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DeleteModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ChangeReasonOut ChangeReasonOut { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changereasonout = await _context.ChangeReasonsOut.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);

            if (changereasonout == null)
            {
                return NotFound();
            }
            else
            {
                ChangeReasonOut = changereasonout;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changereasonout = await _context.ChangeReasonsOut.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);
            if (changereasonout != null)
            {
                ChangeReasonOut = changereasonout;
                _context.ChangeReasonsOut.Remove(ChangeReasonOut);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
