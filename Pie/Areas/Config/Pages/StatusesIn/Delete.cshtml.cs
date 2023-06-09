using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.In;
using Pie.Data.Services.In;

namespace Pie.Areas.Config.Pages.StatusesIn
{
    public class DeleteModel : PageModel
    {
        private readonly StatusInService _statusService;

        public DeleteModel(StatusInService statusService)
        {
            _statusService = statusService;
        }

        [BindProperty]
        public StatusIn Status { get; set; } = default!;

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
