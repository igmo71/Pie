namespace Pie.Data.Models.In
{
    public class DocInVm
    {
        public DocIn? Value { get; set; }
        public string? Barcode { get; set; }
        public string? AtWorkUserName { get; set; }
        public float Weight => Value != null ? Value.Products.Sum(x => x.Weight) : default;
    }
}
