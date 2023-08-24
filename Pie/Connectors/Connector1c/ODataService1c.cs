using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace Pie.Connectors.Connector1c
{
    public class ODataService1c
    {
        private readonly HttpClient _httpClient1c;
        private readonly Client1cConfig _client1cConfig;
        private readonly ILogger<ODataService1c> _logger;

        public ODataService1c(
            HttpClient httpClient,
            IOptions<Client1cConfig> client1cOptions,
            ILogger<ODataService1c> logger)
        {
            _client1cConfig = client1cOptions.Value;

            _httpClient1c = httpClient;
            
            _httpClient1c.BaseAddress = new Uri(_client1cConfig.BaseAddress ?? "Client1c BaseAddress not found");
            _httpClient1c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                            "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_client1cConfig.UserName}:{_client1cConfig.Password}")));

            _httpClient1c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            _logger = logger;
        }

        public async Task<string> GetAsync(string uri)
        {
            string? requestUri = $"{_client1cConfig.OData}/{uri}";
            HttpResponseMessage response = await _httpClient1c.GetAsync(requestUri);
            string content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("ODataService GetAsync {StatusCode} {Response}", response.StatusCode, content);
            }

            return content;
        }
    }
}
