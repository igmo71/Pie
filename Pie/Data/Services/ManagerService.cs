﻿using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Data.Services
{
    public class ManagerService
    {
        private readonly ApplicationDbContext _context;

        public ManagerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Manager>> GetListAsync(int skip = AppConfig.SKIP, int take = AppConfig.TAKE)
        {
            var managers = await _context.Managers.AsNoTracking().Skip(skip).Take(take).ToListAsync();

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
            _context.Entry(manager).State = EntityState.Modified;

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

        public void ExecuteUpdate(Manager manager)
        {
            _context.Managers.Where(m => m.Id == manager.Id)
                .ExecuteUpdate(s => s.SetProperty(m => m.Name, manager.Name));
        }

        public async Task CreateOrUpdateAsync(Manager manager)
        {
            if (Exists(manager.Id))
            {
                ExecuteUpdate(manager);
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
