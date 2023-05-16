using Microsoft.EntityFrameworkCore;
using Pie.Common;
using Pie.Connectors.Connector1c;
using Pie.Data.Models;
using Pie.Data.Models.Out;
using Pie.Data.Services.Application;

namespace Pie.Data.Services.Out
{
    public class DocOutService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly BaseDocService _baseDocService;
        private readonly QueueOutService _queueService;
        private readonly Service1c _service1C;
        private readonly ApplicationUserService _userService;
        private readonly ILogger<DocOutService> _logger;

        public event EventHandler<Guid>? DocCreated;

        public DocOutService(ApplicationDbContext context, IDbContextFactory<ApplicationDbContext> contextFactory,
            BaseDocService baseDocService, QueueOutService queueService, Service1c service1C,
            ApplicationUserService userService,
            ILogger<DocOutService> logger)
        {
            _context = context;
            //_context = contextFactory.CreateDbContext();
            _contextFactory = contextFactory;
            _baseDocService = baseDocService;
            _queueService = queueService;
            _service1C = service1C;
            _userService = userService;
            _logger = logger;
        }
        public async Task<List<DocOut>> GetListAsync()
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

        public async Task<DocOutVm?> GetVmAsync(Guid id)
        {
            DocOutVm? result = new();
            result.Value = await GetAsync(id);
            result.UserName = await _userService.GetCurrentUserNameAsync();
            result.Barcode = BarcodeGuidConvert.GetBarcodeBase64(id);

            return result;
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
            _ = await CreateAsync(doc);

            return docDto;
        }

        public async Task<DocOut> CreateAsync(DocOut doc)
        {
            if (Exists(doc.Id))
                await DeleteAsync(doc.Id);

            await SetShipDateTime(doc);

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
