using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pie.Data;
using Pie.Data.Models;

namespace Pie.Areas.Config.Pages.StatusesOut
{
    public class EditModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public EditModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StatusOut StatusOut { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusout =  await _context.StatusesOut.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);
            if (statusout == null)
            {
                return NotFound();
            }
            StatusOut = statusout;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(StatusOut).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusOutExists(StatusOut.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StatusOutExists(Guid id)
        {
            return _context.StatusesOut.Any(e => e.Id == id);
        }
    }
}
