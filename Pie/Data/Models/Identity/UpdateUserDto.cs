using Pie.Data.Services;
using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models.Identity
{
    public class UpdateUserDto
    {
        public string Id { get; set; } = null!;
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Фамилия")]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;


        [DataType(DataType.Password)]
        [Display(Name = "Старый пароль")]
        public string? CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string? NewPassword { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить новый пароль")]
        [Compare("NewPassword", ErrorMessage = "Пароль и подтверждение не совпадают.")]
        public string? ConfirmPassword { get; set; }

        [Display(Name = "Склад")]
        public Guid? WarehouseId { get; set; }

        public static UpdateUserDto MapFromAppUser(AppUser user)
        {
            UpdateUserDto userDto = new UpdateUserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty,
                WarehouseId = user.WarehouseId,
            };

            return userDto;
        }
    }

    public class UpdateUserResult : ServiceResult
    { }
}
