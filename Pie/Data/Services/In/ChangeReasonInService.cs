using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.In;

namespace Pie.Data.Services.In
{
    public class ChangeReasonInService
    {
        private readonly ApplicationDbContext _context;

        public ChangeReasonInService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChangeReasonIn>> GetListAsync()
        {
            var changeReasons = await _context.ChangeReasonsIn.AsNoTracking().ToListAsync();

            return changeReasons;
        }

        public async Task<ChangeReasonIn?> GetAsync(Guid? id)
        {
            var changeReason = await _context.ChangeReasonsIn.FirstOrDefaultAsync(m => m.Id == id);

            return changeReason;
        }

        public async Task<ChangeReasonIn> CreateAsync(ChangeReasonIn changeReason)
        {
            if (Exists(changeReason.Id))
            {
                await UpdateAsync(changeReason);
            }
            else
            {
                _context.ChangeReasonsIn.Add(changeReason);
                await _context.SaveChangesAsync();
            }

            return changeReason;
        }

        public async Task UpdateAsync(ChangeReasonIn changeReason)
        {
            _context.Attach(changeReason).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(changeReason.Id))
                {
                    throw new ApplicationException($"ChangeReasonInService UpdateAsync NotFount {changeReason.Id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteAsync(Guid? id)
        {
            var changeReason = await _context.ChangeReasonsIn.FirstOrDefaultAsync(m => m.Id == id)
                ?? throw new ApplicationException($"ChangeReasonInService DeleteAsync NotFount {id}");
            _context.ChangeReasonsIn.Remove(changeReason);
            await _context.SaveChangesAsync();
        }

        private bool Exists(Guid id)
        {
            return _context.ChangeReasonsIn.Any(e => e.Id == id);
        }

    }
}
