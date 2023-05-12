using Microsoft.EntityFrameworkCore;
using Pie.Data.Models.Out;

namespace Pie.Data.Services.Out
{
    public class ChangeReasonOutService
    {
        private readonly ApplicationDbContext _context;

        public ChangeReasonOutService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChangeReasonOut>> GetListAsync()
        {
            var changeReasons = await _context.ChangeReasonsOut.AsNoTracking().ToListAsync();

            return changeReasons;
        }

        public async Task<ChangeReasonOut?> GetAsync(Guid? id)
        {
            var changeReason = await _context.ChangeReasonsOut.FirstOrDefaultAsync(m => m.Id == id);

            return changeReason;
        }

        public async Task<ChangeReasonOut> CreateAsync(ChangeReasonOut changeReason)
        {
            if (ChangeReasonExists(changeReason.Id))
            {
                await UpdateAsync(changeReason);
            }
            else
            {
                _context.ChangeReasonsOut.Add(changeReason);
                await _context.SaveChangesAsync();
            }

            return changeReason;
        }

        public async Task UpdateAsync(ChangeReasonOut changeReason)
        {
            _context.Attach(changeReason).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ChangeReasonExists(changeReason.Id))
                {
                    throw new ApplicationException($"ChangeReasonOutService UpdateAsync NotFount {changeReason.Id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteAsync(Guid? id)
        {
            var changeReason = await _context.ChangeReasonsOut.FirstOrDefaultAsync(m => m.Id == id)
                ?? throw new ApplicationException($"ChangeReasonOutService DeleteAsync NotFount {id}");
            _context.ChangeReasonsOut.Remove(changeReason);
            await _context.SaveChangesAsync();
        }

        private bool ChangeReasonExists(Guid id)
        {
            return _context.ChangeReasonsOut.Any(e => e.Id == id);
        }
    }
}
