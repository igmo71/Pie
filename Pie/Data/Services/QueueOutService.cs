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

        public async Task<IEnumerable<QueueOut>> GetQueuesAsync()
        {
            var queues = await _context.QueuesOut.ToListAsync();
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
                await UpdateQueueAsync(queue.Id, queue);
            }
            else
            {
                _context.QueuesOut.Add(queue);
                await _context.SaveChangesAsync();
            }
            return queue;
        }

        public async Task UpdateQueueAsync(Guid id, QueueOut queue)
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
                    throw new ApplicationException($"QueueOutService UpdateQueueAsync NotFount {id}", ex);
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


        private bool QueueExists(Guid id)
        {
            return _context.QueuesOut.Any(e => e.Id == id);
        }
    }
}
