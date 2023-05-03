using Mapster;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Data.Services
{
    public class DocOutService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DocOutService> _logger;

        public DocOutService(ApplicationDbContext context, ILogger<DocOutService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<DocOut>> GetDocsAsync()
        {
            var docs = await _context.DocsOut.AsNoTracking()
                .Include(d => d.Status)
                .Include(d => d.Queue)
                .Include(d => d.Warehouse)
                .ToListAsync();
            return docs;
        }

        public async Task<DocOut?> GetDocAsync(Guid id)
        {
            var doc = await _context.DocsOut.FindAsync(id);
            return doc;
        }
        public async Task<DocOut> CreateDocAsync(DocOut doc)
        {
            if (DocExists(doc.Id))
            {
                await UpdateDocAsync(doc.Id, doc);
            }
            else
            {
                _context.DocsOut.Add(doc);
                await _context.SaveChangesAsync();
            }
            return doc;
        }

        public async Task UpdateDocAsync(Guid id, DocOut doc)
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
                    throw new ApplicationException($"DocOutService UpdateDocAsync NotFount {id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task DeleteDocAsync(Guid id)
        {
            var doc = await _context.DocsOut.FindAsync(id)
                ?? throw new ApplicationException($"DocOutService DeleteDocAsync NotFount {id}");
            _context.DocsOut.Remove(doc);
            await _context.SaveChangesAsync();
        }

        public bool DocExists(Guid id)
        {
            return _context.DocsOut.Any(e => e.Id == id);
        }
    }
}
