using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Identity;
using Pie.Data.Services.Identity;

namespace Pie.Areas.Identity.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly AppUserService _userService;

        public DetailsModel(AppUserService userService)
        {
            _userService = userService;
        }

        public AppUserDto AppUserDto { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
                return NotFound();

            var user = await _userService.GetUserDtoAsync(id);
            if (user == null)
                return NotFound();
                
                AppUserDto = user;
            return Page();
        }
    }
}
