using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models.Out
{
    public class DocOut : Doc
    {
        public int? StatusKey { get; set; }
        public StatusOut? Status { get; set; }

        public int? QueueKey { get; set; }
        public QueueOut? Queue { get; set; }

        [MaxLength(5)]
        public string? QueueNumber { get; set; }

        [MaxLength(250)]
        public string? Comment { get; set; }

        public List<DocOutProduct> Products { get; set; } = new();
        public List<DocOutBaseDoc> BaseDocs { get; set; } = new();

        public DateTime ShipDateTime { get; set; }
    }
}
