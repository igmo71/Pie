﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Areas.Config.Pages.StatusesOut
{
    public class IndexModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public IndexModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<StatusOut> StatusOut { get; set; } = default!;

        public async Task OnGetAsync()
        {
            StatusOut = await _context.StatusesOut.IgnoreQueryFilters().ToListAsync();
        }
    }
}
