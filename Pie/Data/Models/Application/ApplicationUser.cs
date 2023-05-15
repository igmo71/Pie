using Microsoft.AspNetCore.Identity;
using Pie.Common;
using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models.Application
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        [Display(Name = "Имя")]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Фамилия")]
        public string? LastName { get; set; }

        public string? Barcode => BarcodeGuidConvert.GetBarcodeBase64(Id);
    }
}
