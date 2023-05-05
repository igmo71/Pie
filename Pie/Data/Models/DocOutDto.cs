using Mapster;

namespace Pie.Data.Models
{
    public class DocOutDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Number { get; set; }
        public DateTime DateTime { get; set; }
        public bool Active { get; set; }
        public Guid WarehouseId { get; set; }
        public int StatusKey { get; set; }
        public int QueueKey { get; set; }
        public string? QueueNumber { get; set; }
        public string? Comment { get; set; }
        public DateTime ShipDateTime { get; set; }
        public List<ProductDto>? Products { get; set; }
        public List<BaseDocDto>? BaseDocs { get; set; }
    }

    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public float Count { get; set; }
    }

    public class BaseDocDto : MappedModel
    {
        public Guid BaseDocId { get; set; }
        public string? Name { get; set; }

        public override void Register(TypeAdapterConfig config)
        {
            config.NewConfig<BaseDocDto, BaseDoc>()
                .RequireDestinationMemberSource(true)
                .Map(dst => dst.Id, src => src.BaseDocId);
            
            config.NewConfig<DocOutBaseDoc, BaseDocDto>()
                .RequireDestinationMemberSource(true)
                .Map(dst => dst.Name, src => src.BaseDoc != null ? src.BaseDoc.Name : string.Empty);
        }
    }
}
