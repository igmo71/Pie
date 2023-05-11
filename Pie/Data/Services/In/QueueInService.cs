using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.In;

namespace Pie.Data.Services.In
{
    public class QueueInService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<QueueInService> _logger;

        public QueueInService(ApplicationDbContext context, ILogger<QueueInService> logger)
        {

            _context = context;
            _logger = logger;
        }

        public async Task<List<QueueIn>> GetQueuesAsync()
        {
            var queues = await _context.QueuesIn.AsNoTracking()
                .OrderBy(q => q.Key)
                .ToListAsync();
            return queues;
        }

        public async Task<QueueIn?> GetQueueAsync(Guid id)
        {
            var queue = await _context.QueuesIn.FindAsync(id);
            return queue;
        }
        public async Task<QueueIn> CreateQueueAsync(QueueIn queue)
        {
            if (QueueExists(queue.Id))
            {
                await UpdateQueueAsync(queue.Id, queue);
            }
            else
            {
                _context.QueuesIn.Add(queue);
                await _context.SaveChangesAsync();
            }
            return queue;
        }

        public async Task UpdateQueueAsync(Guid id, QueueIn queue)
        {
            _context.Entry(queue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!QueueExists(id))
                {
                    throw new ApplicationException($"QueueInService UpdateQueueAsync NotFount {id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteQueueAsync(Guid id)
        {
            var queue = await _context.QueuesIn.FindAsync(id)
                ?? throw new ApplicationException($"QueueInService DeleteQueueAsync NotFount {id}");
            _context.QueuesIn.Remove(queue);
            await _context.SaveChangesAsync();
        }

        public bool QueueExists(Guid id)
        {
            return _context.QueuesIn.Any(e => e.Id == id);
        }

    }
}
