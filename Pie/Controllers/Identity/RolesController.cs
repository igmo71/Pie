using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pie.Data.Models.Identity;

namespace Pie.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetRoles() => Ok(_roleManager.Roles.ToList());
        
        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
            if (!result.Succeeded)
                return Problem();

            return Ok(name);
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return BadRequest();

            IdentityResult result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
                return Problem();

            return Ok();
        }

        [HttpGet("UserRoles")]
        public async Task<IActionResult> UserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            return Ok(userRoles);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddRoles(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(userId);

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

            return Ok();
        }
    }
}
