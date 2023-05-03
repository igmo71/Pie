using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pie.Data;
using Pie.Data.Models;
using Pie.Data.Services;

namespace Pie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly WarehouseService _warehousesService;

        public WarehousesController(WarehouseService warehousesService)
        {
            _warehousesService = warehousesService;
        }

        // GET: api/Warehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouses()
        {
            var result = await _warehousesService.GetWarehouses();

            return Ok(result);
        }

        // GET: api/Warehouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> GetWarehouse(Guid id)
        {
            var warehouse = await _warehousesService.GetWarehouseAsync(id);

            if (warehouse == null)
                return NotFound();  

            return warehouse;
        }

        // PUT: api/Warehouses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(Guid id, Warehouse warehouse)
        {
            if (id != warehouse.Id)
                return BadRequest();

            await _warehousesService.UpdateWarehouseAsync(id, warehouse);

            return NoContent();
        }

        // POST: api/Warehouses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Warehouse>> PostWarehouse(Warehouse warehouse)
        {
            var result = await _warehousesService.CreateWarehouseAsync(warehouse);

            return CreatedAtAction("GetWarehouse", new { id = result.Id }, result);
        }

        // DELETE: api/Warehouses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(Guid id)
        {
            await _warehousesService.DeleteWarehouseAsync(id);

            return NoContent();
        }
    }
}
