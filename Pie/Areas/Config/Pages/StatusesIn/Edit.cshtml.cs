using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.In;
using Pie.Data.Services.In;

namespace Pie.Areas.Config.Pages.StatusesIn
{
    public class EditModel : PageModel
    {
        private readonly StatusInService _statusService;

        public EditModel(StatusInService statusService)
        {
            _statusService = statusService;
        }

        [BindProperty]
        public StatusIn Status { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _statusService.GetAsync((Guid)id);
            if (status == null)
            {
                return NotFound();
            }
            Status = status;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _statusService.UpdateAsync(Status);

            return RedirectToPage("./Index");
        }
    }
}
