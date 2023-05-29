using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pie.Data.Models;
using Pie.Data.Models.Identity;
using Pie.Data.Services;
using Pie.Data.Services.Identity;

namespace Pie.Areas.Identity.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly AppUserService _userService;
        private readonly WarehouseService _warehouseService;

        public IndexModel(AppUserService userService, WarehouseService warehouseService)
        {
            _userService = userService;
            _warehouseService = warehouseService;
        }

        public List<AppUserDto> Users { get; set; } = default!;

        public SelectList WarehouseSL { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public Guid? WarehouseId { get; set; } = Guid.Empty;

        public async Task OnGetAsync()
        {
            List<Warehouse> warehouses = await _warehouseService.GetListActiveAsync();
            WarehouseSL = new SelectList(warehouses, nameof(Warehouse.Id), nameof(Warehouse.Name));

            Users = await _userService.GetUserDtoListAsync();

            if (WarehouseId != Guid.Empty)
                Users = Users.Where(u => u.WarehouseId == WarehouseId).ToList();
        }
    }
}
