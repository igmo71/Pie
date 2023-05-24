using Microsoft.Extensions.Options;
using Pie.Data.Models.Out;
using Pie.Data.Services;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Pie.Connectors.Connector1c
{
    public class HttpService1c
    {
        private readonly HttpClient _httpClient1c;
        private readonly Client1cConfig _client1cConfig;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly ILogger<Service1c> _logger;

        public HttpService1c(
            HttpClient httpClient, 
            IOptions<Client1cConfig> client1cOptions,
            IOptions<JsonSerializerOptions> jsonOptions,
            ILogger<Service1c> logger)
        {

            _client1cConfig = client1cOptions.Value;

            _httpClient1c = httpClient;
            _httpClient1c.BaseAddress = new Uri(_client1cConfig.BaseAddress ?? "Client1c BaseAddress not found");
            _httpClient1c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            _httpClient1c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                            "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_client1cConfig.UserName}:{_client1cConfig.Password}")));

            _jsonOptions = jsonOptions.Value;
            _logger = logger;
        }

        public async Task<ServiceResult> SendOutAsync(DocOutDto docDto)
        {
            ServiceResult result = new();
            _logger.LogDebug("Service1c SendToHttpServiceDirectly {DocOutDto}", JsonSerializer.Serialize(docDto));

            string content = JsonSerializer.Serialize(docDto);
            StringContent stringContent = new(content, Encoding.UTF8, MediaTypeNames.Application.Json);
            string? requestUri = $"{_client1cConfig.HttpService}/{nameof(DocOut)}";
            HttpResponseMessage response = await _httpClient1c.PutAsync(requestUri, stringContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Service1c SendOutAsync - {StatusCode} {ResponseContent}", response.StatusCode, responseContent);
                result.Message = responseContent;
                //return result; // TODO: For Testing - Uncomment for Release !!!
            }

            result.IsSuccess = true;
            return result;
        }
    }
}
