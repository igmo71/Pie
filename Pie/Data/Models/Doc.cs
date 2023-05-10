﻿using Pie.Common;
using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models
{
    public abstract class Doc
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        public bool Active { get; set; }

        [MaxLength(20)]
        public string? Number { get; set; }

        public DateTime DateTime { get; set; }

        public Guid? WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }

        public string? BarcodeBase64 => BarcodeGuidConvert.GetBarcodeBase64(Id);
    }
}
