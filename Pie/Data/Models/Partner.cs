using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models
{
    public class Partner
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        //public bool Active { get; set; }
    }
}
