namespace Pie.Data.Models.Out
{
    public class DocOutVm
    {
        public DocOut? Value { get; set; }
        public string? Barcode { get; set; }
        public string? AtWorkUserName { get; set; }
        public float Weight => Value != null ? Value.Products.Sum(x => x.Weight) : default;
    }
}
