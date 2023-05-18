using Microsoft.AspNetCore.Identity;
using Pie.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pie.Data.Models.Identity
{
    public class AppUser : IdentityUser
    {
        [MaxLength(100)]
        [Display(Name = "Имя")]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Фамилия")]
        public string? LastName { get; set; }

        [Display(Name = "Склад")]
        public Guid? WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }

        [NotMapped]
        public string? Barcode => BarcodeGenerator.GetBarCode128(Id);

        [NotMapped]
        public string FullName => $"{FirstName ?? string.Empty} {LastName ?? string.Empty}";
    }
}
