using Microsoft.Extensions.Options;
using Pie.Data.Models.In;
using Pie.Data.Models.Out;
using Pie.Data.Services;
using System.Net.Http;
using System.Text.Json;

namespace Pie.Connectors.Connector1c
{
    public class Service1c
    {
        private readonly HttpService1c _httpService1c;
        private readonly HubService1c _hubService1c;
        private readonly IConfiguration _configuration;
        private readonly ILogger<Service1c> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        private readonly IHttpClientFactory _httpClientFactory;

        public Service1c(
            HttpService1c httpService1c, 
            HubService1c hubService1c, 
            IConfiguration configuration, 
            ILogger<Service1c> logger,
            IOptions<JsonSerializerOptions> jsonSerializerOptions
            ,
            IHttpClientFactory httpClientFactory)
        {
            _httpService1c = httpService1c;
            _hubService1c = hubService1c;
            _configuration = configuration;
            _logger = logger;
            _jsonSerializerOptions = jsonSerializerOptions.Value;

            _httpClientFactory = httpClientFactory;
        }

        public async Task<DocInDto> SendInAsync(DocInDto docDto)
        {
            _logger.LogDebug("Service1c SendInAsync - Start {DocInDto.Id} {DocInDto.Name}", docDto.Id, docDto.Name);

            var httpClient = _httpClientFactory.CreateClient();

            string request = JsonSerializer.Serialize(docDto);
            string response;

            var useProxy = _configuration.GetValue<bool>("Connectors:UseProxy");
            if (useProxy)
            {
                response = await _hubService1c.SendInAsync(request);
            }
            else
            {
                response = await _httpService1c.SendInAsync(request);
            }

            DocInDto? result = JsonSerializer.Deserialize<DocInDto?>(response, _jsonSerializerOptions);
            if (result == null)
            {
                _logger.LogError("HttpService1c SendInAsync - 1C returned empty result {@DocInDto}", docDto);
                throw new ApplicationException($"1С вернула пустой результат");
            }
            _logger.LogDebug("HttpService1c SendInAsync - Ok {DocInDto.Id} {DocInDto.Name}", docDto.Id, docDto.Name);
            return result;
        }

        public async Task<DocOutDto> SendOutAsync(DocOutDto docDto)
        {
            _logger.LogDebug("Service1c SendOutAsync - Start {DocOutDto.Id} {DocOutDto.Name}", docDto.Id, docDto.Name);

            var httpClient = _httpClientFactory.CreateClient();

            string request = JsonSerializer.Serialize(docDto);
            string response;

            var useProxy = _configuration.GetValue<bool>("Connectors:UseProxy");
            if (useProxy)
            {
                response = await _hubService1c.SendOutAsync(request);
            }
            else
            {
                response = await _httpService1c.SendOutAsync(request);
            }

            DocOutDto? result = JsonSerializer.Deserialize<DocOutDto?>(response, _jsonSerializerOptions);
            if (result == null)
            {
                _logger.LogError("HttpService1c SendOutAsync - 1C returned empty result {@DocOutDto}", docDto);
                throw new ApplicationException($"1С вернула пустой результат");
            }
            _logger.LogDebug("HttpService1c SendOutAsync - Ok {DocOutDto.Id} {DocOutDto.Name}", docDto.Id, docDto.Name);
            return result;
        }
    }
}
