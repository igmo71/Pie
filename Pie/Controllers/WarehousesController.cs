using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pie.Connectors.Connector1c;
using Pie.Data.Models;
using Pie.Data.Services;

namespace Pie.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(Service1c))]
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly WarehouseService _warehouseService;

        public WarehousesController(WarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        // GET: api/Warehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouses(int skip = AppConfig.SKIP, int take = AppConfig.TAKE)
        {
            var warehouses = await _warehouseService.GetListAsync(skip, take);

            return Ok(warehouses);
        }

        // GET: api/Warehouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> GetWarehouse(Guid id)
        {
            var warehouse = await _warehouseService.GetAsync(id);

            if (warehouse == null)
                return NotFound();

            return warehouse;
        }

        // POST: api/Warehouses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Warehouse>> PostWarehouse(Warehouse warehouse)
        {
            await _warehouseService.CreateOrUpdateAsync(warehouse);

            return CreatedAtAction(nameof(GetWarehouse), new { id = warehouse.Id }, warehouse);
        }

        // PUT: api/Warehouses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(Guid id, Warehouse warehouse)
        {
            if (id != warehouse.Id)
                return BadRequest();

            await _warehouseService.UpdateAsync(warehouse);

            return NoContent();
        }

        // DELETE: api/Warehouses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(Guid id)
        {
            await _warehouseService.DeleteAsync(id);

            return NoContent();
        }
    }
}
