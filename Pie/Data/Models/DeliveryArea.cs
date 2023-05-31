namespace Pie.Data.Models
{
    public class DeliveryArea
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool Active { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsFolder { get; set; }
    }
}
