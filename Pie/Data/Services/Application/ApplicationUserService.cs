using Microsoft.AspNetCore.Identity;
using Pie.Data.Models.Application;
using System.Security.Claims;

namespace Pie.Data.Services.Application
{
    public class ApplicationUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserService(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public string CurrentUserId
        {
            get
            {
                var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                return string.IsNullOrEmpty(userId) ? string.Empty : userId;
            }
        }

        public async Task<ApplicationUser?> GetCurrentUserAsync()
        {
            var user = await _userManager.FindByIdAsync(CurrentUserId);
            return user;
        }

        public async Task<string> GetCurrentUserNameAsync()
        {
            var user = await GetCurrentUserAsync();
            string name = $"{user?.FirstName} {user?.LastName}";
            return name;
        }

        public async Task<ApplicationUser?> GetUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

        public async Task<string> GetUserNameAsync(string id)
        {
            var user = await GetUserAsync(id);
            string name = $"{user?.FirstName} {user?.LastName}";
            return name;
        }
    }
}
