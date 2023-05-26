using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.In;
using Pie.Data.Services.In;

namespace Pie.Areas.Config.Pages.QueuesIn
{
    public class DeleteModel : PageModel
    {
        private readonly QueueInService _queueService;

        public DeleteModel(QueueInService queueService)
        {
            _queueService = queueService;
        }

        [BindProperty]
        public QueueIn Queue { get; set; } = default!;

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
