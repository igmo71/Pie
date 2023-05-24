using Microsoft.Extensions.Options;
using Pie.Data.Models.Out;
using Pie.Data.Services;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Pie.Connectors.Connector1c
{
    public class HttpService1c
    {
        private readonly HttpClient _client1c;
        private readonly Client1c _client1cConfig;
        private readonly ILogger<Service1c> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public HttpService1c(IHttpClientFactory clientFactory, IOptionsSnapshot<Client1c> namedOptionsAccessor,
            ILogger<Service1c> logger, IOptions<JsonSerializerOptions> jsonOptions)
        {
            _client1c = clientFactory.CreateClient(nameof(Client1c));
            _client1cConfig = namedOptionsAccessor.Get(nameof(Client1c));
            _logger = logger;
            _jsonOptions = jsonOptions.Value;
        }
        public async Task<ServiceResult> SendOutAsync(DocOutDto docDto)
        {
            ServiceResult result = new();
            _logger.LogDebug("Service1c SendToHttpServiceDirectly {DocOutDto}", JsonSerializer.Serialize(docDto));

            string content = JsonSerializer.Serialize(docDto);
            StringContent stringContent = new(content, Encoding.UTF8, MediaTypeNames.Application.Json);
            string? requestUri = $"{_client1cConfig.HttpService}/{nameof(DocOut)}";
            HttpResponseMessage response = await _client1c.PutAsync(requestUri, stringContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            // TODO: For Testing - Uncomment for Release !!!
            //if (!response.IsSuccessStatusCode)
            //{
            //    _logger.LogError("Service1c SendOutAsync - {StatusCode} {ResponseContent}", response.StatusCode, responseContent);
            //    result.Message = responseContent;
            //    return result;
            //}

            result.IsSuccess = true;
            return result;
        }
    }
}
