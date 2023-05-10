using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Out;
using Pie.Data.Services;

namespace Pie.Areas.Config.Pages.ChangeReasonsOut
{
    public class EditModel : PageModel
    {
        private readonly ChangeReasonOutService _changeReasonService;

        public EditModel(ChangeReasonOutService changeReasonService)
        {
            _changeReasonService = changeReasonService;
        }

        [BindProperty]
        public ChangeReasonOut ChangeReasonOut { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changeReason = await _changeReasonService.GetAsync(id);

            if (changeReason == null)
                return NotFound();

            ChangeReasonOut = changeReason;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _changeReasonService.UpdateAsync(ChangeReasonOut);

            return RedirectToPage("./Index");
        }
    }
}
