﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pie.Connectors.Connector1c;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Controllers.Out
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(Service1c))]
    [Route("api/[controller]")]
    [ApiController]
    public class QueuesOutController : ControllerBase
    {
        private readonly QueueOutService _queueService;

        public QueuesOutController(QueueOutService queueService)
        {
            _queueService = queueService;
        }

        // GET: api/QueuesOut
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QueueOut>>> GetQueues()
        {
            var queues = await _queueService.GetListAsync();
            return Ok(queues);
        }

        // GET: api/QueuesOut/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QueueOut>> GetQueue(Guid id)
        {
            var queue = await _queueService.GetAsync(id);

            if (queue == null)
                return NotFound();

            return Ok(queue);
        }

        // PUT: api/QueuesOut/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQueue(Guid id, QueueOut queue)
        {
            if (id != queue.Id)
                return BadRequest();

            await _queueService.UpdateAsync(queue);

            return NoContent();
        }

        // POST: api/QueuesOut
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QueueOut>> PostQueue(QueueOut queue)
        {
            var result = await _queueService.CreateAsync(queue);

            return CreatedAtAction("GetQueue", new { id = result.Id }, result);
        }

        // DELETE: api/QueuesOut/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQueue(Guid id)
        {
            await _queueService.DeleteAsync(id);

            return NoContent();
        }
    }
}
