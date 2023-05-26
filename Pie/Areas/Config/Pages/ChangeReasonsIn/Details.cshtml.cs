﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.In;
using Pie.Data.Services.In;

namespace Pie.Areas.Config.Pages.ChangeReasonsIn
{
    public class DetailsModel : PageModel
    {
        private readonly ChangeReasonInService _changeReasonService;

        public DetailsModel(ChangeReasonInService changeReasonService)
        {
            _changeReasonService = changeReasonService;
        }

        public ChangeReasonIn ChangeReason { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            var changeReason = await _changeReasonService.GetAsync(id);

            if (changeReason == null)
                return NotFound();
            else
                ChangeReason = changeReason;

            return Page();
        }
    }
}
