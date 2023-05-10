namespace Pie.Data.Models.Out
{
    public class DocOutHistory : DocHistory
    {
        public Guid DocId { get; set; }
        public DocOut? Doc { get; set; }

        public Guid StatusId { get; set; }
        public StatusOut? Status { get; set; }
    }
}
