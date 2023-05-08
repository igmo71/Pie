using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pie.Data;
using Pie.Data.Models;

namespace Pie.Areas.Config.Pages.ChangeReasonsIn
{
    public class CreateModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public CreateModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ChangeReasonIn ChangeReasonIn { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ChangeReasonsIn.Add(ChangeReasonIn);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
