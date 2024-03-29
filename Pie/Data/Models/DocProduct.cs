﻿using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models
{
    public abstract class DocProduct
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        public int LineNumber { get; set; }

        public float CountPlan { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "Количество не должно быть отрицательным")]
        public float CountFact { get; set; }

        [MaxLength(20)]
        public string? Unit { get; set; }

        public float Weight { get; set; }
    }
}
