using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pie.Data.Models;
using Pie.Data.Models.Application;
using Pie.Data.Services;
using Pie.Data.Services.Application;

namespace Pie.Areas.Identity.Pages.Users
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationUserService _userService;
        private readonly WarehouseService _warehouseService;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(ApplicationUserService userService, WarehouseService warehouseService, ILogger<RegisterModel> logger)
        {
            _userService = userService;
            _warehouseService = warehouseService;
            _logger = logger;
        }

        [BindProperty]
        public CreateUserDto CreateUserDto { get; set; } = default!;

        public SelectList WarehouseSL { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            List<Warehouse> warehouses = await _warehouseService.GetListActiveAsync();
            WarehouseSL = new SelectList(warehouses, nameof(Warehouse.Id), nameof(Warehouse.Name));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var result = await _userService.CreateAsync(CreateUserDto);
            if (!result.IsSuccess)
            {
                result.Errors?.ForEach(e => ModelState.AddModelError(string.Empty, e));
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
