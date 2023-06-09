using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Areas.Config.Pages.StatusesOut
{
    public class IndexModel : PageModel
    {
        private readonly StatusOutService _statusService;

        public IndexModel(StatusOutService statusService)
        {
            _statusService = statusService;
        }

        public IList<StatusOut> Statuses { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Statuses = await _statusService.GetListAsync();
        }
    }
}
