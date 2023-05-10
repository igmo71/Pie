using Microsoft.AspNetCore.Mvc;
using Pie.Data.Models.In;
using Pie.Data.Services.In;

namespace Pie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueuesInController : ControllerBase
    {
        private readonly QueueInService _queueService;

        public QueuesInController(QueueInService queueService)
        {
            _queueService = queueService;
        }

        // GET: api/QueuesIn
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QueueIn>>> GetQueues()
        {
            var queues = await _queueService.GetQueuesAsync();
            return Ok(queues);
        }

        // GET: api/QueuesIn/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QueueIn>> GetQueue(Guid id)
        {
            var queue = await _queueService.GetQueueAsync(id);

            if (queue == null)
                return NotFound();

            return Ok(queue);
        }

        // PUT: api/QueuesIn/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQueue(Guid id, QueueIn queue)
        {
            if (id != queue.Id)
                return BadRequest();

            await _queueService.UpdateQueueAsync(id, queue);

            return NoContent();
        }

        // POST: api/QueuesIn
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QueueIn>> PostQueue(QueueIn queue)
        {
            var result = await _queueService.CreateQueueAsync(queue);

            return CreatedAtAction("GetQueue", new { id = result.Id }, result);
        }

        // DELETE: api/QueuesIn/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQueue(Guid id)
        {
            await _queueService.DeleteQueueAsync(id);

            return NoContent();
        }
    }
}
