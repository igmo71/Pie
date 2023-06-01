using Pie.Common;

namespace Pie.Data.Models
{
    public class DeliveryAreaTreeNode : ITreeNode
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<ITreeNode>? Children { get; set; }
    }
}
