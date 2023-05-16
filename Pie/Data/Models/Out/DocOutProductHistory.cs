using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models.Out
{
    public class DocOutProductHistory : DocProductHistory
    {
        public DocOutProductHistory()
        { }

        public DocOutProductHistory(DocOutProduct product) : this()
        {
            DocId = product.DocId;
            ProductId = product.ProductId;
            LineNumber = product.LineNumber;
            CountPlan = product.CountPlan;
            CountFact = product.CountFact;
            ChangeReasonId = product.ChangeReasonId;
        }

        public Guid DocId { get; set; }
        [Display(Name = "Документ")]
        public DocOut? Doc { get; set; }

        public Guid? ChangeReasonId { get; set; }
        [Display(Name = "Причина")]
        public ChangeReasonOut? ChangeReason { get; set; }
    }
}
