using Pie.Data.Models;

namespace Pie.Connectors.Connector1c.Models1c
{
    public class WarehouseDto1c
    {
        public string Ref_Key { get; set; } = null!;
        public string? Description { get; set; }
        public bool DeletionMark { get; set; }

        public static Warehouse MapToWarehouse(WarehouseDto1c dto)
        {
            Warehouse warehouse = new()
            {
                Id = Guid.Parse(dto.Ref_Key),
                Name = dto.Description,
                Active = !dto.DeletionMark
            };
            return warehouse;
        }

        public static List<Warehouse> MapToWarehouseList(List<WarehouseDto1c> dtos)
        {
            List<Warehouse> warehouses = new();
            foreach (var dto in dtos)
            {
                warehouses.Add(MapToWarehouse(dto));
            }
            return warehouses;
        }
    }
}
