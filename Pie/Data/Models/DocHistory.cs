﻿using Pie.Data.Models.Application;

namespace Pie.Data.Models
{
    public abstract class DocHistory
    {
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
