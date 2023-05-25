using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;
using Pie.Data.Services.Identity;

namespace Pie.Data.Services.Out
{
    public class DocOutProductHistoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly AppUserService _userService;

        public DocOutProductHistoryService(ApplicationDbContext context, AppUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task CreateAsync(DocOut doc, string? barcode = null)
        {
            foreach (var product in doc.Products)
            {
                if (product.ChangeReasonId != null)
                {
                    DocOutProductHistory docProductHistory = new()
                    {
                        DateTime = DateTime.Now,
                        UserId = await _userService.GetUserIdByBarcodeOrCurrentAsync(barcode),
                        DocId = doc.Id,
                        ProductId = product.ProductId,
                        LineNumber = product.LineNumber,
                        CountPlan = product.CountPlan,
                        CountFact = product.CountFact,
                        ChangeReasonId = product.ChangeReasonId
                    };
                    _context.DocOutProductsHistory.Add(docProductHistory);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<DocOutProductHistory>> GetListAsync(string? searchString, Guid? docId)
        {
            IQueryable<DocOutProductHistory> queryProduct = _context.DocOutProductsHistory.AsNoTracking()
                .Include(d => d.ChangeReason)
                .Include(d => d.Doc)
                .Include(d => d.Product)
                .Include(d => d.User)
                .Where(d => d.Doc != null);

            if (!string.IsNullOrEmpty(searchString))
                queryProduct = queryProduct
                    .Where(d => d.Doc!.Name != null && d.Doc.Name.ToUpper().Contains(searchString.ToUpper()));

            if (docId != null)
                queryProduct = queryProduct.Where(d => d.DocId == docId);

            List<DocOutProductHistory> result = await queryProduct
                .OrderBy(d => d.Doc!.Name).ThenBy(d => d.LineNumber).ThenBy(d => d.DateTime)
                .Take(50)
                .ToListAsync();

            return result;
        }
    }
}
