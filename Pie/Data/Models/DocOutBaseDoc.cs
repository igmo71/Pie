namespace Pie.Data.Models
{
    public class DocOutBaseDoc
    {
        public Guid Id { get; set; }

        public Guid DocOutId { get; set; }
        public DocOut? DocOut { get; set; }

        public Guid BaseDocId { get; set; }
        public BaseDoc? BaseDoc { get; set; }
    }
}
