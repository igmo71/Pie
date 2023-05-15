using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pie.Data.Models;
using Pie.Data.Models.Application;
using Pie.Data.Services;
using Pie.Data.Services.Application;

namespace Pie.Areas.Identity.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly ApplicationUserService _userService;
        private readonly WarehouseService _warehouseService;

        public EditModel(ApplicationUserService userService, WarehouseService warehouseService)
        {
            _userService = userService;
            _warehouseService = warehouseService;
        }

        [BindProperty]
        public UpdateUserDto UpdateUserDto { get; set; } = default!;

        public SelectList WarehouseSL { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
                return NotFound();

            List<Warehouse> warehouses = await _warehouseService.GetListActiveAsync();
            WarehouseSL = new SelectList(warehouses, nameof(Warehouse.Id), nameof(Warehouse.Name));
            var user = await _userService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                UpdateUserDto = UpdateUserDto.MapFromApplicationUser(user);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            ServiceResult result = await _userService.UpdateAsync(UpdateUserDto);

            if (!result.IsSuccess && result.Errors != null && result.Errors.Count != 0)
            {
                result.Errors?.ForEach(e => ModelState.AddModelError(string.Empty, e));
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
