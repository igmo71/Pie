using Microsoft.Extensions.Options;
using Pie.Connectors.Connector1c.Models1c;
using System.Text.Json;

namespace Pie.Connectors.Connector1c.Services1c
{
    public class DeliveryAreaService1c
    {
        private readonly string RESOURCE_URL = "Catalog_ЗоныДоставки";

        private readonly ODataService1c _oDataService1c;
        private readonly ILogger<DeliveryAreaService1c> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public DeliveryAreaService1c(
            ODataService1c oDataService1c, 
            IOptions<JsonSerializerOptions> jsonSerializerOptions, 
            ILogger<DeliveryAreaService1c> logger)
        {
            _oDataService1c = oDataService1c;
            _logger = logger;
            _jsonSerializerOptions = jsonSerializerOptions.Value;
        }

        public async Task<List<DeliveryAreaDto>?> GetAsync()
        {           
                var response = await _oDataService1c.GetAsync(RESOURCE_URL);
                
                RootObject<DeliveryAreaDto>? rootObject = JsonSerializer.Deserialize<RootObject<DeliveryAreaDto>>(response);

                return rootObject?.Value;
        }
    }
}
