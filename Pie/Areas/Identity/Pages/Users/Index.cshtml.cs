using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Application;
using Pie.Data.Services.Application;

namespace Pie.Areas.Identity.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationUserService _userService;

        public IndexModel(ApplicationUserService userService)
        {
            _userService = userService;
        }

        public List<ApplicationUser> Users { get; set; } = default!;

        public void OnGet()
        {
            Users = _userService.GetUserList();
        }
    }
}
