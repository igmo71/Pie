namespace Pie.Data.Models
{
    public class WhsOrderOut : Document
    {

        public List<Product>? Products { get; set; }

        public List<BaseDocument>? BaseDocuments { get; set; }
    }
}
