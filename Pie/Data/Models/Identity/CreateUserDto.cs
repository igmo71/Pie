using Pie.Data.Services;
using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models.Identity
{
    public class CreateUserDto
    {
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Фамилия")]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; } = null!;

        [Display(Name = "Склад")]
        public Guid? WarehouseId { get; set; }
    }

    public class CreateUserResult : ServiceResult
    { }
}
