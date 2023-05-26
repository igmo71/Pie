using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.In;
using Pie.Data.Services.In;

namespace Pie.Areas.Config.Pages.StatusesIn
{
    public class IndexModel : PageModel
    {
        private readonly StatusInService _statusService;

        public IndexModel(StatusInService statusService)
        {
            _statusService = statusService;
        }

        public IList<StatusIn> Statuses { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Statuses = await _statusService.GetListAsync();
        }
    }
}
