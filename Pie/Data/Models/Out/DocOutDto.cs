namespace Pie.Data.Models.Out
{
    public class DocOutDto
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
        public List<DocOutProductDto>? Products { get; set; }
        public List<DocOutBaseDocDto>? BaseDocs { get; set; }
        public int? StatusKey { get; set; }
        public int? QueueKey { get; set; }
        public string? QueueNumber { get; set; }
        public DateTime ShipDateTime { get; set; }

        //public Guid? DeliveryAreaId { get; set; }
        //public string? DeliveryAddress { get; set; }

        public static DocOut MapToDocOut(DocOutDto dto)
        {
            DocOut doc = new()
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
                QueueKey = dto.QueueKey,
                QueueNumber = dto.QueueNumber,
                ShipDateTime = dto.ShipDateTime,
                //DeliveryAreaId = dto.DeliveryAreaId,
                //DeliveryAddress = dto.DeliveryAddress,
            };

            if (dto.Products != null && dto.Products.Count > 0)
                doc.Products = DocOutProductDto.MapToDocOutProductList(dto.Id, dto.Products);

            if (dto.BaseDocs != null && dto.BaseDocs.Count > 0)
                doc.BaseDocs = DocOutBaseDocDto.MapToDocOutBaseDocList(dto.Id, dto.BaseDocs);

            return doc;
        }

        public static DocOutDto MapFromDocOut(DocOut doc)
        {
            DocOutDto dto = new()
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
                QueueKey = doc.QueueKey,
                QueueNumber = doc.QueueNumber,
                ShipDateTime = doc.ShipDateTime,
                //DeliveryAreaId = doc.DeliveryAreaId,
                //DeliveryAddress = doc.DeliveryAddress
            };

            if (doc.Products != null && doc.Products.Count > 0)
                dto.Products = DocOutProductDto.MapFromDocOutProductList(doc.Products);

            if (doc.BaseDocs != null && doc.BaseDocs.Count > 0)
                dto.BaseDocs = DocOutBaseDocDto.MapFromDocOutBaseDocList(doc.BaseDocs);

            return dto;
        }
    }
}
