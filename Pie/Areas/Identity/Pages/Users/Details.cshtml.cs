using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Application;
using Pie.Data.Services.Application;

namespace Pie.Areas.Identity.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationUserService _userService;

        public DetailsModel(ApplicationUserService userService)
        {
            _userService = userService;
        }

        public ApplicationUser? AppUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                AppUser = user;
            }
            return Page();
        }
    }
}
