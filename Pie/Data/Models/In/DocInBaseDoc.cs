namespace Pie.Data.Models.In
{
    public class DocInBaseDoc : DocBaseDoc
    {
        public Guid DocId { get; set; }
        public DocIn? Doc { get; set; }
    }
}
