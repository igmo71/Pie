using Pie.Data.Models;

namespace Pie.Connectors.Connector1c.Models1c
{
    public class DeliveryAreaDto1c
    {
        public string Ref_Key { get; set; } = null!;
        //public string? DataVersion { get; set; }
        public bool DeletionMark { get; set; }
        public string? Parent_Key { get; set; }
        public bool IsFolder { get; set; }
        public string? Description { get; set; }
        //public string? Описание { get; set; }
        //public bool Predefined { get; set; }
        //public string? PredefinedDataName { get; set; }
        //public string? ParentnavigationLinkUrl { get; set; }

        public static DeliveryArea MapToDeliveryArea(DeliveryAreaDto1c dto)
        {
            DeliveryArea deliveryArea = new DeliveryArea()
            {
                Id = Guid.Parse(dto.Ref_Key),
                Name = dto.Description,
                Active = !dto.DeletionMark,
                IsFolder = dto.IsFolder,
                ParentId = dto.Parent_Key == null || dto.Parent_Key == Guid.Empty.ToString() ? null : Guid.Parse(dto.Parent_Key)
            };
            return deliveryArea;
        }

        public static List<DeliveryArea> MapToDeliveryAreaList(List<DeliveryAreaDto1c> dtos)
        {
            List<DeliveryArea> deliveryAreas = new();
            foreach (var dto in dtos)
            {
                deliveryAreas.Add(MapToDeliveryArea(dto));
            }
            return deliveryAreas;
        }
    }
}
