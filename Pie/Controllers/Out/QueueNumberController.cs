﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pie.Data;
using Pie.Data.Models.Out;

namespace Pie.Controllers.Out
{
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
