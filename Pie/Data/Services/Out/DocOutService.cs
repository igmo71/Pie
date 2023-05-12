﻿using Microsoft.EntityFrameworkCore;
using Pie.Connectors.Connector1c;
using Pie.Data.Models;
using Pie.Data.Models.Out;

namespace Pie.Data.Services.Out
{
    public class DocOutService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly BaseDocService _baseDocService;
        private readonly Service1c _service1C;
        private readonly ILogger<DocOutService> _logger;

        public event EventHandler<Guid>? DocCreated;

        public DocOutService(ApplicationDbContext context, IDbContextFactory<ApplicationDbContext> contextFactory, BaseDocService baseDocService, Service1c service1C, ILogger<DocOutService> logger)
        {
            _context = context;
            //_context = contextFactory.CreateDbContext();
            _contextFactory = contextFactory;
            _baseDocService = baseDocService;
            _service1C = service1C;
            _logger = logger;
        }
        public async Task<List<DocOut>> GetAsync()
        {
            var docs = await _context.DocsOut.AsNoTracking()
                .Include(d => d.Status)
                .Include(d => d.Queue)
                .Include(d => d.Warehouse)
                .ToListAsync();
            return docs;
        }

        public async Task<DocOut?> GetAsync(Guid id)
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
            using var context = _contextFactory.CreateDbContext();

            var result = await context.DocsOut.AsNoTracking()
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

        public async Task<DocOutDto> CreateAsync(DocOutDto docDto)
        {
            if (docDto.BaseDocs != null)
            {
                List<BaseDoc>? baseDocs = BaseDocDto.MapToBaseDocList(docDto.BaseDocs);
                await _baseDocService.CreateRangeAsync(baseDocs);
            }

            DocOut doc = DocOutDto.MapToDocOut(docDto);
            _ = await CreateAsync(doc);

            return docDto;
        }

        public async Task<DocOut> CreateAsync(DocOut doc)
        {
            if (Exists(doc.Id))
                await DeleteAsync(doc.Id);

            _context.DocsOut.Add(doc);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return doc;
        }

        public async Task UpdateAsync(DocOut doc)
        {
            _context.Entry(doc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(doc.Id))
                {
                    throw new ApplicationException($"DocOutService UpdateDocAsync NotFount {doc.Id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var doc = await _context.DocsOut.FindAsync(id)
                ?? throw new ApplicationException($"DocOutService DeleteDocAsync NotFount {id}");
            _context.DocsOut.Remove(doc);
            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid id)
        {
            return _context.DocsOut.Any(e => e.Id == id);
        }

        protected virtual void OnCreated(Guid id)
        {
            DocCreated?.Invoke(this, id);
        }

        public async Task<ServiceResult> SendAsync(DocOut doc)
        {
            ServiceResult result = new();

            await _service1C.SendOutAsync(doc);

            result.IsSuccess = true;

            return result;
        }
    }
}
