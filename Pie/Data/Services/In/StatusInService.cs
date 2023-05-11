using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.In;

namespace Pie.Data.Services.In
{
    public class StatusInService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StatusInService> _logger;

        public StatusInService(ApplicationDbContext context, ILogger<StatusInService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<StatusIn>> GetStatusesAsync()
        {
            var statuses = await _context.StatusesIn.AsNoTracking()
                .OrderBy(s => s.Key)
                .ToListAsync();
            return statuses;
        }

        public async Task<StatusIn?> GetStatusAsync(Guid id)
        {
            var status = await _context.StatusesIn.FindAsync(id);
            return status;
        }

        public async Task<StatusIn> CreateStatusAsync(StatusIn status)
        {
            if (StatusExists(status.Id))
            {
                await UpdateStatusAsync(status.Id, status);
            }
            else
            {
                _context.StatusesIn.Add(status);
                await _context.SaveChangesAsync();
            }
            return status;
        }

        public async Task UpdateStatusAsync(Guid id, StatusIn status)
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
                    throw new ApplicationException($"StatusInService UpdateStatusAsync NotFount {id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteStatusAsync(Guid id)
        {
            var status = await _context.StatusesIn.FindAsync(id)
                ?? throw new ApplicationException($"StatusInService DeleteStatusAsync NotFount {id}");
            _context.StatusesIn.Remove(status);
            await _context.SaveChangesAsync();
        }

        public bool StatusExists(Guid id)
        {
            return _context.StatusesIn.Any(e => e.Id == id);
        }
    }
}
