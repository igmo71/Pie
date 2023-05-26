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

        public async Task<List<StatusIn>> GetListAsync()
        {
            var statuses = await _context.StatusesIn.AsNoTracking().OrderBy(s => s.Key).ToListAsync();

            return statuses;
        }

        public async Task<List<StatusIn>> GetListActiveAsync()
        {
            var statuses = await _context.StatusesIn.AsNoTracking().Where(e => e.Active).OrderBy(s => s.Key).ToListAsync();

            return statuses;
        }

        public async Task<StatusIn?> GetAsync(Guid id)
        {
            var status = await _context.StatusesIn.FindAsync(id);

            return status;
        }

        public async Task<StatusIn> CreateAsync(StatusIn status)
        {
            if (Exists(status.Id))
            {
                await UpdateAsync(status);
            }
            else
            {
                _context.StatusesIn.Add(status);
                await _context.SaveChangesAsync();
            }
            return status;
        }

        public async Task UpdateAsync(StatusIn status)
        {
            _context.Entry(status).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(status.Id))
                {
                    throw new ApplicationException($"StatusInService UpdateStatusAsync NotFount {status.Id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var status = await _context.StatusesIn.FindAsync(id)
                ?? throw new ApplicationException($"StatusInService DeleteStatusAsync NotFount {id}");
            _context.StatusesIn.Remove(status);

            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid id)
        {
            return _context.StatusesIn.Any(e => e.Id == id);
        }
    }
}
