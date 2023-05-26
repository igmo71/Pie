using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pie.Common;
using Pie.Connectors.Connector1c;
using Pie.Data.Models;
using Pie.Data.Models.Out;
using System.Text.Json;

namespace Pie.Data.Services.Out
{
    public class DocOutService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly BaseDocService _baseDocService;
        private readonly QueueOutService _queueService;
        private readonly Service1c _service1c;
        private readonly DocOutHistoryService _docHistoryService;
        private readonly DocOutProductHistoryService _docProductHistoryService;
        private readonly ILogger<DocOutService> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public static event EventHandler<Guid>? DocCreated;

        public DocOutService(
            ApplicationDbContext context,
            IDbContextFactory<ApplicationDbContext> contextFactory,
            BaseDocService baseDocService,
            QueueOutService queueService,
            Service1c service1c,
            DocOutHistoryService docHistoryService,
            DocOutProductHistoryService docProductHistoryService,
            ILogger<DocOutService> logger,
            IOptions<JsonSerializerOptions> jsonOptions)
        {
            _context = context;
            _contextFactory = contextFactory;
            _baseDocService = baseDocService;
            _queueService = queueService;
            _service1c = service1c;
            _docHistoryService = docHistoryService;
            _docProductHistoryService = docProductHistoryService;
            _logger = logger;
            _jsonOptions = jsonOptions.Value;
        }
        public async Task<List<DocOut>> GetListAsync()
        {
            var docs = await _context.DocsOut.AsNoTracking()
                .Include(d => d.Status)
                .Include(d => d.Queue)
                .Include(d => d.Warehouse)
                .Take(100)
                .ToListAsync();
            return docs;
        }

        public async Task<DocOut?> GetAsync(Guid id)
        {
            var doc = await _context.DocsOut.AsNoTracking()
                .Include(d => d.Status)
                .Include(d => d.Queue)
                .Include(d => d.Warehouse)
                .Include(d => d.Products.OrderBy(p => p.LineNumber)).ThenInclude(p => p.Product)
                .Include(d => d.BaseDocs).ThenInclude(b => b.BaseDoc)
                .FirstOrDefaultAsync(d => d.Id == id);
            return doc;
        }

        public async Task<DocOutVm?> GetVmAsync(Guid id)
        {
            DocOutVm? vm = new();
            vm.Value = await GetAsync(id);
            vm.AtWorkUserName = await _docHistoryService.GetAtWorkUserNameAsync(id);
            vm.Barcode = BarcodeGenerator.GetBarCode128(id);

            return vm;
        }

        public async Task<Dictionary<int, List<DocOut>>> GetDictionaryByQueueAsync(SearchOutParameters searchParameters)
        {
            using var context = _contextFactory.CreateDbContext();

            var result = await context.DocsOut.AsNoTracking()
                .Search(searchParameters)
                .Include(d => d.Status)
                .Include(d => d.Queue)
                .Include(d => d.Warehouse)
                .Include(d => d.Products)
                .OrderBy(d => d.StatusKey.GetValueOrDefault())
                    .ThenBy(d => d.QueueKey.GetValueOrDefault())
                    .ThenByDescending(d => d.ShipDateTime)
                .Take(100)
                .GroupBy(e => e.QueueKey.GetValueOrDefault())
                .ToDictionaryAsync(g => g.Key, g => g.ToList());
            return result;
        }

        public async Task<DocOutDictionaryByQueueVm> GetDictionaryByQueueVmAsync(SearchOutParameters searchParameters)
        {
            DocOutDictionaryByQueueVm vm = new();
            vm.Value = await GetDictionaryByQueueAsync(searchParameters);
            return vm;
        }

        public async Task<Dictionary<int, int>?> GetCountByStatusAsync(SearchOutParameters searchParameters)
        {
            using var context = _contextFactory.CreateDbContext();

            var result = await context.DocsOut.AsNoTracking()
            .Search(searchParameters.ExceptStatus())
            .GroupBy(e => e.StatusKey.GetValueOrDefault())
            .Select(e => new { e.Key, Value = e.Count() })
            .ToDictionaryAsync(e => e.Key, e => e.Value);

            return result;
        }

        public async Task<DocOutDto> CreateAsync(DocOutDto docDto, string? barcode = null)
        {
            if (docDto.BaseDocs != null)
            {
                List<BaseDoc>? baseDocs = BaseDocOutDto.MapToBaseDocList(docDto.BaseDocs);
                await _baseDocService.CreateRangeAsync(baseDocs);
            }

            DocOut doc = DocOutDto.MapToDocOut(docDto);

            doc = await CreateAsync(doc);

            OnDocCreated(doc.Id);

            await _docHistoryService.CreateAsync(doc, barcode);
            await _docProductHistoryService.CreateAsync(doc, barcode);

            return docDto;
        }

        public async Task<DocOut> CreateAsync(DocOut doc)
        {
            if (Exists(doc.Id))
                await DeleteAsync(doc.Id);

            await SetShipDateTime(doc);

            _context.DocsOut.Add(doc);
            await _context.SaveChangesAsync();

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

        protected virtual void OnDocCreated(Guid id)
        {
            DocCreated?.Invoke(this, id);
        }

        public async Task SendTo1cAsync(DocOut doc, string? barcode = null)
        {
            DocOutDto docDto = DocOutDto.MapFromDocOut(doc);

            DocOutDto? result = await _service1c.SendOutAsync(docDto);

            if (result != null)
                _ = await CreateAsync(result, barcode);
        }

        private async Task SetShipDateTime(DocOut doc)
        {
            if (doc.ShipDateTime != DateTime.Parse("0001-01-01 00:00:00"))
                return;

            if (doc.QueueKey == null)
                return;

            var queue = await _queueService.GetAsync((int)doc.QueueKey);
            if (queue == null || (queue.Days == 0 && queue.Hours == 0 && queue.Minutes == 0 && queue.ConcreteTime == new TimeOnly()))
                return;

            doc.ShipDateTime = doc.DateTime.AddDays(queue.Days).AddHours(queue.Hours).AddMinutes(queue.Minutes);

            if (queue.ConcreteTime == TimeOnly.Parse("00:00:00"))
                return;

            doc.ShipDateTime = new DateTime(doc.ShipDateTime.Year, doc.ShipDateTime.Month, doc.ShipDateTime.Day, queue.ConcreteTime.Hour, queue.ConcreteTime.Minute, 0);
        }
    }
}
