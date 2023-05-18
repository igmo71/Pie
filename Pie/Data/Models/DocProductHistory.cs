using Pie.Data.Models.Application;
using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models
{
    public abstract class DocProductHistory
    {
        public Guid Id { get; set; }

        [Display(Name = "Дата/время")]
        public DateTime DateTime { get; set; }

        [MaxLength(36)]
        public string? UserId { get; set; }
        [Display(Name = "Пользователь")]
        public AppUser? User { get; set; }

        public Guid ProductId { get; set; }

        [Display(Name = "Товар")]
        public Product? Product { get; set; }

        [Display(Name = "Строка")]
        public int LineNumber { get; set; }


        [Display(Name = "Кол план")]
        public float CountPlan { get; set; }
        [Display(Name = "Кол факт")]
        public float CountFact { get; set; }
    }
}
