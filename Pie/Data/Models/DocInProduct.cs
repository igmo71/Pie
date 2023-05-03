namespace Pie.Data.Models
{
    public class DocInProduct
    {
        public Guid Id { get; set; }

        public Guid DocInId { get; set; }
        public DocIn? DocIn { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        public float Count { get; set; }
    }
}
