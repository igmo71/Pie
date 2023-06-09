using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pie.Data.Models.Identity;
using Pie.Data.Services;
using Pie.Data.Services.Identity;

namespace Pie.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppUserService _userService;

        public RolesController(AppUserService userService, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            List<IdentityRole> roles = _userService.GetRoles();

            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            ServiceResult result = await _userService.CreateRoleAsync(name);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(name);
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            ServiceResult result = await _userService.DeleteRoleAsync(roleId);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok();
        }

        [HttpGet("UserRoles")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var result = await _userService.GetUserRolesAsync(userId);
            if (!result.IsSuccess)
                return BadRequest(result);

            var userRoles = result.Value;
            return Ok(userRoles);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddRoles(string userId, List<string> roles)
        {
            ServiceResult result = await _userService.AddRolesAsync(userId, roles);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok();
        }
    }
}
