using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.In;
using Pie.Data.Services.In;

namespace Pie.Areas.Config.Pages.ChangeReasonsIn
{
    public class IndexModel : PageModel
    {
        private readonly ChangeReasonInService _changeReasonService;

        public IndexModel(ChangeReasonInService changeReasonService)
        {
            _changeReasonService = changeReasonService;
        }

        public IList<ChangeReasonIn> ChangeReasons { get; set; } = default!;

        public async Task OnGetAsync()
        {
            ChangeReasons = await _changeReasonService.GetListAsync();
        }
    }
}
