using Microsoft.AspNetCore.Mvc;
using Pie.Data.Models;
using Pie.Data.Services;

namespace Pie.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(Service1c))]
    [Route("api/[controller]")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        private readonly ManagerService _managerService;

        public ManagersController(ManagerService managerService)
        {
            _managerService = managerService;
        }

        // GET: api/<ManagersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manager>>> GetManagers(int skip = AppConfig.SKIP, int take = AppConfig.TAKE)
        {
            var managers = await _managerService.GetListAsync(skip, take);

            return Ok(managers);
        }

        // GET api/<ManagersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manager>> GetManager(Guid id)
        {
            var manager = await _managerService.GetAsync(id);

            if (manager == null)
                return NotFound();

            return Ok(manager);
        }

        // POST api/<ManagersController>
        [HttpPost]
        public async Task<ActionResult<Manager>> PostManager([FromBody] Manager manager)
        {
            await _managerService.CreateOrUpdateAsync(manager);

            return CreatedAtAction(nameof(GetManager), new { id = manager.Id }, manager);
        }

        // PUT api/<ManagersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Manager manager)
        {
            if (id != manager.Id)
                return BadRequest();

            await _managerService.UpdateAsync(manager);

            return NoContent();
        }

        // DELETE api/<ManagersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _managerService.DeleteAsync(id);

            return NoContent();
        }
    }
}
