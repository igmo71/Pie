using Pie.Connectors.Connector1c.Models1c;
using Pie.Data.Models;
using System.Text.Json;

namespace Pie.Connectors.Connector1c.Services1c
{
    public class DeliveryAreaService1c
    {
        private readonly string SUBJECT = "Catalog_ЗоныДоставки";
        private readonly string SELECT = "Ref_Key,Description,DeletionMark,Parent_Key,IsFolder";

        private readonly ODataService1c _oDataService1c;
        private readonly ILogger<DeliveryAreaService1c> _logger;

        public DeliveryAreaService1c(
            ODataService1c oDataService1c,
            ILogger<DeliveryAreaService1c> logger)
        {
            _oDataService1c = oDataService1c;
            _logger = logger;
        }

        public async Task<List<DeliveryArea>?> GetListAsync()
        {
            string uri = $"{SUBJECT}?$format=json&$select={SELECT}";

            var response = await _oDataService1c.GetAsync(uri);

            RootObject<DeliveryAreaDto1c>? rootObject = JsonSerializer.Deserialize<RootObject<DeliveryAreaDto1c>>(response);

            if (rootObject?.Value == null) return default;

            List<DeliveryArea> result = DeliveryAreaDto1c.MapToDeliveryAreaList(rootObject.Value);

            return result;
        }
    }
}
