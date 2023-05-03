using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Data.Services
{
    public class StatusService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StatusService> _logger;

        public StatusService(ApplicationDbContext context, ILogger<StatusService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Status>> GetStatusesAsync()
        {
            var statuses = await _context.Statuses.ToListAsync();
            return statuses;
        }

        public async Task<Status?> GetStatusAsync(Guid id)
        {
            var status = await _context.Statuses.FindAsync(id);
            return status;
        }

        public async Task<Status> CreateStatusAsync(Status status)
        {
            if (StatusExists(status.Id))
            {
                await UpdateStatusAsync(status.Id, status);
            }
            else
            {
                _context.Statuses.Add(status);
                await _context.SaveChangesAsync();
            }
            return status;
        }

        public async Task UpdateStatusAsync(Guid id, Status status)
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
                    throw new ApplicationException($"StatusService UpdateStatusAsync NotFount {id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteStatusAsync(Guid id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                throw new ApplicationException($"StatusService DeleteStatusAsync NotFount {id}");
            }

            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();
        }

        private bool StatusExists(Guid id)
        {
            return _context.Statuses.Any(e => e.Id == id);
        }
    }
}
