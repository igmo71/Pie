namespace Pie.Data.Models.In
{
    public class DocInDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Number { get; set; }
        public DateTime DateTime { get; set; }
        public bool Active { get; set; }
        public string? Comment { get; set; }
        public Guid? WarehouseId { get; set; }
        public Guid? ManagerId { get; set; }
        public Guid? PartnerId { get; set; }
        public bool IsTransfer { get; set; }
        public List<DocInProductDto>? Products { get; set; }
        public List<BaseDoc>? BaseDocs { get; set; }
        public int? StatusKey { get; set; }
        public int? QueueKey { get; set; }

        public static DocIn MapToDocIn(DocInDto dto)
        {
            DocIn doc = new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Number = dto.Number,
                DateTime = dto.DateTime,
                Active = dto.Active,
                Comment = dto.Comment,
                WarehouseId = dto.WarehouseId,
                ManagerId = dto.ManagerId,
                PartnerId = dto.PartnerId,
                IsTransfer = dto.IsTransfer,
                StatusKey = dto.StatusKey,
                QueueKey = dto.QueueKey
            };

            if (dto.Products != null && dto.Products.Count > 0)
                doc.Products = DocInProductDto.MapToDocInProductList(dto.Id, dto.Products);


            if (dto.BaseDocs != null && dto.BaseDocs.Count > 0)
            {
                foreach (var item in dto.BaseDocs)
                {
                    doc.BaseDocs.Add(new DocInBaseDoc { DocId = doc.Id, BaseDocId = item.Id });
                }
            }        

            return doc;
        }

        public static DocInDto MapFromDocIn(DocIn doc)
        {
            DocInDto dto = new()
            {
                Id = doc.Id,
                Name = doc.Name,
                Number = doc.Number,
                DateTime = doc.DateTime,
                Active = doc.Active,
                Comment = doc.Comment,
                WarehouseId = doc.WarehouseId,
                ManagerId = doc.ManagerId,
                PartnerId = doc.PartnerId,
                IsTransfer = doc.IsTransfer,
                StatusKey = doc.StatusKey,
                QueueKey = doc.QueueKey
            };

            if (doc.Products != null && doc.Products.Count > 0)
                dto.Products = DocInProductDto.MapFromDocInProductList(doc.Products);

            if (doc.BaseDocs != null && doc.BaseDocs.Count > 0)
            {
                dto.BaseDocs = new List<BaseDoc>();
                foreach (var item in doc.BaseDocs)
                    dto.BaseDocs.Add(new BaseDoc { Id = item.BaseDocId, Name = item.BaseDoc?.Name });
            }

            return dto;
        }
    }
}
