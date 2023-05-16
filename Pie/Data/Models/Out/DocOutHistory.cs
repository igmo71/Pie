using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Документ")]
        public DocOut? Doc { get; set; }

        public int? StatusKey { get; set; }
        [Display(Name = "Статус")]
        public StatusOut? Status { get; set; }
    }
}
