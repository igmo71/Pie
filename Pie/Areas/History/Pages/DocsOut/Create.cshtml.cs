using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pie.Data.Models.Application;
using Pie.Data.Models.Out;

namespace Pie.Areas.History.Pages.DocsOut
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
            ViewData["DocId"] = new SelectList(_context.DocsOut, "Id", "Name");
            ViewData["StatusKey"] = new SelectList(_context.StatusesOut, "Key", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", $"{nameof(ApplicationUser.FullName)}");
            return Page();
        }

        [BindProperty]
        public DocOutHistory DocOutHistory { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.DocsOutHistory.Add(DocOutHistory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
