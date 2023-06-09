using Pie.Data.Services;
using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models.Identity
{
    public class AuthUserDto
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }

    public class AuthUserResult : ServiceResult
    {
        public string? Token { get; set; }
    }
}
