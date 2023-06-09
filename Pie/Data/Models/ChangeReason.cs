using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models
{
    public class ChangeReason
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Наименование")]
        public string? Name { get; set; }

        [Display(Name = "Активен")]
        public bool Active { get; set; }
    }
}
