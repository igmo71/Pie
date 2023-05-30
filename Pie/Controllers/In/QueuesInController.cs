using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pie.Connectors.Connector1c;
using Pie.Data.Models.In;
using Pie.Data.Services.In;
using System.Data;

namespace Pie.Controllers.In
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(Service1c))]
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
            var queues = await _queueService.GetListAsync();
            return Ok(queues);
        }

        // GET: api/QueuesIn/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QueueIn>> GetQueue(Guid id)
        {
            var queue = await _queueService.GetAsync(id);

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

            await _queueService.UpdateAsync(queue);

            return NoContent();
        }

        // POST: api/QueuesIn
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QueueIn>> PostQueue(QueueIn queue)
        {
            var result = await _queueService.CreateAsync(queue);

            return CreatedAtAction("GetQueue", new { id = result.Id }, result);
        }

        // DELETE: api/QueuesIn/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQueue(Guid id)
        {
            await _queueService.DeleteAsync(id);

            return NoContent();
        }
    }
}
