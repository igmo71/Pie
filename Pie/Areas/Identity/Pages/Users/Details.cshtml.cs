using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Models.Application;
using Pie.Data.Services;
using Pie.Data.Services.Application;

namespace Pie.Areas.Identity.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly AppUserService _userService;
        private readonly WarehouseService _warehouseService;

        public DetailsModel(AppUserService userService, WarehouseService warehouseService)
        {
            _userService = userService;
            _warehouseService = warehouseService;
        }

        public AppUser AppUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
                return NotFound();

            var user = await _userService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.Warehouse = await _warehouseService.GetAsync(user.WarehouseId);
                AppUser = user;
            }
            return Page();
        }
    }
}
