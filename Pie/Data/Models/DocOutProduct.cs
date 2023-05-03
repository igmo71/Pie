namespace Pie.Data.Models
{
    public class DocOutProduct
    {
        public Guid Id { get; set; }

        public Guid DocOutId { get; set; }
        public DocOut? DocOut { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        public float Count { get; set; }
    }
}
