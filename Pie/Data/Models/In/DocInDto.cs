﻿namespace Pie.Data.Models.In
{
    public class DocInDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Number { get; set; }
        public DateTime DateTime { get; set; }
        public bool Active { get; set; }
        public Guid? WarehouseId { get; set; }
        public int? StatusKey { get; set; }
        public int? QueueKey { get; set; }
        public string? Comment { get; set; }
        public List<ProductDto>? Products { get; set; }
        public List<BaseDocDto>? BaseDocs { get; set; }

        public static DocIn MapToDocIn(DocInDto dto)
        {
            DocIn doc = new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Number = dto.Number,
                DateTime = dto.DateTime,
                Active = dto.Active,
                WarehouseId = dto.WarehouseId,
                StatusKey = dto.StatusKey,
                QueueKey = dto.QueueKey,
                Comment = dto.Comment
            };

            if (dto.Products != null && dto.Products.Count > 0)
            {
                foreach (var item in dto.Products)
                {
                    DocInProduct product = new()
                    {
                        DocId = dto.Id,
                        ProductId = item.ProductId,
                        LineNumber = item.LineNumber,
                        CountPlan = item.CountPlan,
                        CountFact = item.CountFact,
                        Weight = item.Weight
                    };
                    doc.Products.Add(product);
                }
            }

            if (dto.BaseDocs != null && dto.BaseDocs.Count > 0)
            {
                foreach (var item in dto.BaseDocs)
                {
                    DocInBaseDoc baseDoc = new()
                    {
                        DocId = dto.Id,
                        BaseDocId = item.BaseDocId
                    };
                    doc.BaseDocs.Add(baseDoc);
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
                WarehouseId = doc.WarehouseId,
                StatusKey = doc.StatusKey,
                QueueKey = doc.QueueKey,
                Comment = doc.Comment
            };

            if (doc.Products != null && doc.Products.Count > 0)
            {
                dto.Products = new List<ProductDto>();
                foreach (var item in doc.Products)
                {
                    ProductDto product = new()
                    {
                        ProductId = item.ProductId,
                        LineNumber = item.LineNumber,
                        CountPlan = item.CountPlan,
                        CountFact = item.CountFact,
                        Weight = item.Weight
                    };
                    dto.Products.Add(product);
                }
            }

            if (doc.BaseDocs != null && doc.BaseDocs.Count > 0)
            {
                dto.BaseDocs = new List<BaseDocDto>();
                foreach (var item in doc.BaseDocs)
                {
                    BaseDocDto baseDoc = new()
                    {
                        BaseDocId = item.BaseDocId,
                        Name = item.BaseDoc?.Name
                    };
                    dto.BaseDocs.Add(baseDoc);
                }
            }

            return dto;
        }
    }

    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public int LineNumber { get; set; }
        public float CountPlan { get; set; }
        public float CountFact { get; set; }
        public float Weight { get; set; }
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