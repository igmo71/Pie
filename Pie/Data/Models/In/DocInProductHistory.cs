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
            CountPlan = product.CountPlan;
            CountFact = product.CountFact;
            ChangeReasonId = product.ChangeReasonId;
        }

        public Guid DocId { get; set; }
        public DocIn? Doc { get; set; }

        public Guid? ChangeReasonId { get; set; }
        public ChangeReasonIn? ChangeReason { get; set; }
    }
}
