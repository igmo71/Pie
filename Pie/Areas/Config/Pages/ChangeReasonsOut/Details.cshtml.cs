    using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Areas.Config.Pages.ChangeReasonsOut
{
    public class DetailsModel : PageModel
    {
        private readonly ChangeReasonOutService _changeReasonService;

        public DetailsModel(ChangeReasonOutService changeReasonService)
        {
            _changeReasonService = changeReasonService;
        }

        public ChangeReasonOut ChangeReason { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            var changeReason = await _changeReasonService.GetAsync(id);

            if (changeReason == null)
                return NotFound();
            else
                ChangeReason = changeReason;

            return Page();
        }
    }
}
