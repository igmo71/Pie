using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.In;

namespace Pie.Data.Services.In
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
            var docs = await _context.DocsIn.AsNoTracking()
                .Include(d => d.Status)
                .Include(d => d.Queue)
                .Include(d => d.Warehouse)
                .ToListAsync();
            return docs;
        }

        public async Task<DocIn?> GetDocAsync(Guid id)
        {
            var doc = await _context.DocsIn.AsNoTracking()
                .Include(d => d.Status)
                .Include(d => d.Queue)
                .Include(d => d.Warehouse)
                .Include(d => d.Products).ThenInclude(p => p.Product)
                .Include(d => d.BaseDocs).ThenInclude(b => b.BaseDoc)
                .FirstOrDefaultAsync(d => d.Id == id);
            return doc;
        }

        public async Task<Dictionary<int, List<DocIn>>> GetDictionaryByQueue()
        {
            var result = await _context.DocsIn.AsNoTracking()
                .Include(d => d.Status)
                .Include(d => d.Queue)
                .Include(d => d.Warehouse)
                .OrderBy(d => d.StatusKey.GetValueOrDefault())
                    .ThenBy(d => d.QueueKey.GetValueOrDefault())
                .GroupBy(e => e.QueueKey.GetValueOrDefault())
                .ToDictionaryAsync(g => g.Key, g => g.ToList());
            return result;
        }

        public async Task<DocIn> CreateDocAsync(DocIn doc)
        {
            if (DocExists(doc.Id))
                await UpdateDocAsync(doc.Id, doc);

            _context.DocsIn.Add(doc);
            await _context.SaveChangesAsync();

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
            return _context.DocsIn.IgnoreQueryFilters().Any(e => e.Id == id);
        }
    }
}
