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
    public class QueuesOutController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QueuesOutController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/QueuesOut
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QueueOut>>> GetQueuesOut()
        {
            return await _context.QueuesOut.ToListAsync();
        }

        // GET: api/QueuesOut/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QueueOut>> GetQueueOut(Guid id)
        {
            var queueOut = await _context.QueuesOut.FindAsync(id);

            if (queueOut == null)
            {
                return NotFound();
            }

            return queueOut;
        }

        // PUT: api/QueuesOut/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQueueOut(Guid id, QueueOut queueOut)
        {
            if (id != queueOut.Id)
            {
                return BadRequest();
            }

            _context.Entry(queueOut).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QueueOutExists(id))
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

        // POST: api/QueuesOut
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QueueOut>> PostQueueOut(QueueOut queueOut)
        {
            _context.QueuesOut.Add(queueOut);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQueueOut", new { id = queueOut.Id }, queueOut);
        }

        // DELETE: api/QueuesOut/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQueueOut(Guid id)
        {
            var queueOut = await _context.QueuesOut.FindAsync(id);
            if (queueOut == null)
            {
                return NotFound();
            }

            _context.QueuesOut.Remove(queueOut);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QueueOutExists(Guid id)
        {
            return _context.QueuesOut.Any(e => e.Id == id);
        }
    }
}
