using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.In;

namespace Pie.Areas.Config.Pages.QueuesIn
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
        public QueueIn QueueIn { get; set; } = default!;

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.QueuesIn.Add(QueueIn);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
