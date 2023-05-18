using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pie.Data.Models.Application;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pie.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] ApiUserLogin apiUserLogin)
        {
            var user = await _userManager.FindByNameAsync(apiUserLogin.UserName);
            if (user == null)
                return NotFound(apiUserLogin.UserName);

            var result = await _signInManager.PasswordSignInAsync(user, apiUserLogin.Password, false, false);
            if (!result.Succeeded)
                return Problem();

            var claims = new List<Claim> {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName ?? string.Empty)
                };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            }

            var jwt = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(double.Parse(_configuration["JWT:ExpiryInMinutes"] 
                    ?? throw new ApplicationException("Expiry In Minutes not found.")))),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:IssuerSigningKey"] 
                    ?? throw new ApplicationException("Issuer Signing Key not found."))), SecurityAlgorithms.HmacSha256),
                claims: claims
                );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(jwt) });
        }

        public class ApiUserLogin
        {
            [Required]
            public string UserName { get; set; } = null!;

            [Required]
            public string Password { get; set; } = null!;
        }
    }
}
