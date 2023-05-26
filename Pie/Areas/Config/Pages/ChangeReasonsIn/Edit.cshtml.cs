using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.In;
using Pie.Data.Services.In;

namespace Pie.Areas.Config.Pages.ChangeReasonsIn
{
    public class EditModel : PageModel
    {
        private readonly ChangeReasonInService _changeReasonService;

        public EditModel(ChangeReasonInService changeReasonService)
        {
            _changeReasonService = changeReasonService;
        }

        [BindProperty]
        public ChangeReasonIn ChangeReason { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            var changeReason = await _changeReasonService.GetAsync(id);

            if (changeReason == null)
                return NotFound();

            ChangeReason = changeReason;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _changeReasonService.UpdateAsync(ChangeReason);

            return RedirectToPage("./Index");
        }
    }
}
