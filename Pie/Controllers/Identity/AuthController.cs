using Microsoft.AspNetCore.Mvc;
using Pie.Data.Models.Identity;
using Pie.Data.Services.Identity;

namespace Pie.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppUserService _userService;

        public AuthController(AppUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthUserDto authUserDto)
        {
            AuthUserResult result = await _userService.Login(authUserDto);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(new { token = result.Token });
        }
    }
}
