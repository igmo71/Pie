using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pie.Data.Models.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pie.Data.Services.Identity
{
    public class AppUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<AppUser> _userStore;
        private readonly IUserEmailStore<AppUser> _emailStore;
        private readonly ILogger<AppUserService> _logger;
        private readonly IConfiguration _configuration;

        public AppUserService(IHttpContextAccessor contextAccessor, ILogger<AppUserService> logger,
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager,
                IUserStore<AppUser> userStore, IConfiguration configuration)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _configuration = configuration;
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

        public async Task<AppUserDto?> GetUserDtoAsync(string id)
        {
            var user = await GetUserAsync(id);
            if (user == null) return default;

            AppUserDto userDto = AppUserDto.MapFromAppUser(user);
            return userDto;
        }

        public async Task<string> GetUserNameAsync(string id)
        {
            var user = await GetUserAsync(id);
            string name = $"{user?.FirstName} {user?.LastName}";
            return name;
        }

        public async Task<List<AppUser>> GetUserListAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }

        public async Task<List<AppUserDto>> GetUserDtoListAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = users.Select(u => AppUserDto.MapFromAppUser(u)).ToList();

            return userDtos;
        }

        public async Task<ServiceResult> CreateAsync(CreateUserDto createUserDto)
        {
            ServiceResult result = new();

            AppUser user = CreateUserInstance();

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

        private AppUser CreateUserInstance()
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

        public async Task<AuthUserResult> Login(AuthUserDto authUserDto)
        {
            AuthUserResult result = new() { Errors = new() };

            var user = await _userManager.FindByNameAsync(authUserDto.UserName);
            if (user == null)
            {
                result.Errors.Add("User not found");
                return result;
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, authUserDto.Password, false, false);
            if (!signInResult.Succeeded)
            {
                result.Errors.Add("SignIn not succeeded");
                return result;
            }

            result.Token = await GetTokenAsync(user);
            result.IsSuccess = true;

            return result;
        }


        private async Task<string> GetTokenAsync(AppUser user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName ?? string.Empty)
                };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            }

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(double.Parse(_configuration["JWT:ExpiryInMinutes"]
                    ?? throw new InvalidOperationException("JWT:ExpiryInMinutes not found.")))),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:IssuerSigningKey"]
                    ?? throw new InvalidOperationException("JWT:IssuerSigningKey not found."))), SecurityAlgorithms.HmacSha256),
                claims: claims
                );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }

        public async Task<ServiceResult> CreateRoleAsync(string name)
        {
            ServiceResult result = new() { Errors = new List<string>()};

            if (string.IsNullOrEmpty(name))
            {
                result.Errors.Add("Roles name is empty");
                return result;
            }

            IdentityResult identityResult = await _roleManager.CreateAsync(new IdentityRole(name));

            if (!identityResult.Succeeded)
            {
                result.Errors.Add("Create role error");
                return result;
            }

            result.IsSuccess = true;
            return result;
        }

        public async Task<ServiceResult> DeleteRoleAsync(string roleId)
        {
            ServiceResult result = new() { Errors = new List<string>() };

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                result.Errors.Add("Role not found");
                return result;
            }

            IdentityResult identityResult = await _roleManager.DeleteAsync(role);
            if (!identityResult.Succeeded)
            {
                result.Errors.Add("Delete role error");
                return result;
            }

            result.IsSuccess= true;
            return result;
        }

        public List<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles = _roleManager.Roles.ToList();

            return roles;
        }

        public async Task<ServiceResult<IList<string>>> GetUserRolesAsync(string userId)
        {
            ServiceResult<IList<string>> result = new() { Errors = new List<string>() };

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                result.Errors.Add("User not found");
                return result;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            result.Value = userRoles;
            result.IsSuccess = true;
            return result;
        }

        public async Task<ServiceResult> AddRolesAsync(string userId, List<string> roles)
        {
            ServiceResult result = new() { Errors = new List<string>() };

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                if (user == null)
                {
                    result.Errors.Add("User not found");
                    return result;
                }

            // все роли
            var allRoles = _roleManager.Roles.ToList();
            // список ролей пользователя
            var userRoles = await _userManager.GetRolesAsync(user);
            // список ролей, которые были добавлены
            var addedRoles = roles.Except(userRoles);
            // роли, которые были удалены
            var removedRoles = userRoles.Except(roles);

            await _userManager.AddToRolesAsync(user, addedRoles);

            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            result.IsSuccess = true;
            return result;
        }
    }
}
