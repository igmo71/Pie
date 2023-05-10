using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models.Out
{
    public class DocOutProductHistory : DocProductHistory
    {
        public DocOutProductHistory()
        { }

        public DocOutProductHistory(DocOutProduct product)
        {
            DocId = product.DocId;
            ProductId = product.ProductId;
            CountPlan = product.CountPlan;
            CountFact = product.CountFact;
        }

        public Guid DocId { get; set; }
        public DocOut? Doc { get; set; }


        [Required(ErrorMessage = "Причину изменения нужно указать")]
        public Guid? ChangeReasonId { get; set; }
        public ChangeReasonOut? ChangeReason { get; set; }
    }
}
