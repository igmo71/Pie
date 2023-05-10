using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Areas.Config.Pages.ChangeReasonsOut
{
    public class CreateModel : PageModel
    {
        private readonly ChangeReasonOutService _changeReasonService;

        public CreateModel(ChangeReasonOutService changeReasonService)
        {
            _changeReasonService = changeReasonService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ChangeReasonOut ChangeReasonOut { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _changeReasonService.CreateAsync(ChangeReasonOut);

            return RedirectToPage("./Index");
        }
    }
}
