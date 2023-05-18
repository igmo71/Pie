using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Identity;
using Pie.Data.Services.Identity;

namespace Pie.Areas.Identity.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly AppUserService _userService;

        public IndexModel(AppUserService userService)
        {
            _userService = userService;
        }

        public List<AppUser> Users { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Users = await _userService.GetUserListAsync();
        }
    }
}
