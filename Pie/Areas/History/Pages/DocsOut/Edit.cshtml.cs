using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pie.Data;
using Pie.Data.Models.Out;

namespace Pie.Areas.History.Pages.DocsOut
{
    public class EditModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public EditModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DocOutHistory DocOutHistory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docouthistory =  await _context.DocsOutHistory.FirstOrDefaultAsync(m => m.Id == id);
            if (docouthistory == null)
            {
                return NotFound();
            }
            DocOutHistory = docouthistory;
           ViewData["DocId"] = new SelectList(_context.DocsOut, "Id", "Name");
           ViewData["StatusKey"] = new SelectList(_context.StatusesOut, "Key", "Name");
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
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

            _context.Attach(DocOutHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocOutHistoryExists(DocOutHistory.Id))
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

        private bool DocOutHistoryExists(Guid id)
        {
            return _context.DocsOutHistory.Any(e => e.Id == id);
        }
    }
}
