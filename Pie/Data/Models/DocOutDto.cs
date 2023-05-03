﻿using Mapster;

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
        public List<ProductDto>? Products { get; set; }
        public List<BaseDocDto>? BaseDocs { get; set; }
    }

    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }

    public class BaseDocDto : MappedModel
    {
        public Guid BaseDocId { get; set; }
        public string? Name { get; set; }

        public override void Register(TypeAdapterConfig config)
        {
            config.NewConfig<DocOutBaseDoc, BaseDocDto>()
                .RequireDestinationMemberSource(true)
                .Map(dst => dst.Name, src => src.BaseDoc != null ? src.BaseDoc.Name : string.Empty);
        }
    }
}