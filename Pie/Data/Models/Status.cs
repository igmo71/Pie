﻿using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models
{
    public abstract class Status
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }
        public int? Key { get; set; }
        public bool Active { get; set; }
    }
}
