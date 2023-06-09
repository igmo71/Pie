using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Areas.Config.Pages.ChangeReasonsOut
{
    public class IndexModel : PageModel
    {
        private readonly ChangeReasonOutService _changeReasonService;

        public IndexModel(ChangeReasonOutService changeReasonService)
        {
            _changeReasonService = changeReasonService;
        }

        public IList<ChangeReasonOut> ChangeReasons { get; set; } = default!;

        public async Task OnGetAsync()
        {
            ChangeReasons = await _changeReasonService.GetListAsync();
        }
    }
}
