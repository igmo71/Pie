using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.In;

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
