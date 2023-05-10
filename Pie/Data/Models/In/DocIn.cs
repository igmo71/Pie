using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models.In
{
    public class DocIn : Doc
    {
        public List<DocInProduct> Products { get; set; } = new();
        public List<DocInBaseDoc> BaseDocs { get; set; } = new();

        [MaxLength(250)]
        public string? Comment { get; set; }

        public int? StatusKey { get; set; }
        public StatusIn? Status { get; set; }

        public int? QueueKey { get; set; }
        public QueueIn? Queue { get; set; }
    }
}
