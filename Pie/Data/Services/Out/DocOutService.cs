using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pie.Common;
using Pie.Connectors.Connector1c;
using Pie.Data.Models;
using Pie.Data.Models.Out;
using Pie.Data.Services.Identity;
using System.Text.Json;
using ZXing;

namespace Pie.Data.Services.Out
{
    public class DocOutService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly BaseDocService _baseDocService;
        private readonly QueueOutService _queueService;
        private readonly Service1c _service1c;
        private readonly AppUserService _userService;
        private readonly DocOutHistoryService _docHistoryService;
        private readonly DocOutProductHistoryService _docProductHistoryService;
        private readonly ILogger<DocOutService> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public static event EventHandler<Guid>? DocCreated;

        public DocOutService(ApplicationDbContext context, IDbContextFactory<ApplicationDbContext> contextFactory,
            BaseDocService baseDocService, QueueOutService queueService, Service1c service1c,
            AppUserService userService, DocOutHistoryService docHistoryService, DocOutProductHistoryService docProductHistoryService,
            ILogger<DocOutService> logger, IOptions<JsonSerializerOptions> jsonOptions)
        {
            _context = context;
            _contextFactory = contextFactory;
            _baseDocService = baseDocService;
            _queueService = queueService;
            _service1c = service1c;
            _userService = userService;
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
            vm.UserName = await _userService.GetCurrentUserNameAsync();
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

        public async Task<DocOutDto> CreateAsync(DocOutDto docDto)
        {
            if (docDto.BaseDocs != null)
            {
                List<BaseDoc>? baseDocs = BaseDocDto.MapToBaseDocList(docDto.BaseDocs);
                await _baseDocService.CreateRangeAsync(baseDocs);
            }

            DocOut doc = DocOutDto.MapToDocOut(docDto);
            ServiceResult<DocOut> result = await CreateAsync(doc);

            if (result.IsSuccess) // TODO: Обновить статус !!! Возможно, перезапросить из базы, вызвать событие... 
            {
                await _docHistoryService.CreateAsync(doc);
                await _docProductHistoryService.CreateAsync(doc);
            }

            return docDto;
        }

        public async Task<ServiceResult<DocOut>> CreateAsync(DocOut doc)
        {
            ServiceResult<DocOut> result = new();

            if (Exists(doc.Id))
                await DeleteAsync(doc.Id);

            await SetShipDateTime(doc);

            _context.DocsOut.Add(doc);
            try
            {
                await _context.SaveChangesAsync();
                OnCreated(doc.Id);
            }
            catch (Exception ex)
            {
                throw;
            }

            result.IsSuccess = true;
            result.Value = doc;
            return result;
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

        public async Task<ServiceResult> SendAsync(DocOut doc, string? barcode = null)
        {
            ServiceResult result = new();

            try
            {
                DocOutDto dto = DocOutDto.MapFromDocOut(doc);
                var service1cResult = await _service1c.SendOutAsync(dto);
                result = service1cResult;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                _logger.LogError("DocOutService SendAsync {DocOut}", JsonSerializer.Serialize(doc, _jsonOptions));
                //throw;
            }

            if (result.IsSuccess) // TODO: Обновить статус !!! Возможно, перезапросить из базы, вызвать событие... 
            {
                await _docHistoryService.CreateAsync(doc, barcode);
                await _docProductHistoryService.CreateAsync(doc, barcode);
            }

            return result;
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
