using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Data.Services
{
    public class ManagerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ManagerService> _logger;

        public ManagerService(ApplicationDbContext context, ILogger<ManagerService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Manager>> GetListAsync()
        {
            var managers = await _context.Managers.AsNoTracking().ToListAsync();

            return managers;
        }

        public async Task<Manager?> GetAsync(Guid id)
        {
            var manager = await _context.Managers.FindAsync(id);

            return manager;
        }

        public async Task<Manager> CreateAsync(Manager manager)
        {
            _context.Managers.Add(manager);
            
            await _context.SaveChangesAsync();

            return manager;
        }

        public async Task UpdateAsync(Manager manager)
        {
            Manager? entity = _context.Managers.Find(manager.Id);
            if (entity == null) return;

            entity.Name = manager.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(manager.Id))
                {
                    throw new ApplicationException($"ManagerService UpdateAsync NotFount {manager.Id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task CreateOrUpdateAsync(Manager manager)
        {
            if (Exists(manager.Id))
            {
                await UpdateAsync(manager);
            }
            else
            {
                await CreateAsync(manager);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var manager = await _context.Managers.FindAsync(id)
                ?? throw new ApplicationException($"ManagerService DeleteAsync NotFount {id}");

            _context.Managers.Remove(manager);

            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid id)
        {
            var result = _context.Managers.Any(e => e.Id == id);

            return result;
        }
    }
}
