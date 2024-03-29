﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;

namespace Pie.Areas.History.Pages.DocsOut
{
    public class DeleteModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DeleteModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DocOutHistory DocHistory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docHistory = await _context.DocsOutHistory.FirstOrDefaultAsync(m => m.Id == id);

            if (docHistory == null)
            {
                return NotFound();
            }
            else
            {
                DocHistory = docHistory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docHistory = await _context.DocsOutHistory.FindAsync(id);
            if (docHistory != null)
            {
                DocHistory = docHistory;
                _context.DocsOutHistory.Remove(DocHistory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
