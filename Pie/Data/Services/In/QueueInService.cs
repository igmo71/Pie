using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.In;

namespace Pie.Data.Services.In
{
    public class QueueInService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly ILogger<QueueInService> _logger;

        public QueueInService(
            ApplicationDbContext context,
            IDbContextFactory<ApplicationDbContext> contextFactory, 
            ILogger<QueueInService> logger)
        {
            _context = context;
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<List<QueueIn>> GetListAsync()
        {
            var queues = await _context.QueuesIn.AsNoTracking().OrderBy(q => q.Key).ToListAsync();

            return queues;
        }

        public async Task<List<QueueIn>> GetListActiveAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            var queues = await context.QueuesIn.AsNoTracking().Where(e => e.Active).OrderBy(q => q.Key).ToListAsync();

            return queues;
        }

        public async Task<QueueIn?> GetAsync(Guid id)
        {
            var queue = await _context.QueuesIn.FindAsync(id);  

            return queue;
        }

        public async Task<QueueIn?> GetAsync(int key)
        {
            var queue = await _context.QueuesIn.FirstOrDefaultAsync(q => q.Key == key);

            return queue;
        }

        public async Task<QueueIn> CreateAsync(QueueIn queue)
        {
            if (Exists(queue.Id))
            {
                await UpdateAsync( queue);
            }
            else
            {
                _context.QueuesIn.Add(queue);
                await _context.SaveChangesAsync();
            }

            return queue;
        }

        public async Task UpdateAsync(QueueIn queue)
        {
            _context.Entry(queue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(queue.Id))
                {
                    throw new ApplicationException($"QueueInService UpdateQueueAsync NotFount {queue.Id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var queue = await _context.QueuesIn.FindAsync(id)
                ?? throw new ApplicationException($"QueueInService DeleteQueueAsync NotFount {id}");
            _context.QueuesIn.Remove(queue);
            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid id)
        {
            return _context.QueuesIn.Any(e => e.Id == id);
        }

    }
}
