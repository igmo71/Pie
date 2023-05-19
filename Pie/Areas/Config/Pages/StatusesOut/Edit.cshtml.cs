using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        public StatusOut StatusOut { get; set; } = default!;

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
            StatusOut = status;
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

            _context.Attach(StatusOut).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusOutExists(StatusOut.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StatusOutExists(Guid id)
        {
            return _context.StatusesOut.Any(e => e.Id == id);
        }
    }
}
