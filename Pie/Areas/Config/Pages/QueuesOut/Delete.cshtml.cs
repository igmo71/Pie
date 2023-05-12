using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Areas.Config.Pages.QueuesOut
{
    public class DeleteModel : PageModel
    {
        private readonly QueueOutService _queueService;

        public DeleteModel(QueueOutService queueService)
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            if (!_queueService.Exists((Guid)id))
                return NotFound();
            else
                await _queueService.DeleteAsync((Guid)id);

            return RedirectToPage("./Index");
        }
    }
}
