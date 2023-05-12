using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models
{
    public abstract class Queue
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name ="Наименование")]
        public string? Name { get; set; }

        [Display(Name = "Активен")]
        public bool Active { get; set; }

        [Required]
        [Display(Name = "Ключ")]
        public int? Key { get; set; }        
    }
}
