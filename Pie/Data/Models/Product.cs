using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(100)]
        public string? Code { get; set; }

        public bool Active { get; set; }
    }
}
