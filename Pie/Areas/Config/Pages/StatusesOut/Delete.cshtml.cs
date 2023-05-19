using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Areas.Config.Pages.StatusesOut
{
    public class DeleteModel : PageModel
    {
        private readonly StatusOutService _statusService;

        public DeleteModel(StatusOutService statusService)
        {
            _statusService = statusService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            if (!_statusService.Exists((Guid)id))
                return NotFound();
            else
                await _statusService.DeleteAsync((Guid)id);

            return RedirectToPage("./Index");
        }
    }
}
