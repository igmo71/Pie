﻿namespace Pie.Data.Models.Out
{
    public class DocOutProductHistory : DocProductHistory
    {
        public DocOutProductHistory()
        { }

        public DocOutProductHistory(DocOutProduct product) : this()
        {
            DocId = product.DocId;
            ProductId = product.ProductId;
            CountPlan = product.CountPlan;
            CountFact = product.CountFact;
            ChangeReasonId = product.ChangeReasonId;
        }

        public Guid DocId { get; set; }
        public DocOut? Doc { get; set; }

        public Guid? ChangeReasonId { get; set; }
        public ChangeReasonOut? ChangeReason { get; set; }
    }
}
