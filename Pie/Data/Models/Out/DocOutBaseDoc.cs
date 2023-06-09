namespace Pie.Data.Models.Out
{
    public class DocOutBaseDoc : DocBaseDoc
    {
        public Guid DocId { get; set; }
        public DocOut? Doc { get; set; }
    }
}
