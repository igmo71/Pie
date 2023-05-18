using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Application;
using Pie.Data.Services.Application;

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

        public void OnGet()
        {
            Users = _userService.GetUserList();
        }
    }
}
