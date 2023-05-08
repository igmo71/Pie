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

namespace Pie.Areas.Config.Pages.ChangeReasonsOut
{
    public class EditModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public EditModel(Pie.Data.ApplicationDbContext context)
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

            var changereasonout =  await _context.ChangeReasonsOut.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);
            if (changereasonout == null)
            {
                return NotFound();
            }
            ChangeReasonOut = changereasonout;
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

            _context.Attach(ChangeReasonOut).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChangeReasonOutExists(ChangeReasonOut.Id))
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

        private bool ChangeReasonOutExists(Guid id)
        {
            return _context.ChangeReasonsOut.Any(e => e.Id == id);
        }
    }
}
