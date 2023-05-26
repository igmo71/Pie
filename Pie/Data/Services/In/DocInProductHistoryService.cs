using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.In;
using Pie.Data.Services.Identity;

namespace Pie.Data.Services.In
{
    public class DocInProductHistoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly AppUserService _userService;

        public DocInProductHistoryService(ApplicationDbContext context, AppUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task CreateAsync(DocIn doc, string? barcode = null)
        {
            foreach (var product in doc.Products)
            {
                if (product.ChangeReasonId != null)
                {
                    DocInProductHistory docProductHistory = new()
                    {
                        DateTime = DateTime.Now,
                        UserId = await _userService.GetUserIdByBarcodeOrCurrentAsync(barcode),
                        DocId = doc.Id,
                        DocName = doc.Name,
                        ProductId = product.ProductId,
                        LineNumber = product.LineNumber,
                        CountPlan = product.CountPlan,
                        CountFact = product.CountFact,
                        ChangeReasonId = product.ChangeReasonId
                    };
                    _context.DocInProductsHistory.Add(docProductHistory);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<DocInProductHistory>> GetListAsync(string? searchString, Guid? docId)
        {
            IQueryable<DocInProductHistory> queryProduct = _context.DocInProductsHistory.AsNoTracking()
                .Include(d => d.ChangeReason)
                .Include(d => d.Product)
                .Include(d => d.User)
                .Where(d => d.DocId != null);

            if (!string.IsNullOrEmpty(searchString))
                queryProduct = queryProduct
                    .Where(d => d.DocName != null && d.DocName.ToUpper().Contains(searchString.ToUpper()));

            if (docId != null)
                queryProduct = queryProduct.Where(d => d.DocId == docId);

            List<DocInProductHistory> result = await queryProduct
                .OrderBy(d => d.DocName).ThenBy(d => d.LineNumber).ThenBy(d => d.DateTime)
                .Take(50)
                .ToListAsync();

            return result;
        }
    }
}
