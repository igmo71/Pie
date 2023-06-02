using Pie.Data.Models;

namespace Pie.Connectors.Connector1c.Models1c
{
    public class DeliveryAreaDto
    {
        public string Ref_Key { get; set; } = null!;
        public string? DataVersion { get; set; }
        public bool DeletionMark { get; set; }
        public string? Parent_Key { get; set; }
        public bool IsFolder { get; set; }
        public string? Description { get; set; }
        public string? Описание { get; set; }
        public bool Predefined { get; set; }
        public string? PredefinedDataName { get; set; }
        public string? ParentnavigationLinkUrl { get; set; }

        public static DeliveryArea MapToDeliveryArea(DeliveryAreaDto dto)
        {
            DeliveryArea deliveryArea = new DeliveryArea()
            {
                Id = Guid.Parse(dto.Ref_Key),
                Name = dto.Description,
                Active = !dto.DeletionMark,
                IsFolder = dto.IsFolder,
                ParentId = dto.Parent_Key == Guid.Empty.ToString() ? null : Guid.Parse(dto.Parent_Key)
            };
            return deliveryArea;
        }
    }
}
