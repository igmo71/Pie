using System.ComponentModel.DataAnnotations.Schema;

namespace Pie.Data.Models.In
{
    public class DocIn : Doc
    {
        public List<DocInProduct> Products { get; set; } = new();
        public List<DocInBaseDoc> BaseDocs { get; set; } = new();

        public Guid? PartnerId { get; set; }
        public Partner? Partner { get; set; }

        public int? StatusKey { get; set; }
        public StatusIn? Status { get; set; }

        public int? QueueKey { get; set; }
        public QueueIn? Queue { get; set; }

        [NotMapped]
        public float Weight => Products.Sum(x => x.Weight);
    }
}
