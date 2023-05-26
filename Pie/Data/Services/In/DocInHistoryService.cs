using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.In;
using Pie.Data.Services.Identity;

namespace Pie.Data.Services.In
{
    public class DocInHistoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly AppUserService _userService;


        public DocInHistoryService(ApplicationDbContext context, AppUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task CreateAsync(DocIn doc, string? barcode = null)
        {
            DocInHistory docHistory = new()
            {
                DateTime = DateTime.Now,
                UserId = await _userService.GetUserIdByBarcodeOrCurrentAsync(barcode),
                DocId = doc.Id,
                StatusKey = doc.StatusKey
            };

            _context.Add(docHistory);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DocInHistory>> GetListAsync(string? searchString, Guid? docId)
        {
            IQueryable<DocInHistory> query = _context.DocsInHistory.AsNoTracking()
                    .Include(d => d.Doc)
                    .Include(d => d.Status)
                    .Include(d => d.User)
                    .Where(d => d.Doc != null);

            if (!string.IsNullOrEmpty(searchString))
                query = query.Where(d => d.Doc!.Name != null && d.Doc.Name.ToUpper().Contains(searchString.ToUpper()));

            if (docId != null)
                query = query.Where(d => d.DocId == docId);

            List<DocInHistory> result = await query
                .OrderBy(d => d.Doc!.Name).ThenBy(d => d.DateTime)
                .Take(50)
                .ToListAsync();

            return result;
        }

        public async Task<string?> GetAtWorkUserIdAsync(Guid docId)
        {
            var docHistory = await GetListAsync(null, docId);
            var userId = docHistory.LastOrDefault()?.UserId;
            return userId;
        }

        public async Task<string?> GetAtWorkUserNameAsync(Guid docId)
        {
            var userId = await GetAtWorkUserIdAsync(docId);
            if (userId == null) return null;
            string? userName = await _userService.GetUserNameAsync(userId);
            return userName;
        }
    }
}
