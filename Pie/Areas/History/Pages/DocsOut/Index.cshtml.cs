using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data;
using Pie.Data.Models.Out;

namespace Pie.Areas.History.Pages.DocsOut
{
    public class IndexModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public IndexModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<DocOutHistory> DocOutHistory { get; set; } = default!;
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(string searchString)
        {
            CurrentFilter = searchString;
            try
            {
                IQueryable<DocOutHistory> query = _context.DocsOutHistory.AsNoTracking()
                    .Include(d => d.Doc)
                    .Include(d => d.Status)
                    .Include(d => d.User);

                if (!string.IsNullOrEmpty(searchString))
                    query = query.Where(d => d.Doc != null && d.Doc.Name != null && d.Doc.Name.ToUpper().Contains(searchString.ToUpper()));

                DocOutHistory = await query
                    .OrderByDescending(d => d.DateTime)
                    .Take(50)
                    .ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
