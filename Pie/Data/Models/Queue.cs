using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models
{
    public abstract class Queue
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        public bool Active { get; set; }

        [Required]
        public int? Key { get; set; }

        // Отгрузить До:
        [DefaultValue(0)]
        public int Days { get; set; }

        [DefaultValue(0)]
        public int Hours { get; set; }

        [DefaultValue(0)]
        public int Minutes { get; set; }

        public TimeOnly ConcreteTime { get; set; } = new TimeOnly();
    }
}
