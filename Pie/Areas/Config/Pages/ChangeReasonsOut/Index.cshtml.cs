using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models;
using Pie.Data.Services;

namespace Pie.Areas.Config.Pages.ChangeReasonsOut
{
    public class IndexModel : PageModel
    {
        private readonly ChangeReasonOutService _changeReasonService;

        public IndexModel(ChangeReasonOutService changeReasonService)
        {
            _changeReasonService = changeReasonService;
        }

        public IList<ChangeReasonOut> ChangeReasonOut { get; set; } = default!;

        public async Task OnGetAsync()
        {
            ChangeReasonOut = await _changeReasonService.GetAsync();
        }
    }
}
