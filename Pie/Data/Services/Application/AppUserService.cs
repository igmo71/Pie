using Microsoft.AspNetCore.Identity;
using Pie.Data.Models.Application;
using System.Security.Claims;

namespace Pie.Data.Services.Application
{
    public class AppUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserStore<AppUser> _userStore;
        private readonly IUserEmailStore<AppUser> _emailStore;
        private readonly ILogger<AppUserService> _logger;

        public AppUserService(IHttpContextAccessor contextAccessor, ILogger<AppUserService> logger,
            UserManager<AppUser> userManager, IUserStore<AppUser> userStore)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _logger = logger;
        }

        public string CurrentUserId
        {
            get
            {
                var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                return string.IsNullOrEmpty(userId) ? string.Empty : userId;
            }
        }

        public async Task<AppUser?> GetCurrentUserAsync()
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

        public async Task<AppUser?> GetUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

        public async Task<AppUser?> GetUserDtoAsync(string id)
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

        public List<AppUser> GetUserList()
        {
            var result = _userManager.Users.ToList();
            return result;
        }

        public async Task<ServiceResult> CreateAsync(CreateUserDto createUserDto)
        {
            ServiceResult result = new();

            AppUser user = CreateUser();

            user.FirstName = createUserDto.FirstName;
            user.LastName = createUserDto.LastName;
            user.WarehouseId = createUserDto.WarehouseId;

            await _userStore.SetUserNameAsync(user, createUserDto.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, createUserDto.Email, CancellationToken.None);
            await _emailStore.SetEmailConfirmedAsync(user, true, CancellationToken.None);

            IdentityResult identityResult = await _userManager.CreateAsync(user, createUserDto.Password);

            if (identityResult.Succeeded)
            {
                result.IsSuccess = true;
                _logger.LogInformation("User created");
            }
            else
            {
                result.Errors = identityResult.Errors.Select(e => e.Description).ToList();
                _logger.LogInformation("User create error: {errors}", result.Errors);
            }

            return result;
        }

        public async Task<ServiceResult> UpdateAsync(UpdateUserDto updateUserDto)
        {
            ServiceResult result = new();

            AppUser? user = await _userManager.FindByIdAsync(updateUserDto.Id);
            if (user == null) return result;

            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.WarehouseId = updateUserDto.WarehouseId;

            IdentityResult identityResult = await _userManager.UpdateAsync(user);

            if (identityResult.Succeeded)
            {
                result.IsSuccess = true;
                _logger.LogInformation("User updated");
            }
            else
            {
                result.Errors = identityResult.Errors.Select(e => e.Description).ToList();
                _logger.LogInformation("User update error: {errors}", result.Errors);
            }

            return result;
        }

        private AppUser CreateUser()
        {
            try
            {
                var user = Activator.CreateInstance<AppUser>();
                return user;
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. ");
            }
        }

        private IUserEmailStore<AppUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<AppUser>)_userStore;
        }
    }
}
