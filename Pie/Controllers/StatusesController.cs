﻿using Microsoft.AspNetCore.Mvc;
using Pie.Data.Models;
using Pie.Data.Services;

namespace Pie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly StatusService _statusService;

        public StatusesController(StatusService statusService)
        {
            _statusService = statusService;
        }

        // GET: api/Statuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()
        {
            var statuses = _statusService.GetStatusesAsync();

            return Ok(statuses);
        }

        // GET: api/Statuses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatus(Guid id)
        {
            var status = await _statusService.GetStatusAsync(id);

            if (status == null)
                return NotFound();

            return Ok(status);
        }

        // PUT: api/Statuses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(Guid id, Status status)
        {
            if (id != status.Id)
                return BadRequest();

            await _statusService.UpdateStatusAsync(id, status);

            return NoContent();
        }

        // POST: api/Statuses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Status>> PostStatus(Status status)
        {
            var result = await _statusService.CreateStatusAsync(status);

            return CreatedAtAction("GetStatus", new { id = status.Id }, status);
        }

        // DELETE: api/Statuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(Guid id)
        {
            await _statusService.DeleteStatusAsync(id);

            return NoContent();
        }
    }
}
