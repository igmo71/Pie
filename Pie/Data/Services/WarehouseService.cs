using Microsoft.EntityFrameworkCore;
using Pie.Connectors.Connector1c.Services1c;
using Pie.Data.Models;

namespace Pie.Data.Services
{
    public class WarehouseService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WarehouseService> _logger;
        private readonly WarehouseService1c _warehouseService1c;

        public WarehouseService(ApplicationDbContext context, ILogger<WarehouseService> logger, WarehouseService1c warehouseService1c)
        {
            _context = context;
            _logger = logger;
            _warehouseService1c = warehouseService1c;
        }

        public async Task<List<Warehouse>> GetListAsync()
        {
            var warehouses = await _context.Warehouses.AsNoTracking().ToListAsync();

            return warehouses;
        }

        public async Task<List<Warehouse>> GetListActiveAsync()
        {
            var warehouses = await _context.Warehouses.AsNoTracking().Where(e => e.Active).ToListAsync();

            return warehouses;
        }

        public async Task<Warehouse?> GetAsync(Guid? id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);

            return warehouse;
        }

        public async Task<Warehouse> CreateAsync(Warehouse warehouse)
        {
            if (Exists(warehouse.Id))
            {
                await UpdateAsync(warehouse);
            }
            else
            {
                _context.Warehouses.Add(warehouse);
                await _context.SaveChangesAsync();
            }
            return warehouse;
        }

        public async Task<List<Warehouse>> CreateRangeAsync(List<Warehouse> warehouses)
        {
            List<Warehouse> result = new();
            foreach (var warehouse in warehouses)
            {
                result.Add(await CreateAsync(warehouse));
            }

            return result;
        }

        public async Task<ServiceResult<List<Warehouse>>> LoadAsync()
        {
            ServiceResult<List<Warehouse>> result = new();
            List<Warehouse>? list = await _warehouseService1c.GetListAsync();
            if (list == null || list.Count == 0)
            {
                result.Message = "Warehouse List is Empty";
                return result;
            }
            result.Value = await CreateRangeAsync(list);
            result.IsSuccess = true;
            return result;
        }

        public async Task UpdateAsync(Warehouse warehouse)
        {
            _context.Entry(warehouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(warehouse.Id))
                {
                    throw new ApplicationException($"WarehouseService UpdateWarehouseAsync NotFount {warehouse.Id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id)
                ?? throw new ApplicationException($"WarehouseService UpdateWarehouseAsync NotFount {id}");

            _context.Warehouses.Remove(warehouse);

            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid id)
        {
            return _context.Warehouses.Any(e => e.Id == id);
        }
    }
}
