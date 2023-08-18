using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models
{
    public abstract class Doc
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        public bool Active { get; set; }

        [MaxLength(20)]
        public string? Number { get; set; }

        public DateTime DateTime { get; set; }

        public Guid? ManagerId { get; set; }
        public Manager? Manager { get; set; }

        public Guid? PartnerId { get; set; }  // TODO: Получателем (партнером) может быть склад в случае перемещения!
        public Partner? Partner { get; set; }

        public Guid? WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }

        public bool IsTransfer { get; set; }

        [MaxLength(250)]
        public string? Comment { get; set; }
    }
}
