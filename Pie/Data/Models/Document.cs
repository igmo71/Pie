using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models
{
    public abstract class Document
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(20)]
        public string? Number { get; set; }

        public DateTime DateTime { get; set; }

        public bool Active { get; set; }
    }
}
