using Microsoft.AspNetCore.Mvc;
using Pie.Data.Models.Identity;
using Pie.Data.Services;
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
            if (userDto == null)
                return NotFound();

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            ServiceResult result = await _userService.CreateAsync(createUserDto);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(createUserDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
        {
            ServiceResult result = await _userService.UpdateAsync(updateUserDto);
            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            ServiceResult result = await _userService.DeleteAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result);

            return NoContent();
        }
    }
}
