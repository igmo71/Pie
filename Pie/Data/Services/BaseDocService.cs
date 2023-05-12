using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Data.Services
{
    public class BaseDocService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BaseDocService> _logger;

        public BaseDocService(ApplicationDbContext context, ILogger<BaseDocService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<BaseDoc>> GetAsync()
        {
            var baseDocs = await _context.BaseDocs.AsNoTracking().ToListAsync();
            return baseDocs;
        }
        public async Task<BaseDoc?> GetBaseDocAsync(Guid id)
        {
            var baseDoc = await _context.BaseDocs.FindAsync(id);
            return baseDoc;
        }

        public async Task<BaseDoc> CreateAsync(BaseDoc baseDoc)
        {
            if (Exists(baseDoc.Id))
            {
                await UpdateAsync(baseDoc);
            }
            else
            {
                _context.BaseDocs.Add(baseDoc);
                await _context.SaveChangesAsync();
            }
            return baseDoc;
        }

        public async Task CreateRangeAsync(List<BaseDoc>? baseDocs)
        {
            if (baseDocs == null || baseDocs.Count == 0) return;

            foreach (BaseDoc baseDoc in baseDocs)
            {
                await CreateAsync(baseDoc);
            }
        }

        public async Task UpdateAsync(BaseDoc baseDoc)
        {
            _context.Entry(baseDoc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(baseDoc.Id))
                {
                    throw new ApplicationException($"BaseDocService UpdateBaseDocAsync NotFount {baseDoc.Id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var baseDoc = await _context.BaseDocs.FindAsync(id)
                ?? throw new ApplicationException($"BaseDocService DeleteBaseDocAsync NotFount {id}");
            _context.BaseDocs.Remove(baseDoc);
            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid id)
        {
            return _context.BaseDocs.Any(e => e.Id == id);
        }
    }
}
