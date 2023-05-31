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

        public Guid? WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }

        [MaxLength(250)]
        public string? Comment { get; set; }

        public Guid? PartnerId { get; set; }
        public Partner? Partner { get; set; }

        public bool IsTransfer { get; set; }

        public Guid? TransferWarehouseId { get; set; }
        public Warehouse? TransferWarehouse { get; set; }
    }
}
