using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models
{
    public abstract class Status
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        public bool Active { get; set; }

        [Required]
        public int? Key { get; set; }

        public bool CanChange { get; set; }
    }
}
