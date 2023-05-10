using Microsoft.AspNetCore.Mvc;
using Pie.Data.Models.Out;
using Pie.Data.Services.Out;

namespace Pie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocsOutController : ControllerBase
    {
        private readonly DocOutService _docService;

        public DocsOutController(DocOutService docService)
        {
            _docService = docService;
        }

        // GET: api/DocsOut
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocOut>>> GetDocs()
        {
            var doc = await _docService.GetDocsAsync();
            return Ok(doc);
        }

        // GET: api/DocsOut/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocOut>> GetDoc(Guid id)
        {
            var doc = await _docService.GetDocAsync(id);

            if (doc == null)
                return NotFound();

            return Ok(doc);
        }

        // PUT: api/DocsOut/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoc(Guid id, DocOut doc)
        {
            if (id != doc.Id)
                return BadRequest();

            await _docService.UpdateDocAsync(id, doc);

            return NoContent();
        }

        // POST: api/DocsOut
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocOut>> PostDoc(DocOutDto docDto)
        {

            var result = await _docService.CreateDocAsync(docDto);

            return CreatedAtAction("GetDoc", new { id = result.Id }, result);
        }

        // DELETE: api/DocsOut/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoc(Guid id)
        {
            await _docService.DeleteDocAsync(id);

            return NoContent();
        }
    }
}
