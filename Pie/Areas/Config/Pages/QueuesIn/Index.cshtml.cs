using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.In;
using Pie.Data.Services.In;

namespace Pie.Areas.Config.Pages.QueuesIn
{
    public class IndexModel : PageModel
    {
        private readonly QueueInService _queueService;

        public IndexModel(QueueInService queueService)
        {
            _queueService = queueService;
        }

        public IList<QueueIn> Queues { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Queues = await _queueService.GetListAsync();
        }
    }
}
