using Microsoft.AspNetCore.Mvc;
using Pie.Data.Models.Identity;
using Pie.Data.Services.Identity;

namespace Pie.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppUserService _userService;

        public UsersController(AppUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersDtoAsync()
        {
            List<AppUserDto> users = await _userService.GetUserDtoListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDtoAsync(string id)
        {
            AppUserDto? userDto = await _userService.GetUserDtoAsync(id);
            if(userDto == null)
                return NotFound();

            return Ok(userDto);
        }
    }
}
