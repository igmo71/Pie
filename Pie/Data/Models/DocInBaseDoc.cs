namespace Pie.Data.Models
{
    public class DocInBaseDoc
    {
        public Guid Id { get; set; }

        public Guid DocInId { get; set; }
        public DocIn? DocIn { get; set; }

        public Guid BaseDocId { get; set; }
        public BaseDoc? BaseDoc { get; set; }
    }
}
