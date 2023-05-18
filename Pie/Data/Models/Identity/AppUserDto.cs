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

        public static AppUserDto MapFromAppUser(AppUser user)
        {
            AppUserDto userDto = new AppUserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty
            };

            return userDto;
        }
    }
}
