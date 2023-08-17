using Microsoft.AspNetCore.Mvc;
using Pie.Data.Models;
using Pie.Data.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly PartnerService _partnerService;

        public PartnersController(PartnerService partnerService)
        {
            _partnerService = partnerService;
        }
        // GET: api/<PartnersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Partner>>> GetPartners(int skip = AppConfig.SKIP, int take = AppConfig.TAKE)
        {
            var partners = await _partnerService.GetListAsync(skip, take);

            return Ok(partners);
        }

        // GET api/<PartnersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Partner>> GetPartner(Guid id)
        {
            var partner = await _partnerService.GetAsync(id);

            return Ok(partner);
        }

        // POST api/<PartnersController>
        [HttpPost]
        public async Task<ActionResult<Partner>> PostPartner([FromBody] Partner partner)
        {
            await _partnerService.CreateOrUpdateAsync(partner);

            return CreatedAtAction(nameof(GetPartner), new { id = partner.Id }, partner);
        }

        // PUT api/<PartnersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartner(Guid id, [FromBody] Partner partner)
        {
            if (id != partner.Id)
                return BadRequest();

            await _partnerService.UpdateAsync(partner);

            return NoContent();
        }

        // DELETE api/<PartnersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _partnerService.DeleteAsync(id);

            return NoContent();
        }
    }
}
