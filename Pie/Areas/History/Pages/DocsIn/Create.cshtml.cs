using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pie.Data.Models.Identity;
using Pie.Data.Models.In;

namespace Pie.Areas.History.Pages.DocsIn
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
            ViewData["DocId"] = new SelectList(_context.DocsIn, "Id", "Name");
            ViewData["StatusKey"] = new SelectList(_context.StatusesIn, "Key", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", $"{nameof(AppUser.FullName)}");
            return Page();
        }

        [BindProperty]
        public DocInHistory DocHistory { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.DocsInHistory.Add(DocHistory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
