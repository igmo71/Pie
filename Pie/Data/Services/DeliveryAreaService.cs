using Microsoft.EntityFrameworkCore;
using Pie.Connectors.Connector1c.Models1c;
using Pie.Data.Models;
using Services1c = Pie.Connectors.Connector1c.Services1c;

namespace Pie.Data.Services
{
    public class DeliveryAreaService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DeliveryAreaService> _logger;
        private readonly Services1c.DeliveryAreaService _deliveryAreaService1c;

        public DeliveryAreaService(ApplicationDbContext context, ILogger<DeliveryAreaService> logger, Services1c.DeliveryAreaService deliveryAreaService1c)
        {
            _context = context;
            _logger = logger;
            _deliveryAreaService1c = deliveryAreaService1c;
        }

        public async Task<ServiceResult<List<DeliveryAreaDto>>> LoadAsync()
        {
            ServiceResult<List<DeliveryAreaDto>> result = new();
            var list = await _deliveryAreaService1c.GetAsync();
            if (list == null || list.Count == 0)
            {
                result.Message = "DeliveryAreaDto List is Empty";
                return result;
            }
            await AddRangeAsync(list);

            return result;
        }

        public async Task AddRangeAsync(List<DeliveryAreaDto> deliveryAreaDtos)
        {
            foreach (var delivery in deliveryAreaDtos)
                await CreateAsync(delivery);
        }

        public async Task CreateAsync(DeliveryAreaDto deliveryAreaDto)
        {
            DeliveryArea deliveryArea = DeliveryAreaDto.MapToDeliveryArea(deliveryAreaDto);

            await CreateAsync(deliveryArea);
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

        private async Task UpdateAsync(DeliveryArea deliveryArea)
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
                    throw new ApplicationException($"ProductsService UpdateProductAsync NotFount {deliveryArea.Id}", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        private bool Exists(Guid id)
        {
            var result = _context.DeliveryAreas.Any(e => e.Id == id);

            return result;
        }
    }
}
