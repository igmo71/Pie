using Microsoft.Extensions.Options;
using Pie.Connectors.Connector1c.Models1c;
using System.Text.Json;

namespace Pie.Connectors.Connector1c.Services1c
{
    public class DeliveryAreaService
    {
        private readonly string RESOURCE_URL = "Catalog_ЗоныДоставки";

        private readonly ODataService _oDataService;
        private readonly ILogger<DeliveryAreaService> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public DeliveryAreaService(
            ODataService oDataService, 
            IOptions<JsonSerializerOptions> jsonSerializerOptions, 
            ILogger<DeliveryAreaService> logger)
        {
            _oDataService = oDataService;
            _logger = logger;
            _jsonSerializerOptions = jsonSerializerOptions.Value;
        }

        public async Task<RootObject<DeliveryAreaDto>?> GetAsync()
        {           
                var response = await _oDataService.GetAsync(RESOURCE_URL);
                
                RootObject<DeliveryAreaDto>? rootObject = JsonSerializer.Deserialize<RootObject<DeliveryAreaDto>>(response);

                return rootObject;
        }
    }
}
