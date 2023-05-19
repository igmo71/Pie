using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.In;

namespace Pie.Areas.Config.Pages.StatusesIn
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
        public StatusIn StatusIn { get; set; } = default!;

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.StatusesIn.Add(StatusIn);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
