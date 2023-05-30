using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Areas.Config.Pages.StatusesOut
{
    public class CreateModel : PageModel
    {
        private readonly StatusOutService _statusService;

        public CreateModel(StatusOutService statusService)
        {
            _statusService = statusService;
        }

        [BindProperty]
        public StatusOut Status { get; set; } = default!;

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
