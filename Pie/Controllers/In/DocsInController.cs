using Microsoft.AspNetCore.Mvc;
using Pie.Data.Models.In;
using Pie.Data.Services.In;

namespace Pie.Controllers.In
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocsInController : ControllerBase
    {
        private readonly DocInService _docService;

        public DocsInController(DocInService docService)
        {
            _docService = docService;
        }

        // GET: api/DocsIn
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocIn>>> GetDocs()
        {
            var doc = await _docService.GetListAsync();
            return Ok(doc);
        }

        // GET: api/DocsIn/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocIn>> GetDoc(Guid id)
        {
            var doc = await _docService.GetAsync(id);

            if (doc == null)
                return NotFound();

            return Ok(doc);
        }

        // PUT: api/DocsIn/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoc(Guid id, DocIn doc)
        {
            if (id != doc.Id)
                return BadRequest();

            await _docService.UpdateAsync(doc);

            return NoContent();
        }

        // POST: api/DocsIn
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocIn>> PostDoc(DocInDto docDto)
        {
            var result = await _docService.CreateAsync(docDto);

            return CreatedAtAction("GetDoc", new { id = result.Id }, result);
        }

        // DELETE: api/DocsIn/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoc(Guid id)
        {
            await _docService.DeleteAsync(id);

            return NoContent();
        }
    }
}
