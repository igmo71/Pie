using Pie.Common;

namespace Pie.Data.Models
{
    public class DeliveryArea : ISelfRefEntity
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string? Name { get; set; }
        public bool Active { get; set; }
        public bool IsFolder { get; set; }
    }
}
