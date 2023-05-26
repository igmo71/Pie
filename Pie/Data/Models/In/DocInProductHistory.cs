using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models.In
{
    public class DocInProductHistory : DocProductHistory
    {
        public DocInProductHistory()
        { }

        public DocInProductHistory(DocInProduct product) : this()
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
        public DocIn? Doc { get; set; }

        public Guid? ChangeReasonId { get; set; }

        [Display(Name = "Причина")]
        public ChangeReasonIn? ChangeReason { get; set; }
    }
}
