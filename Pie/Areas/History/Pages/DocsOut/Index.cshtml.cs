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
        public IList<DocOutProductHistory> DocOutProductHistory { get; set; } = default!;
        public string? CurrentFilter { get; set; }
        public Guid? DocId { get; set; }

        public async Task OnGetAsync(string? searchString, Guid? docId)
        {
            CurrentFilter = searchString;
            DocId = docId;
            try
            {
                // DocOutHistory
                IQueryable<DocOutHistory> query = _context.DocsOutHistory.AsNoTracking()
                    .Include(d => d.Doc)
                    .Include(d => d.Status)
                    .Include(d => d.User);

                if (!string.IsNullOrEmpty(searchString))
                    query = query.Where(d => d.Doc != null && d.Doc.Name != null && d.Doc.Name.ToUpper().Contains(searchString.ToUpper()));

                if (docId != null)
                    query = query.Where(d => d.Doc != null && d.DocId == docId);

                DocOutHistory = await query
                    .OrderBy(d => d.Doc.Name).ThenBy(d => d.DateTime)
                    .Take(50)
                    .ToListAsync();

                // DocOutProductHistory
                if (!string.IsNullOrEmpty(searchString) || docId != null)
                {
                    IQueryable<DocOutProductHistory> queryProduct = _context.DocOutProductsHistory.AsNoTracking()
                        .Include(d => d.ChangeReason)
                        .Include(d => d.Doc)
                        .Include(d => d.Product)
                        .Include(d => d.User);

                    if (!string.IsNullOrEmpty(searchString))
                        queryProduct = queryProduct.Where(d => d.Doc != null && d.Doc.Name != null && d.Doc.Name.ToUpper().Contains(searchString.ToUpper()));

                    if (docId != null)
                        queryProduct = queryProduct.Where(d => d.Doc != null && d.DocId == docId);

                    DocOutProductHistory = await queryProduct
                        .OrderBy(d => d.Doc.Name).ThenBy(d => d.LineNumber).ThenBy(d => d.DateTime)
                        .Take(50)
                        .ToListAsync();
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
