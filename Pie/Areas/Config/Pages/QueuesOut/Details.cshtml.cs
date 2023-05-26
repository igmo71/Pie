using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Areas.Config.Pages.QueuesOut
{
    public class DetailsModel : PageModel
    {
        private readonly QueueOutService _queueService;

        public DetailsModel(QueueOutService queueOutService)
        {
            _queueService = queueOutService;
        }

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
    }
}
