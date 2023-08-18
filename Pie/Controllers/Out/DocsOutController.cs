using Microsoft.AspNetCore.Mvc;
using Pie.Data.Models.Out;
using Pie.Data.Services.EventBus;
using Pie.Data.Services.Out;

namespace Pie.Controllers.Out
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(Service1c))] // TODO: У Макса Basic Авторизация!
    [Route("api/[controller]")]
    [ApiController]
    public class DocsOutController : ControllerBase
    {
        private readonly DocOutService _docService;
        private readonly AppEventDispatcher _eventDispatcher;

        public DocsOutController(DocOutService docService, AppEventDispatcher eventDispatcher)
        {
            _docService = docService;
            _eventDispatcher = eventDispatcher;
        }

        // GET: api/DocsOut
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocOut>>> GetDocs()
        {
            var doc = await _docService.GetListAsync();
            return Ok(doc);
        }

        // GET: api/DocsOut/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocOut>> GetDoc(Guid id)
        {
            var doc = await _docService.GetAsync(id);

            if (doc == null)
                return NotFound();

            return Ok(doc);
        }

        // GET: api/DocsOut/GetDocByBarcode/5
        [HttpGet("GetDocByBarcode/{barcode}")]
        public async Task<ActionResult<DocOut>> GetDocByBarcode(string barcode)
        {
            var doc = await _docService.GetAsync(barcode);

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

            await _docService.UpdateAsync(doc);

            return NoContent();
        }

        // POST: api/DocsOut
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostDoc(DocOutDto docDto)
        {
            //var jsonString = JsonSerializer.Serialize(docDto);

            await _eventDispatcher.PublishAsync(new DocOutDtoReceivedEvent { Value = docDto });

            var result = await _docService.CreateAsync(docDto);

            return CreatedAtAction("GetDoc", new { id = result.Id }, result);
        }

        // DELETE: api/DocsOut/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoc(Guid id)
        {
            await _docService.DeleteAsync(id);

            return NoContent();
        }
    }
}
