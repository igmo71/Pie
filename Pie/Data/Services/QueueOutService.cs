using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Data.Services
{
    public class QueueOutService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<QueueOutService> _logger;

        public QueueOutService(ApplicationDbContext context, ILogger<QueueOutService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<QueueOut>> GetQueuesAsync()
        {
            var queues = await _context.QueuesOut.AsNoTracking().OrderBy(q => q.Key).ToListAsync();

            return queues;
        }

        public async Task<QueueOut?> GetQueueAsync(Guid id)
        {
            var queue = await _context.QueuesOut.FindAsync(id);

            return queue;
        }

        public async Task<QueueOut> CreateQueueAsync(QueueOut queue)
        {
            if (QueueExists(queue.Id))
            {
                await UpdateQueueAsync(queue);
            }
            else
            {
                _context.QueuesOut.Add(queue);
                await _context.SaveChangesAsync();
            }

            return queue;
        }

        public async Task UpdateQueueAsync(QueueOut queue)
        {
            _context.Entry(queue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!QueueExists(queue.Id))
                {
                    throw new ApplicationException($"QueueOutService UpdateQueueAsync NotFount {queue.Id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteQueueAsync(Guid id)
        {
            var queue = await _context.QueuesOut.FindAsync(id)
                ?? throw new ApplicationException($"QueueOutService DeleteQueueAsync NotFount {id}");
            _context.QueuesOut.Remove(queue);
            await _context.SaveChangesAsync();
        }

        public bool QueueExists(Guid id)
        {
            return _context.QueuesOut.IgnoreQueryFilters().Any(e => e.Id == id);
        }
    }
}
