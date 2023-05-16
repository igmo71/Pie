using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data;
using Pie.Data.Models.Out;

namespace Pie.Areas.History.Pages.DocsOut.Products
{
    public class DeleteModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public DeleteModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DocOutProductHistory DocOutProductHistory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docoutproducthistory = await _context.DocOutProductsHistory.FirstOrDefaultAsync(m => m.Id == id);

            if (docoutproducthistory == null)
            {
                return NotFound();
            }
            else
            {
                DocOutProductHistory = docoutproducthistory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docoutproducthistory = await _context.DocOutProductsHistory.FindAsync(id);
            if (docoutproducthistory != null)
            {
                DocOutProductHistory = docoutproducthistory;
                _context.DocOutProductsHistory.Remove(DocOutProductHistory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
