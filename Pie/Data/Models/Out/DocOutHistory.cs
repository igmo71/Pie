namespace Pie.Data.Models.Out
{
    public class DocOutHistory : DocHistory
    {
        public Guid DocId { get; set; }
        public DocOut? Doc { get; set; }

        public Guid StatusOutId { get; set; }
        public StatusOut? StatusOut { get; set; }
    }
}
