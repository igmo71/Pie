using Pie.Data.Models.Application;
using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models
{
    public abstract class DocHistory
    {
        public Guid Id { get; set; }

        [Display(Name = "Дата/время")]
        public DateTime DateTime { get; set; }

        [MaxLength(36)]
        public string? UserId { get; set; }
        [Display(Name = "Пользователь")]
        public ApplicationUser? User { get; set; }
    }
}
