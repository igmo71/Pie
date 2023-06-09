using Pie.Connectors.Connector1c.Models1c;
using Pie.Data.Models;
using System.Text.Json;

namespace Pie.Connectors.Connector1c.Services1c
{
    public class WarehouseService1c
    {
        private readonly string SUBJECT = "Catalog_Склады";
        private readonly string SELECT = "Ref_Key,Description,DeletionMark";

        private readonly ODataService1c _oDataService1c;
        private readonly ILogger<WarehouseService1c> _logger;

        public WarehouseService1c(
            ODataService1c oDataService1c,
            ILogger<WarehouseService1c> logger)
        {
            _oDataService1c = oDataService1c;
            _logger = logger;
        }

        public async Task<List<Warehouse>?> GetListAsync()
        {
            string uri = $"{SUBJECT}?$format=json&$select={SELECT}";

            var response = await _oDataService1c.GetAsync(uri);

            RootObject<WarehouseDto1c>? rootObject = JsonSerializer.Deserialize<RootObject<WarehouseDto1c>>(response);

            if (rootObject?.Value == null) return default;

            List<Warehouse> result = WarehouseDto1c.MapToWarehouseList(rootObject.Value);

            return result;
        }
    }
}
