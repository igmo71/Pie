using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Data.Services
{
    public class DocInService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DocInService> _logger;

        public DocInService(ApplicationDbContext context, ILogger<DocInService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<DocIn>> GetDocsAsync()
        {
            var docsIn = await _context.DocsIn.ToListAsync();
            return docsIn;
        }

        public async Task<DocIn?> GetDocAsync(Guid id)
        {
            var docIn = await _context.DocsIn.FindAsync(id);
            return docIn;
        }

        public async Task<DocIn> CreateDocAsync(DocIn doc)
        {
            if (DocExists(doc.Id))
            {
                await UpdateDocAsync(doc.Id, doc);
            }
            else
            {
                _context.DocsIn.Add(doc);
                await _context.SaveChangesAsync();
            }
            return doc;
        }

        public async Task UpdateDocAsync(Guid id, DocIn doc)
        {
            _context.Entry(doc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!DocExists(id))
                {
                    throw new ApplicationException($"DocInService UpdateDocAsync NotFount {id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task DeleteDocAsync(Guid id)
        {
            var doc = await _context.DocsIn.FindAsync(id)
                ?? throw new ApplicationException($"DocInService DeleteDocAsync NotFount {id}");
            _context.DocsIn.Remove(doc);
            await _context.SaveChangesAsync();
        }

        public bool DocExists(Guid id)
        {
            return _context.DocsIn.Any(e => e.Id == id);
        }
    }
}
