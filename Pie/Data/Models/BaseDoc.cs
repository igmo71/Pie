﻿using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models
{
    public class BaseDoc
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }
    }
}