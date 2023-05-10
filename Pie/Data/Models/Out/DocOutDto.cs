using Mapster;

namespace Pie.Data.Models.Out
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

        public static DocOut MapToDocOut(DocOutDto dto)
        {
            DocOut doc = new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Number = dto.Number,
                DateTime = dto.DateTime,
                Active = dto.Active,
                WarehouseId = dto.WarehouseId,
                StatusKey = dto.StatusKey,
                QueueKey = dto.QueueKey,
                QueueNumber = dto.QueueNumber,
                Comment = dto.Comment,
                ShipDateTime = dto.ShipDateTime
            };

            if (dto.Products != null)
            {
                foreach (var item in dto.Products)
                {
                    DocOutProduct product = new()
                    {
                        DocId = dto.Id,
                        ProductId = item.ProductId,
                        CountPlan = item.Count, // TODO: item.CountPlan
                        CountFact = item.Count  // TODO: item.CountFact
                    };
                    doc.Products.Add(product);
                }
            }

            if (dto.BaseDocs != null)
            {
                foreach (var item in dto.BaseDocs)
                {
                    DocOutBaseDoc baseDoc = new()
                    {
                        DocOutId = dto.Id,
                        BaseDocId = item.BaseDocId
                    };
                    doc.BaseDocs.Add(baseDoc);
                }
            }

            return doc;
        }
    }

    public class ProductDto : MappedModel
    {
        public Guid ProductId { get; set; }
        public float Count { get; set; }

        public override void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ProductDto, DocOutProduct>()
                .IgnoreNullValues(true)
                .RequireDestinationMemberSource(true)
                .Map(dst => dst.CountPlan, src => src.Count)
                .Map(dst => dst.CountFact, src => src.Count);
        }
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
