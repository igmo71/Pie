using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;
using Pie.Data.Services.Identity;

namespace Pie.Data.Services.Out
{
    public class DocOutHistoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly AppUserService _userService;

        public DocOutHistoryService(ApplicationDbContext context, AppUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task CreateAsync(DocOut doc, string? barcode = null)
        {
            DocOutHistory docHistory = new()
            {
                DateTime = DateTime.Now,
                UserId = await _userService.GetUserIdByBarcodeOrCurrentAsync(barcode),
                DocId = doc.Id,
                StatusKey = doc.StatusKey
            };

            _context.Add(docHistory);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DocOutHistory>> GetListAsync(string? searchString, Guid? docId)
        {
            IQueryable<DocOutHistory> query = _context.DocsOutHistory.AsNoTracking()
                    .Include(d => d.Doc)
                    .Include(d => d.Status)
                    .Include(d => d.User)
                    .Where(d => d.Doc != null);

            if (!string.IsNullOrEmpty(searchString))
                query = query.Where(d => d.Doc!.Name != null && d.Doc.Name.ToUpper().Contains(searchString.ToUpper()));

            if (docId != null)
                query = query.Where(d => d.DocId == docId);

            List<DocOutHistory> result = await query
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
            if(userId == null) return null;
            string? userName = await _userService.GetUserNameAsync(userId);
            return userName;
        }
    }
}
