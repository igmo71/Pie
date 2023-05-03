namespace Pie.Data.Models
{
    public class DocIn : Doc
    {
        public int? StatusKey { get; set; }
        public StatusIn? Status { get; set; }

        public int? QueueKey { get; set; }
        public QueueIn? Queue { get; set; }

        public List<DocInProduct>? Products { get; set; }
        public List<DocInBaseDoc>? BaseDocs { get; set; }
    }
}
