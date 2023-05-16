using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data;
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
        public DocOutHistory DocOutHistory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docouthistory = await _context.DocsOutHistory.FirstOrDefaultAsync(m => m.Id == id);

            if (docouthistory == null)
            {
                return NotFound();
            }
            else
            {
                DocOutHistory = docouthistory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docouthistory = await _context.DocsOutHistory.FindAsync(id);
            if (docouthistory != null)
            {
                DocOutHistory = docouthistory;
                _context.DocsOutHistory.Remove(DocOutHistory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
