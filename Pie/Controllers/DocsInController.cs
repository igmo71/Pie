using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pie.Data;
using Pie.Data.Models;

namespace Pie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocsInController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DocsInController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DocsIn
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocIn>>> GetDocsIn()
        {
            return await _context.DocsIn.ToListAsync();
        }

        // GET: api/DocsIn/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocIn>> GetDocIn(Guid id)
        {
            var docIn = await _context.DocsIn.FindAsync(id);

            if (docIn == null)
            {
                return NotFound();
            }

            return docIn;
        }

        // PUT: api/DocsIn/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocIn(Guid id, DocIn docIn)
        {
            if (id != docIn.Id)
            {
                return BadRequest();
            }

            _context.Entry(docIn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocInExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DocsIn
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocIn>> PostDocIn(DocIn docIn)
        {
            _context.DocsIn.Add(docIn);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocIn", new { id = docIn.Id }, docIn);
        }

        // DELETE: api/DocsIn/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocIn(Guid id)
        {
            var docIn = await _context.DocsIn.FindAsync(id);
            if (docIn == null)
            {
                return NotFound();
            }

            _context.DocsIn.Remove(docIn);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocInExists(Guid id)
        {
            return _context.DocsIn.Any(e => e.Id == id);
        }
    }
}
