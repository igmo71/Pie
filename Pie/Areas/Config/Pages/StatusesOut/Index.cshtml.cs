﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Areas.Config.Pages.StatusesOut
{
    public class IndexModel : PageModel
    {
        private readonly StatusOutService _statusOutService;

        public IndexModel(StatusOutService statusOutService)
        {
            _statusOutService = statusOutService;
        }

        public IList<StatusOut> StatusOut { get; set; } = default!;

        public async Task OnGetAsync()
        {
            StatusOut = await _statusOutService.GetListAsync();
        }
    }
}
