using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;

namespace Pie.Data.Services.Out
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

        public async Task<List<StatusOut>> GetListAsync()
        {
            var statuses = await _context.StatusesOut.AsNoTracking().OrderBy(s => s.Key).ToListAsync();

            return statuses;
        }

        public async Task<List<StatusOut>> GetListActiveAsync()
        {
            var statuses = await _context.StatusesOut.AsNoTracking().Where(e => e.Active).OrderBy(s => s.Key).ToListAsync();

            return statuses;
        }

        public async Task<StatusOut?> GetAsync(Guid id)
        {
            var status = await _context.StatusesOut.FindAsync(id);

            return status;
        }

        public async Task<StatusOut> CreateAsync(StatusOut status)
        {
            if (Exists(status.Id))
            {
                await UpdateAsync(status);
            }
            else
            {
                _context.StatusesOut.Add(status);
                await _context.SaveChangesAsync();
            }
            return status;
        }

        public async Task UpdateAsync(StatusOut status)
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
                    throw new ApplicationException($"StatusOutService UpdateStatusAsync NotFount {status.Id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var status = await _context.StatusesOut.FindAsync(id)
                ?? throw new ApplicationException($"StatusOutService DeleteStatusAsync NotFount {id}");
            _context.StatusesOut.Remove(status);

            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid id)
        {
            return _context.StatusesOut.Any(e => e.Id == id);
        }
    }
}
