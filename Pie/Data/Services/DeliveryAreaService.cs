using Microsoft.EntityFrameworkCore;
using Pie.Common;
using Pie.Data.Models;

namespace Pie.Data.Services
{
    public class DeliveryAreaService
    {
        private readonly ApplicationDbContext _context;

        public DeliveryAreaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DeliveryArea>> GetListAsync(int skip = 0, int take = 100)
        {
            var deliveryArea = await _context.DeliveryAreas.AsNoTracking().Skip(skip).Take(take).ToListAsync();

            return deliveryArea;
        }

        public async Task<DeliveryArea?> GetAsync(Guid id)
        {
            var deliveryArea = await _context.DeliveryAreas.FindAsync(id);

            return deliveryArea;
        }

        public async Task<DeliveryArea> CreateAsync(DeliveryArea deliveryArea)
        {
            _context.DeliveryAreas.Add(deliveryArea);

            await _context.SaveChangesAsync();

            return deliveryArea;
        }

        public async Task UpdateAsync(DeliveryArea deliveryArea)
        {
            _context.Entry(deliveryArea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(deliveryArea.Id))
                {
                    throw new ApplicationException($"DeliveryAreaService UpdateAsync NotFount {deliveryArea.Id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task CreateOrUpdateAsync(DeliveryArea deliveryArea)
        {
            if (Exists(deliveryArea.Id))
            {
                await UpdateAsync(deliveryArea);
            }
            else
            {
                await CreateAsync(deliveryArea);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var deliveryArea = await _context.DeliveryAreas.FindAsync(id)
                ?? throw new ApplicationException($"DeliveryAreaService DeleteAsync NotFount {id}");

            _context.DeliveryAreas.Remove(deliveryArea);

            await _context.SaveChangesAsync();
        }

        public bool Exists(Guid id)
        {
            var result = _context.DeliveryAreas.Any(e => e.Id == id);

            return result;
        }

        public async Task<List<DeliveryAreaTreeNode>> GetTree()
        {
            List<DeliveryArea> list = await GetListAsync();
            List<DeliveryAreaTreeNode> result = TreeUtils.GetNodes<DeliveryArea, DeliveryAreaTreeNode>(list);
            return result;
        }

        public async Task<Dictionary<Guid, string>> GetFlatList(bool includeParent = false)
        {
            List<DeliveryAreaTreeNode> nodes = await GetTree();
            Dictionary<Guid, string> result = new();

            TreeUtils.GetFlatDictionary(nodes, result, includeParent);

            return result;
        }
    }
}
