using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Data.Services
{
    public class WarehouseService
    {
        private readonly ApplicationDbContext _context;

        public WarehouseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Warehouse>> GetListAsync(int skip = AppConfig.SKIP, int take = AppConfig.TAKE)
        {
            var warehouses = await _context.Warehouses.AsNoTracking().Skip(skip).Take(take).ToListAsync();

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
            _context.Warehouses.Add(warehouse);

            await _context.SaveChangesAsync();

            return warehouse;
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

        public async Task CreateOrUpdateAsync(Warehouse warehouse)
        {
            if (Exists(warehouse.Id))
            {
                await UpdateAsync(warehouse);
            }
            else
            {
                await CreateAsync(warehouse);
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
