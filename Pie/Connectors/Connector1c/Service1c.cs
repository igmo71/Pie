using Pie.Data.Models.In;
using Pie.Data.Models.Out;
using Pie.Data.Services;

namespace Pie.Connectors.Connector1c
{
    public class Service1c
    {
        private readonly HttpService1c _httpService1c;
        private readonly HubService1c _hubService1c;
        private readonly IConfiguration _configuration;
        private readonly ILogger<Service1c> _logger;

        public Service1c(
            HttpService1c httpService1c, 
            HubService1c hubService1c, 
            IConfiguration configuration, 
            ILogger<Service1c> logger)
        {
            _httpService1c = httpService1c;
            _hubService1c = hubService1c;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ServiceResult> SendInAsync(DocIn doc)
        {
            ServiceResult result = new();

            return result;
        }

        public async Task<ServiceResult> SendOutAsync(DocOutDto docDto)
        {
            ServiceResult result;

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
