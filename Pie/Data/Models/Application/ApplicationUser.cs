using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models.Application
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }
    }
}
