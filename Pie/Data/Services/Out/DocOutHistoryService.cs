using Pie.Data.Models.Out;
using Pie.Data.Services.Application;

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
    }
}
