using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Data.Services
{
    public class WarehouseService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductService> _logger;

        public WarehouseService(ApplicationDbContext context, ILogger<ProductService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Warehouse>> GetWarehouses()
        {
            var warehouses = await _context.Warehouses.ToListAsync();
            return warehouses;
        }

        public async Task<Warehouse?> GetWarehouseAsync(Guid id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            return warehouse;
        }

        public async Task<Warehouse> CreateWarehouseAsync(Warehouse warehouse)
        {
            if (WarehouseExists(warehouse.Id))
            {
                await UpdateWarehouseAsync(warehouse.Id, warehouse);
            }
            else
            {
                _context.Warehouses.Add(warehouse);
                await _context.SaveChangesAsync();
            }
            return warehouse;
        }

        public async Task UpdateWarehouseAsync(Guid id, Warehouse warehouse)
        {
            _context.Entry(warehouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!WarehouseExists(id))
                {
                    throw new ApplicationException($"WarehouseService UpdateProductAsync NotFount {id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteWarehouseAsync(Guid id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                throw new ApplicationException($"WarehouseService DeleteProductAsync NotFount {id}");
            }

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
        }

        private bool WarehouseExists(Guid id)
        {
            return _context.Warehouses.Any(e => e.Id == id);
        }
    }
}
