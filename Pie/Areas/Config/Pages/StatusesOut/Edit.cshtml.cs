using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Areas.Config.Pages.StatusesOut
{
    public class EditModel : PageModel
    {
        private readonly StatusOutService _statusOutService;

        public EditModel(StatusOutService statusOutService)
        {
            _statusOutService = statusOutService;
        }

        [BindProperty]
        public StatusOut Status { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _statusOutService.GetAsync((Guid)id);
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

            await _statusOutService.UpdateAsync(Status);

            return RedirectToPage("./Index");
        }
    }
}
