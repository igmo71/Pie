using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pie.Connectors.Connector1c;
using Pie.Data.Models.In;
using Pie.Data.Services.In;

namespace Pie.Controllers.In
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(Service1c))]
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesInController : ControllerBase
    {
        private readonly StatusInService _statusService;

        public StatusesInController(StatusInService statusService)
        {
            _statusService = statusService;
        }

        // GET: api/Statuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusIn>>> GetStatuses()
        {
            var statuses = await _statusService.GetListAsync();

            return Ok(statuses);
        }

        // GET: api/Statuses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusIn>> GetStatus(Guid id)
        {
            var status = await _statusService.GetAsync(id);

            if (status == null)
                return NotFound();

            return Ok(status);
        }

        // PUT: api/Statuses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(Guid id, StatusIn status)
        {
            if (id != status.Id)
                return BadRequest();

            await _statusService.UpdateAsync(status);

            return NoContent();
        }

        // POST: api/Statuses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatusIn>> PostStatus(StatusIn status)
        {
            var result = await _statusService.CreateAsync(status);

            return CreatedAtAction("GetStatus", new { id = result.Id }, result);
        }

        // DELETE: api/Statuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(Guid id)
        {
            await _statusService.DeleteAsync(id);

            return NoContent();
        }
    }
}
