using Pie.Common;
using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models.Identity
{
    public class AppUserDto
    {
        public string Id { get; set; } = null!;
        [Display(Name = "Имя")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Фамилия")]
        public string? LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Display(Name = "Склад")]
        public string? Warehouse { get; set; }
        public Guid? WarehouseId { get; set; }

        [Display(Name = "Активен")]
        public bool Active { get; set; }

        [Display(Name = "Роли")]
        public List<string>? Roles { get; set; }


        public string? Barcode => BarcodeGenerator.GetBarCode128(Id);

        public static AppUserDto MapFromAppUser(AppUser user)
        {
            AppUserDto userDto = new AppUserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Active = user.LockoutEnd == null
            };

            return userDto;
        }
    }
}
