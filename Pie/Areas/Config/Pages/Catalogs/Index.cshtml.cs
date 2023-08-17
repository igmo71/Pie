using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pie.Data.Services;

namespace Pie.Areas.Config.Pages.Catalogs
{
    public class IndexModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly WarehouseService _warehouseService;
        private readonly DeliveryAreaService _deliveryAreaService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            ProductService productService, 
            WarehouseService warehouseService, 
            DeliveryAreaService deliveryAreaService, 
            ILogger<IndexModel> logger)
        {
            _productService = productService;
            _warehouseService = warehouseService;
            _deliveryAreaService = deliveryAreaService;
            _logger = logger;
        }

        public ServiceResult LoadResult { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLoadProducts()
        {
            try
            {
                //LoadResult = await _productService.LoadAsync();
            }
            catch (Exception ex)
            {
                LoadResult.IsSuccess = false;
                LoadResult.Message = ex.Message;
                _logger.LogError(ex, "Config.Pages.Catalogs OnPostLoadProducts {Message}", ex.Message);
            }

            LoadResult.Message = $"Справочник Номенклатуры загружен успешно)";
            return Page();
        }

        public async Task<IActionResult> OnPostLoadWarehouses()
        {
            try
            {
                //LoadResult = await _warehouseService.LoadAsync();
            }
            catch (Exception ex)
            {
                LoadResult.IsSuccess = false;
                LoadResult.Message = ex.Message;
                _logger.LogError(ex, "Config.Pages.Catalogs OnPostLoadWarehouses {Message}", ex.Message);
            }

            LoadResult.Message = $"Справочник Складов загружен успешно)";
            return Page();
        }

        public async Task<IActionResult> OnPostLoadDeliveryAreas()
        {
            try
            {
                //LoadResult = await _deliveryAreaService.LoadAsync();
            }
            catch (Exception ex)
            {
                LoadResult.IsSuccess = false;
                LoadResult.Message = ex.Message;
                _logger.LogError(ex, "Config.Pages.Catalogs OnPostLoadDeliveryAreas {Message}", ex.Message);
            }

            LoadResult.Message = $"Справочник Зон доставки загружен успешно)";
            return Page();
        }
    }
}
