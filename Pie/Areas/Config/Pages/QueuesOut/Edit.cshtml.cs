using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Areas.Config.Pages.QueuesOut
{
    public class EditModel : PageModel
    {
        private readonly QueueOutService _queueService;

        public EditModel(QueueOutService queueService)
        {
            _queueService = queueService;
        }

        [BindProperty]
        public QueueOut Queue { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            var queue = await _queueService.GetAsync((Guid)id);

            if (queue == null)
                return NotFound();
            else
                Queue = queue;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _queueService.UpdateAsync(Queue);

            return RedirectToPage("./Index");
        }
    }
}
