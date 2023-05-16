namespace Pie.Data.Models.Out
{
    public class DocOutVm
    {
        public DocOut? Value { get; set; }
        public string? Barcode { get; set; }
        public string? UserName { get; set; }
        public float Weight => Value != null ? Value.Products.Sum(x => x.Weight) : default;
    }
}
