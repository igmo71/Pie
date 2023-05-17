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
        private readonly HttpClient _client1c;
        private readonly Client1c _client1cConfig;
        private readonly ILogger<Service1c> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public Service1c(IHttpClientFactory clientFactory, IOptionsSnapshot<Client1c> namedOptionsAccessor, ILogger<Service1c> logger, IOptions<JsonSerializerOptions> jsonOptions)
        {
            _client1c = clientFactory.CreateClient(nameof(Client1c));
            _client1cConfig = namedOptionsAccessor.Get(nameof(Client1c));
            _logger = logger;
            _jsonOptions = jsonOptions.Value;
        }

        public async Task<Service1cResult> SendInAsync(DocIn doc)
        {
            Service1cResult result = new();

            return result;
        }

        public async Task<ServiceResult> SendOutAsync(DocOutDto docDto)
        {
            Service1cResult result = new();
            _logger.LogDebug("Service1c SendOutAsync {DocOutDto}", JsonSerializer.Serialize(docDto));

            string content = JsonSerializer.Serialize(docDto);
            StringContent stringContent = new(content, Encoding.UTF8, MediaTypeNames.Application.Json);
            string? requestUri = $"{_client1cConfig.HttpService}/{nameof(DocOut)}";
            HttpResponseMessage response = await _client1c.PutAsync(requestUri, stringContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Service1c SendOutAsync - {StatusCode} {ResponseContent}", response.StatusCode, responseContent);
                result.Message = responseContent;
                return result;
            }

            result.IsSuccess = true;
            return result;
        }
    }
}
