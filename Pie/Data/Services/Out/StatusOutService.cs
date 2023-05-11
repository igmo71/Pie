using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;

namespace Pie.Data.Services.Out
{
    public class StatusOutService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly ILogger<StatusOutService> _logger;

        public StatusOutService(ApplicationDbContext context, IDbContextFactory<ApplicationDbContext> contextFactory, ILogger<StatusOutService> logger)
        {
            _context = context;
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<List<StatusOut>> GetStatusesAsync()
        {
            var statuses = await _context.StatusesOut.AsNoTracking()
                .OrderBy(s => s.Key)
                .ToListAsync();
            return statuses;
        }

        public async Task<StatusOut?> GetStatusAsync(Guid id)
        {
            var status = await _context.StatusesOut.FindAsync(id);
            return status;
        }

        public async Task<StatusOut> CreateStatusAsync(StatusOut status)
        {
            if (StatusExists(status.Id))
            {
                await UpdateStatusAsync(status.Id, status);
            }
            else
            {
                _context.StatusesOut.Add(status);
                await _context.SaveChangesAsync();
            }
            return status;
        }

        public async Task UpdateStatusAsync(Guid id, StatusOut status)
        {
            _context.Entry(status).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!StatusExists(id))
                {
                    throw new ApplicationException($"StatusOutService UpdateStatusAsync NotFount {id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteStatusAsync(Guid id)
        {
            var status = await _context.StatusesOut.FindAsync(id)
                ?? throw new ApplicationException($"StatusOutService DeleteStatusAsync NotFount {id}");
            _context.StatusesOut.Remove(status);
            await _context.SaveChangesAsync();
        }

        public bool StatusExists(Guid id)
        {
            return _context.StatusesOut.Any(e => e.Id == id);
        }

        public async Task<Dictionary<int, int>?> GetCountByStatusAsync(SearchOutParameters searchParameters)
        {
            using var context = _contextFactory.CreateDbContext();

            var result = await context.DocsOut.AsNoTracking()
            .Search(searchParameters.ExceptStatus())
            .GroupBy(e => e.StatusKey.GetValueOrDefault())
            .Select(e => new { e.Key, Value = e.Count() })
            .ToDictionaryAsync(e => e.Key, e => e.Value);

            return result;
        }
    }
}
