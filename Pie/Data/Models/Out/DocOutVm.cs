namespace Pie.Data.Models.Out
{
    public class DocOutVm
    {
        public DocOut? Item { get; set; }
        public string? Barcode { get; set; }
        public string? UserName { get; set; }
        public float Weight => Item != null ? Item.Products.Sum(x => x.Weight) : default;
    }
}
