using Microsoft.AspNetCore.Mvc;
using Pie.Data.Models;
using Pie.Data.Services;

namespace Pie.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(Service1c))]
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryAreasController : ControllerBase
    {
        private readonly DeliveryAreaService _deliveryAreaService;

        public DeliveryAreasController(DeliveryAreaService deliveryAreaService)
        {
            _deliveryAreaService = deliveryAreaService;

        }

        // GET: api/DeliveryAreas
        [HttpGet]
        public async Task<ActionResult<List<DeliveryArea>>> GetDeliveryAreas(int skip = AppConfig.SKIP, int take = AppConfig.TAKE)
        {
            List<DeliveryArea> deliveryAreas = await _deliveryAreaService.GetListAsync(skip, take);

            return Ok(deliveryAreas);
        }


        // GET: api/DeliveryAreas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryArea>> GetDeliveryArea(Guid id)
        {
            var deliveryArea = await _deliveryAreaService.GetAsync(id);

            if (deliveryArea == null)
                return NotFound();

            return Ok(deliveryArea);
        }

        // PUT: api/DeliveryAreas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryArea(Guid id, DeliveryArea deliveryArea)
        {
            if (id != deliveryArea.Id)
                return BadRequest();

            await _deliveryAreaService.UpdateAsync(deliveryArea);

            return NoContent();
        }

        // POST: api/DeliveryAreas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostDeliveryArea(DeliveryArea deliveryArea)
        {
            await _deliveryAreaService.CreateOrUpdateAsync(deliveryArea);

            return CreatedAtAction("GetDeliveryArea", new { id = deliveryArea.Id }, deliveryArea);
        }

        // DELETE: api/DeliveryAreas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryArea(Guid id)
        {
            await _deliveryAreaService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("tree")]
        public async Task<IActionResult> GetTree()
        {
            List<DeliveryAreaTreeNode> result = await _deliveryAreaService.GetTree();
            return Ok(result);
        }

        [HttpGet("flat")]
        public async Task<IActionResult> GetFlat(bool includeParent)
        {
            Dictionary<Guid, string> result = await _deliveryAreaService.GetFlatList(includeParent);
            return Ok(result);
        }
    }
}
