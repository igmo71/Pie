using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Data.Services
{
    public class StatusOutService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StatusOutService> _logger;

        public StatusOutService(ApplicationDbContext context, ILogger<StatusOutService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<StatusOut>> GetStatusesAsync()
        {
            var statuses = await _context.StatusesOut.AsNoTracking().ToListAsync();
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
    }
}
