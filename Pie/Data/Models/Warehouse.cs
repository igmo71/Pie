using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models
{
    public class Warehouse
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Склад")]
        public string? Name { get; set; }

        public bool Active { get; set; }
    }
}
