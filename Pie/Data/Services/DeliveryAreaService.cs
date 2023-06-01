using Microsoft.EntityFrameworkCore;
using Pie.Common;
using Pie.Connectors.Connector1c.Models1c;
using Pie.Connectors.Connector1c.Services1c;
using Pie.Data.Models;
using System.Collections.Generic;

namespace Pie.Data.Services
{
    public class DeliveryAreaService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DeliveryAreaService> _logger;
        private readonly DeliveryAreaService1c _deliveryAreaService1c;

        public DeliveryAreaService(ApplicationDbContext context, ILogger<DeliveryAreaService> logger, DeliveryAreaService1c deliveryAreaService1c)
        {
            _context = context;
            _logger = logger;
            _deliveryAreaService1c = deliveryAreaService1c;
        }

        public async Task<List<DeliveryArea>> GetListAsync()
        {
            var deliveryArea = await _context.DeliveryAreas.AsNoTracking().ToListAsync();

            return deliveryArea;
        }

        public async Task<DeliveryArea?> GetAsync(Guid id)
        {
            var deliveryArea = await _context.DeliveryAreas.FindAsync(id);

            return deliveryArea;
        }

        public async Task<DeliveryArea> CreateAsync(DeliveryArea deliveryArea)
        {
            if (Exists(deliveryArea.Id))
            {
                await UpdateAsync(deliveryArea);
            }
            else
            {
                _context.DeliveryAreas.Add(deliveryArea);
                await _context.SaveChangesAsync();
            }
            return deliveryArea;
        }

        public async Task<DeliveryArea> CreateAsync(DeliveryAreaDto deliveryAreaDto)
        {
            DeliveryArea deliveryArea = DeliveryAreaDto.MapToDeliveryArea(deliveryAreaDto);

            await CreateAsync(deliveryArea);

            return deliveryArea;
        }

        public async Task<List<DeliveryArea>> CreateRangeAsync(List<DeliveryAreaDto> deliveryAreaDtos)
        {
            List<DeliveryArea> result = new();
            foreach (var delivery in deliveryAreaDtos)
            {
                result.Add(await CreateAsync(delivery));
            }

            return result;
        }

        public async Task<ServiceResult<List<DeliveryArea>>> LoadAsync()
        {
            ServiceResult<List<DeliveryArea>> result = new();
            var list = await _deliveryAreaService1c.GetAsync();
            if (list == null || list.Count == 0)
            {
                result.Message = "DeliveryAreaDto List is Empty";
                return result;
            }
            result.Value = await CreateRangeAsync(list);
            result.IsSuccess = true;
            return result;
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

        public async Task DeleteAsync(Guid id)
        {
            var deliveryArea = await _context.DeliveryAreas.FindAsync(id)
                ?? throw new ApplicationException($"DeliveryAreaService DeleteAsync NotFount {id}");

            _context.DeliveryAreas.Remove(deliveryArea);

            await _context.SaveChangesAsync();
        }

        private bool Exists(Guid id)
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

        public async  Task<Dictionary<Guid, string>> GetFlatList(bool includeParent = false)
        {
            List<DeliveryAreaTreeNode> nodes = await GetTree();
            Dictionary<Guid, string> result = new();

            TreeUtils.GetFlatDictionary(nodes, result, includeParent);

            return result;
        }
    }
}
