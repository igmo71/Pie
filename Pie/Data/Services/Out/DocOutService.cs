using Mapster;
using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;
using Pie.Data.Models.Out;

namespace Pie.Data.Services.Out
{
    public class DocOutService
    {
        private readonly ApplicationDbContext _context;
        private readonly BaseDocService _baseDocService;
        private readonly ILogger<DocOutService> _logger;

        public DocOutService(ApplicationDbContext context, BaseDocService baseDocService, ILogger<DocOutService> logger)
        {
            _context = context;
            _baseDocService = baseDocService;
            _logger = logger;
        }
        public async Task<List<DocOut>> GetDocsAsync()
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
            var doc = await _context.DocsOut.AsNoTracking()
                .Include(d => d.Status)
                .Include(d => d.Queue)
                .Include(d => d.Warehouse)
                .Include(d => d.Products).ThenInclude(p => p.Product)
                .Include(d => d.BaseDocs).ThenInclude(b => b.BaseDoc)
                .FirstOrDefaultAsync(d => d.Id == id);
            return doc;
        }

        public async Task<Dictionary<int, List<DocOut>>> GetDictionaryByQueue(SearchOutParameters searchParameters)
        {
            var result = await _context.DocsOut.AsNoTracking()
                .Search(searchParameters)
                .Include(d => d.Status)
                .Include(d => d.Queue)
                .Include(d => d.Warehouse)
                .OrderBy(d => d.StatusKey.GetValueOrDefault())
                    .ThenBy(d => d.QueueKey.GetValueOrDefault())
                    .ThenByDescending(d => d.ShipDateTime)
                .GroupBy(e => e.QueueKey.GetValueOrDefault())
                .ToDictionaryAsync(g => g.Key, g => g.ToList());
            return result;
        }

        public async Task<DocOutDto> CreateDocAsync(DocOutDto docDto)
        {
            List<BaseDoc>? baseDocs = docDto.BaseDocs?.Adapt<List<BaseDoc>>();
            await _baseDocService.CreateRangeAsync(baseDocs);

            //DocOut doc = docDto.Adapt<DocOut>();
            DocOut doc = DocOutDto.MapToDocOut(docDto);
            _ = await CreateDocAsync(doc);

            return docDto;
        }

        public async Task<DocOut> CreateDocAsync(DocOut doc)
        {
            if (DocExists(doc.Id))
                await DeleteDocAsync(doc.Id);

            _context.DocsOut.Add(doc);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DocOutService CreateDocAsync");
                //throw;
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
            return _context.DocsOut.IgnoreQueryFilters().Any(e => e.Id == id);
        }

        public async Task<ServiceResult> SendAsync(DocOut doc)
        {
            ServiceResult result = new();
            result.IsSuccess = true;

            return result;
        }
    }
}
