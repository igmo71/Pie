using System.ComponentModel.DataAnnotations;

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

        public Guid? DocId { get; set; }

        [Display(Name = "Документ")]
        public string? DocName { get; set; }

        public int? StatusKey { get; set; }

        [Display(Name = "Статус")]
        public StatusIn? Status { get; set; }
    }
}
