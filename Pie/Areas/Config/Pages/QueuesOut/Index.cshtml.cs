using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Areas.Config.Pages.QueuesOut
{
    public class IndexModel : PageModel
    {
        private readonly QueueOutService _queueService;

        public IndexModel(QueueOutService queueService)
        {
            _queueService = queueService;
        }

        public IList<QueueOut> QueueList { get; set; } = default!;

        public async Task<IActionResult> OnGet()
        {
            QueueList = await _queueService.GetListAsync();

            return Page();
        }
    }
}
