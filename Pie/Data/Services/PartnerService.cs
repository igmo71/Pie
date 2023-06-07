using Microsoft.EntityFrameworkCore;
using Pie.Data.Models;

namespace Pie.Data.Services
{
    public class PartnerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PartnerService> _logger;

        public PartnerService(ApplicationDbContext context, ILogger<PartnerService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Partner>> GetListAsync()
        {
            var partners = await _context.Partners.AsNoTracking().ToListAsync();

            return partners;
        }

        public async Task<Partner?> GetAsync(Guid id)
        {
            var partner = await _context.Partners.FindAsync(id);

            return partner;
        }

        public async Task<Partner> CreateAsync(Partner partner)
        {
            _context.Partners.Add(partner);

            await _context.SaveChangesAsync();
            
            return partner;
        }

        public async Task UpdateAsync(Partner partner)
        {
            Partner? entity = _context.Partners.Find(partner.Id);
            if (entity == null) return;

            entity.Name = partner.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(partner.Id))
                {
                    throw new ApplicationException($"PartnerService UpdateAsync NotFount {partner.Id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task CreateOrUpdateAsync(Partner partner)
        {
            if (Exists(partner.Id))
            {
                await UpdateAsync(partner);
            }
            else
            {
                await CreateAsync(partner);
            }
        }

        public async Task CreateOrUpdateAsync(PartnerDto dto)
        {
            Partner partner = PartnerDto.MapToPartner(dto);

            await CreateOrUpdateAsync(partner);
        }

        public async Task DeleteAsync(Guid id)
        {
            var partner = await _context.Partners.FindAsync(id)
                ?? throw new ApplicationException($"PartnerService DeleteAsync NotFount {id}");

            _context.Partners.Remove(partner);

            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid id)
        {
            var result = _context.Partners.Any(e => e.Id == id);

            return result;
        }
    }
}
