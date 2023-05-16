using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;
using Pie.Data.Services.Application;

namespace Pie.Data.Services.Out
{
    public class DocOutProductHistoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationUserService _userService;

        public DocOutProductHistoryService(ApplicationDbContext context, ApplicationUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task CreateAsync(DocOut doc)
        {
            foreach (var product in doc.Products)
            {
                if (product.ChangeReasonId != null)
                {
                    DocOutProductHistory docProductHistory = new DocOutProductHistory()
                    {
                        DateTime = DateTime.Now,
                        UserId = _userService.CurrentUserId,
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
                .Include(d => d.User);

            if (!string.IsNullOrEmpty(searchString))
                queryProduct = queryProduct.Where(d => d.Doc != null && d.Doc.Name != null && d.Doc.Name.ToUpper().Contains(searchString.ToUpper()));

            if (docId != null)
                queryProduct = queryProduct.Where(d => d.Doc != null && d.DocId == docId);

            List<DocOutProductHistory> result = await queryProduct
                .OrderBy(d => d.Doc.Name).ThenBy(d => d.LineNumber).ThenBy(d => d.DateTime)
                .Take(50)
                .ToListAsync();

            return result;
        }
    }
}
