﻿using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;
using Pie.Data.Models.In;
using Pie.Data.Models.Out;

namespace Pie.Data.Services
{
    public class BaseDocService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BaseDocService> _logger;

        public BaseDocService(
            ApplicationDbContext context,
            ILogger<BaseDocService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<BaseDoc>> GetListAsync()
        {
            var baseDocs = await _context.BaseDocs.AsNoTracking().ToListAsync();

            return baseDocs;
        }

        public async Task<BaseDoc?> GetAsync(Guid id)
        {
            var baseDoc = await _context.BaseDocs.FindAsync(id);

            return baseDoc;
        }

        public async Task<BaseDoc> CreateAsync(BaseDoc baseDoc)
        {
            _context.BaseDocs.Add(baseDoc);

            await _context.SaveChangesAsync();

            return baseDoc;
        }

        public async Task UpdateAsync(BaseDoc baseDoc)
        {
            BaseDoc? entity = _context.BaseDocs.FirstOrDefault(e => e.Id == baseDoc.Id);
            if (entity == null) return;

            entity.Name = baseDoc.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(baseDoc.Id))
                {
                    throw new ApplicationException($"BaseDocService UpdateBaseDocAsync NotFount {baseDoc.Id}", ex);
                }
                else
                {
                    throw;
                }
            }
            //catch(Exception ex)
            //{
            //    throw;
            //}
        }

        public async Task CreateOrUpdateAsync(BaseDoc baseDoc)
        {
            if (Exists(baseDoc.Id))
            {
                await UpdateAsync(baseDoc);
            }
            else
            {
                await CreateAsync(baseDoc);
            }
        }

        public async Task CreateOrUpdateRangeAsync(List<BaseDoc> baseDocs)
        {
            foreach (BaseDoc baseDoc in baseDocs)
            {
                await CreateOrUpdateAsync(baseDoc);
            }
        }

        internal async Task CreateOrUpdateRangeAsync(List<DocInBaseDocDto> dtoList)
        {
            List<BaseDoc> baseDocs = DocInBaseDocDto.MapToBaseDocList(dtoList);

            await CreateOrUpdateRangeAsync(baseDocs);
        }
        internal async Task CreateOrUpdateRangeAsync(List<DocOutBaseDocDto> dtoList)
        {
            List<BaseDoc> baseDocs = DocOutBaseDocDto.MapToBaseDocList(dtoList);

            await CreateOrUpdateRangeAsync(baseDocs);
        }

        public async Task DeleteAsync(Guid id)
        {
            var baseDoc = await _context.BaseDocs.FindAsync(id)
                ?? throw new ApplicationException($"BaseDocService DeleteBaseDocAsync NotFount {id}");

            _context.BaseDocs.Remove(baseDoc);

            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid id)
        {
            return _context.BaseDocs.Any(e => e.Id == id);
        }
    }
}
