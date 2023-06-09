using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pie.Connectors.Connector1c;
using Pie.Data;
using Pie.Data.Models;

namespace Pie.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(Service1c))]
    [Route("api/[controller]")]
    [ApiController]
    public class QueueNumberController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QueueNumberController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var currentQueueNumber = await _context.QueueNumber.FirstOrDefaultAsync()
                ?? throw new ApplicationException("QueueNumber no found.");

            QueueNumber newQueueNumber = currentQueueNumber.Next(currentQueueNumber);

            _context.QueueNumber.Remove(currentQueueNumber);
            _context.QueueNumber.Add(newQueueNumber);
            await _context.SaveChangesAsync();

            return Ok(newQueueNumber.Value);
        }

        [HttpDelete]
        public async Task<IActionResult> Reset()
        {
            var currentQueueNumber = await _context.QueueNumber.FirstOrDefaultAsync()
                ?? throw new ApplicationException("QueueNumber no found.");

            var newQueueNumber = new QueueNumber()
            {
                CharValue = 0,
                NumValue = 0,
                Value = "A00"
            };

            _context.QueueNumber.Remove(currentQueueNumber);
            _context.QueueNumber.Add(newQueueNumber);
            await _context.SaveChangesAsync();

            return Ok(newQueueNumber.Value);
        }
    }
}
