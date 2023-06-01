using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pie.Common;
using Pie.Connectors.Connector1c;
using Pie.Data.Models;
using Pie.Data.Models.In;
using System.Text.Json;

namespace Pie.Data.Services.In
{
    public class DocInService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly BaseDocService _baseDocService;
        private readonly PartnerService _partnerService;
        //private readonly QueueInService _queueService;
        private readonly Service1c _service1c;
        private readonly DocInHistoryService _docHistoryService;
        private readonly DocInProductHistoryService _docProductHistoryService;
        private readonly ILogger<DocInService> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public static event EventHandler<Guid>? DocCreated;

        public DocInService(
            ApplicationDbContext context,
            IDbContextFactory<ApplicationDbContext> contextFactory,
            BaseDocService baseDocService,
            PartnerService partnerService,
            //QueueInService queueService,
            Service1c service1c,
            DocInHistoryService docHistoryService,
            DocInProductHistoryService docProductHistoryService,
            ILogger<DocInService> logger,
            IOptions<JsonSerializerOptions> jsonOptions)
        {
            _context = context;
            _contextFactory = contextFactory;
            _baseDocService = baseDocService;
            _partnerService = partnerService;
            //_queueService = queueService;
            _service1c = service1c;
            _docHistoryService = docHistoryService;
            _docProductHistoryService = docProductHistoryService;
            _logger = logger;
            _jsonOptions = jsonOptions.Value;
        }

        public async Task<List<DocIn>> GetListAsync()
        {
            var docs = await _context.DocsIn.AsNoTracking()
                .Include(d => d.Status)
                .Include(d => d.Queue)
                .Include(d => d.Warehouse)
                .Include(d => d.Partner)
                .Include(d => d.TransferWarehouse)
                .Take(100)
                .ToListAsync();
            return docs;
        }

        public async Task<DocIn?> GetAsync(Guid id)
        {
            var doc = await _context.DocsIn.AsNoTracking()
                .Include(d => d.Status)
                .Include(d => d.Queue)
                .Include(d => d.Warehouse)
                .Include(d => d.Products.OrderBy(p => p.LineNumber)).ThenInclude(p => p.Product)
                .Include(d => d.BaseDocs).ThenInclude(b => b.BaseDoc)
                .Include(d => d.Partner)
                .Include(d => d.TransferWarehouse)
                .FirstOrDefaultAsync(d => d.Id == id);
            return doc;
        }

        public async Task<DocInVm?> GetVmAsync(Guid id)
        {
            DocInVm? vm = new();
            vm.Value = await GetAsync(id);
            vm.AtWorkUserName = await _docHistoryService.GetAtWorkUserNameAsync(id);
            vm.Barcode = BarcodeGenerator.GetBarCode128(id);

            return vm;
        }

        public async Task<Dictionary<int, List<DocIn>>> GetDictionaryByQueueAsync(SearchInParameters searchParameters)
        {
            using var context = _contextFactory.CreateDbContext();

            var result = await _context.DocsIn.AsNoTracking()
                .Search(searchParameters)
                .Include(d => d.Status)
                .Include(d => d.Queue)
                .Include(d => d.Warehouse)
                .Include(d => d.Products)
                .Include(d => d.Partner)
                .OrderBy(d => d.StatusKey.GetValueOrDefault())
                    .ThenBy(d => d.QueueKey.GetValueOrDefault())
                .Take(100)
                .GroupBy(e => e.QueueKey.GetValueOrDefault())
                .ToDictionaryAsync(g => g.Key, g => g.ToList());
            return result;
        }

        public async Task<DocInDictionaryByQueueVm> GetDictionaryByQueueVmAsync(SearchInParameters searchParameters)
        {
            DocInDictionaryByQueueVm vm = new();
            vm.Value = await GetDictionaryByQueueAsync(searchParameters);
            return vm;
        }

        public async Task<Dictionary<int, int>?> GetCountByStatusAsync(SearchInParameters searchParameters)
        {
            using var context = _contextFactory.CreateDbContext();

            var result = await context.DocsIn.AsNoTracking()
            .Search(searchParameters.ExceptStatus())
            .GroupBy(e => e.StatusKey.GetValueOrDefault())
            .Select(e => new { e.Key, Value = e.Count() })
            .ToDictionaryAsync(e => e.Key, e => e.Value);

            return result;
        }

        public async Task<DocInDto> CreateAsync(DocInDto docDto, string? barcode = null)
        {
            await CreateBaseDocs(docDto);
            await CreatePartner(docDto);

            DocIn doc = DocInDto.MapToDocIn(docDto);

            doc = await CreateAsync(doc);

            OnDocCreated(doc.Id);

            await _docHistoryService.CreateAsync(doc, barcode);
            await _docProductHistoryService.CreateAsync(doc, barcode);

            return docDto;
        }

        private async Task CreateBaseDocs(DocInDto docDto)
        {
            if (docDto.BaseDocs != null)
            {
                List<BaseDoc>? baseDocs = BaseDocInDto.MapToBaseDocList(docDto.BaseDocs);
                await _baseDocService.CreateRangeAsync(baseDocs);
            }
        }

        private async Task CreatePartner(DocInDto docDto)
        {
            if (docDto.Partner != null)
            {
                Partner? partner = PartnerDto.MapToPartner(docDto.Partner);
                if (partner != null)
                {
                    await _partnerService.CreateAsync(partner);
                }
            }
        }

        public async Task<DocIn> CreateAsync(DocIn doc)
        {
            if (Exists(doc.Id))
                await DeleteAsync(doc.Id);

            _context.DocsIn.Add(doc);
            await _context.SaveChangesAsync();

            return doc;
        }

        public async Task UpdateAsync(DocIn doc)
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
                    throw new ApplicationException($"DocInService UpdateDocAsync NotFount {doc.Id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var doc = await _context.DocsIn.FindAsync(id)
                ?? throw new ApplicationException($"DocInService DeleteDocAsync NotFount {id}");

            _context.DocsIn.Remove(doc);

            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid id)
        {
            return _context.DocsIn.Any(e => e.Id == id);
        }

        protected virtual void OnDocCreated(Guid id)
        {
            DocCreated?.Invoke(this, id);
        }

        public async Task SendTo1cAsync(DocIn doc, string? barcode = null)
        {
            DocInDto docDto = DocInDto.MapFromDocIn(doc);

            DocInDto? result = await _service1c.SendInAsync(docDto);

            if (result != null)
                _ = await CreateAsync(result, barcode);
        }
    }
}
