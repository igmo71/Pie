using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.In;
using Pie.Data.Services.In;

namespace Pie.Areas.Config.Pages.ChangeReasonsIn
{
    public class CreateModel : PageModel
    {
        private readonly ChangeReasonInService _changeReasonService;

        public CreateModel(ChangeReasonInService changeReasonService)
        {
            _changeReasonService = changeReasonService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ChangeReasonIn ChangeReason { get; set; } = default!;

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _changeReasonService.CreateAsync(ChangeReason);

            return RedirectToPage("./Index");
        }
    }
}
