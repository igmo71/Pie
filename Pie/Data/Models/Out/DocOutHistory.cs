namespace Pie.Data.Models.Out
{
    public class DocOutHistory : DocHistory
    {
        public DocOutHistory()
        { }

        public DocOutHistory(DocOut doc) : this()   
        {
            DocId = doc.Id;
            StatusKey = doc.StatusKey;
        }

        public Guid DocId { get; set; }
        public DocOut? Doc { get; set; }

        public int? StatusKey { get; set; }
        public StatusOut? Status { get; set; }
    }
}
