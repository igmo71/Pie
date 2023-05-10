using Pie.Data.Models.Application;
using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models
{
    public abstract class DocProductHistory
    {
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        [MaxLength(36)]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        public float CountPlan { get; set; }

        public float CountFact { get; set; }
    }
}
