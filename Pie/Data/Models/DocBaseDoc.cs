namespace Pie.Data.Models
{
    public abstract class DocBaseDoc
    {
        public Guid Id { get; set; }

        public Guid BaseDocId { get; set; }
        public BaseDoc? BaseDoc { get; set; }
    }
}
