namespace Pie.Data.Models.In
{
    public class DocInHistory : DocHistory
    {
        public DocInHistory()
        { }

        public DocInHistory(DocIn doc) : this()
        {
            DocId = doc.Id;
            StatusKey = doc.StatusKey;
        }

        public Guid DocId { get; set; }
        public DocIn? Doc { get; set; }

        public int? StatusKey { get; set; }
        public StatusIn? Status { get; set; }
    }
}
