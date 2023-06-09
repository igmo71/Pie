using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.In;
using Pie.Data.Services.In;

namespace Pie.Areas.Config.Pages.StatusesIn
{
    public class CreateModel : PageModel
    {
        private readonly StatusInService _statusService;

        public CreateModel(StatusInService statusService)
        {
            _statusService = statusService;
        }

        [BindProperty]
        public StatusIn Status { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _statusService.CreateAsync(Status);

            return RedirectToPage("./Index");
        }
    }
}
