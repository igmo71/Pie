using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;

namespace Pie.Data.Services.Out
{
    public class QueueOutService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly ILogger<QueueOutService> _logger;

        public QueueOutService(
            ApplicationDbContext context,
            IDbContextFactory<ApplicationDbContext> contextFactory,
            ILogger<QueueOutService> logger)
        {
            _context = context;
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<List<QueueOut>> GetListAsync()
        {
            var queues = await _context.QueuesOut.AsNoTracking().OrderBy(q => q.Key).ToListAsync();

            return queues;
        }

        public async Task<List<QueueOut>> GetListActiveAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            var queues = await context.QueuesOut.AsNoTracking().Where(e => e.Active).OrderBy(q => q.Key).ToListAsync();

            return queues;
        }

        public async Task<QueueOut?> GetAsync(Guid id)
        {
            var queue = await _context.QueuesOut.FindAsync(id);

            return queue;
        }

        public async Task<QueueOut?> GetAsync(int key)
        {
            var queue = await _context.QueuesOut.FirstOrDefaultAsync(q => q.Key == key);

            return queue;
        }

        public async Task<QueueOut> CreateAsync(QueueOut queue)
        {
            if (Exists(queue.Id))
            {
                await UpdateAsync(queue);
            }
            else
            {
                _context.QueuesOut.Add(queue);
                await _context.SaveChangesAsync();
            }

            return queue;
        }

        public async Task UpdateAsync(QueueOut queue)
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
                    throw new ApplicationException($"QueueOutService UpdateQueueAsync NotFount {queue.Id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var queue = await _context.QueuesOut.FindAsync(id)
                ?? throw new ApplicationException($"QueueOutService DeleteQueueAsync NotFount {id}");
            _context.QueuesOut.Remove(queue);
            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid id)
        {
            return _context.QueuesOut.Any(e => e.Id == id);
        }
    }
}
