using Humanizer;
using Microsoft.Extensions.Options;
using Pie.Data.Models.In;
using Pie.Data.Models.Out;
using Pie.Data.Services;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Pie.Connectors.Connector1c
{
    public class Service1c
    {
        private readonly HttpService1c _httpService1c;
        private readonly HubService1c _hubService1c;
        private readonly ILogger<Service1c> _logger;
        private readonly IConfiguration _configuration;

        public Service1c(HttpService1c httpService1c, HubService1c hubService1c, ILogger<Service1c> logger, IConfiguration configuration)
        {
            _httpService1c = httpService1c;
            _hubService1c = hubService1c;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<ServiceResult> SendInAsync(DocIn doc)
        {
            ServiceResult result = new();

            return result;
        }

        public async Task<ServiceResult> SendOutAsync(DocOutDto docDto)
        {
            ServiceResult result = new();
            _logger.LogDebug("Service1c SendOutAsync {DocOutDto}", JsonSerializer.Serialize(docDto));

            var useProxy = _configuration.GetValue<bool>("Connectors:UseProxy");

            if (useProxy)
            {
                result = await _hubService1c.SendOutAsync(docDto);
            }
            else
            {
                result = await _httpService1c.SendOutAsync(docDto);
            }

            return result;
        }
    }
}
