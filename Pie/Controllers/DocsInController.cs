using Microsoft.AspNetCore.Mvc;
using Pie.Data.Models;
using Pie.Data.Services;

namespace Pie.Controllers
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
        public async Task<ActionResult<IEnumerable<DocIn>>> GetDocsIn()
        {
            var doc = await _docService.GetDocsAsync();
            return Ok(doc);
        }

        // GET: api/DocsIn/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocIn>> GetDocIn(Guid id)
        {
            var doc = await _docService.GetDocAsync(id);

            if (doc == null)
                return NotFound();

            return Ok(doc);
        }

        // PUT: api/DocsIn/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocIn(Guid id, DocIn doc)
        {
            if (id != doc.Id)
                return BadRequest();

            await _docService.UpdateDocAsync(id, doc);

            return NoContent();
        }

        // POST: api/DocsIn
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocIn>> PostDocIn(DocIn doc)
        {
            var result = _docService.CreateDocAsync(doc);

            return CreatedAtAction("GetDocIn", new { id = result.Id }, result);
        }

        // DELETE: api/DocsIn/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocIn(Guid id)
        {
            await _docService.DeleteDocAsync(id);

            return NoContent();
        }
    }
}
