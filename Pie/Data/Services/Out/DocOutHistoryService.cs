using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;
using Pie.Data.Services.Application;
using System.Collections.Generic;

namespace Pie.Data.Services.Out
{
    public class DocOutHistoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationUserService _userService;

        public DocOutHistoryService(ApplicationDbContext context, ApplicationUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task CreateAsync(DocOut doc)
        {
            DocOutHistory docHistory = new DocOutHistory()
            {
                DateTime = DateTime.Now,
                UserId = _userService.CurrentUserId,
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
                    .Include(d => d.User);

            if (!string.IsNullOrEmpty(searchString))
                query = query.Where(d => d.Doc != null && d.Doc.Name != null && d.Doc.Name.ToUpper().Contains(searchString.ToUpper()));

            if (docId != null)
                query = query.Where(d => d.Doc != null && d.DocId == docId);

            List<DocOutHistory> result = await query
                .OrderBy(d => d.Doc.Name).ThenBy(d => d.DateTime)
                .Take(50)
                .ToListAsync();

            return result;
        }
    }
}
