using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pie.Data;
using Pie.Data.Models.Out;

namespace Pie.Areas.History.Pages.DocsOut.Products
{
    public class IndexModel : PageModel
    {
        private readonly Pie.Data.ApplicationDbContext _context;

        public IndexModel(Pie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<DocOutProductHistory> DocOutProductHistory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            DocOutProductHistory = await _context.DocOutProductsHistory
                .Include(d => d.ChangeReason)
                .Include(d => d.Doc)
                .Include(d => d.Product)
                .Include(d => d.User).ToListAsync();    
        }
    }
}
