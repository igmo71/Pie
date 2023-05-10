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
                        DocId = dto.Id,
                        BaseDocId = item.BaseDocId
                    };
                    doc.BaseDocs.Add(baseDoc);
                }
            }

            return doc;
        }
    }

    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public float Count { get; set; }
        //TODO:
        //public float CountPlan { get; set; }
        //public float CountFact { get; set; }
    }

    public class BaseDocDto
    {
        public Guid BaseDocId { get; set; }
        public string? Name { get; set; }

        public static BaseDoc MapToBaseDoc(BaseDocDto dto)
        {
            BaseDoc baseDoc = new()
            {
                Id = dto.BaseDocId,
                Name = dto.Name,
                Active = true
            };
            
            return baseDoc;
        }

        public static List<BaseDoc> MapToBaseDocList(List<BaseDocDto> dtos)
        {
            List<BaseDoc> list = new();
            foreach (var dto in dtos)
            {
                list.Add(MapToBaseDoc(dto));
            }

            return list;
        }
    }
}
