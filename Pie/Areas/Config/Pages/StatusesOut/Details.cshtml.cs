using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Areas.Config.Pages.StatusesOut
{
    public class DetailsModel : PageModel
    {
        private readonly StatusOutService _statusService;

        public DetailsModel(StatusOutService statusService)
        {
            _statusService = statusService;
        }

        public StatusOut Status { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            var status = await _statusService.GetAsync((Guid)id);

            if (status == null)
                return NotFound();
            else
                Status = status;

            return Page();
        }
    }
}
