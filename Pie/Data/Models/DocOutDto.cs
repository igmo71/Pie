namespace Pie.Data.Models
{
    public class DocOutDto
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public string? number { get; set; }
        public DateTime dateTime { get; set; }
        public bool active { get; set; }
        public Guid warehouseId { get; set; }
        public int statusKey { get; set; }
        public int queueKey { get; set; }
        public List<ProductDto>? products { get; set; }
        public List<BaseDocDto>? baseDocs { get; set; }
    }

    public class ProductDto
    {
        public Guid productId { get; set; }
        public int count { get; set; }
    }

    public class BaseDocDto
    {
        public Guid baseDocId { get; set; }
        public string? name { get; set; }
    }
}
