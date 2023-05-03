namespace Pie.Data.Models
{
    public class DocOut : Doc
    {
        public int? StatusKey { get; set; }
        public StatusOut? Status { get; set; }

        public int? QueueKey { get; set; }
        public QueueOut? Queue { get; set; }

        public List<DocOutProduct> Products { get; set; } = new();
        public List<DocOutBaseDoc> BaseDocs { get; set; } = new();
    }
}
