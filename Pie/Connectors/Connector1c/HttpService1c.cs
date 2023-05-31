using Microsoft.Extensions.Options;
using Pie.Data.Models.In;
using Pie.Data.Models.Out;
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
        //private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly ILogger<HttpService1c> _logger;

        public HttpService1c(
            HttpClient httpClient,
            IOptions<Client1cConfig> client1cOptions,
            IOptions<JsonSerializerOptions> jsonSerializerOptions,
            ILogger<HttpService1c> logger)
        {
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _client1cConfig = client1cOptions.Value;

            _httpClient1c = httpClient;
            _httpClient1c.BaseAddress = new Uri(_client1cConfig.BaseAddress ?? "Client1c BaseAddress not found");
            _httpClient1c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            //_httpClient1c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            //_httpClient1c.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
            //_httpClient1c.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("windows-1251"));
            _httpClient1c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                            "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_client1cConfig.UserName}:{_client1cConfig.Password}")));

            //_jsonSerializerOptions = jsonSerializerOptions.Value;
            _logger = logger;
        }

        public async Task<string> SendInAsync(string request)
        {
            StringContent stringContent = new(request, Encoding.UTF8, MediaTypeNames.Application.Json);

            string? requestUri = $"{_client1cConfig.HttpService}/{nameof(DocIn)}";

            HttpResponseMessage httpResponseMessage = await _httpClient1c.PostAsync(requestUri, stringContent);

            string response = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                _logger.LogError("HttpService1c SendInAsync - {ResponseStatusCode} {@RequestMessage} {@ResponseContent}",
                    httpResponseMessage.StatusCode, httpResponseMessage.RequestMessage, response);
                throw new ApplicationException($"Ошибка запроса к 1С ({httpResponseMessage.StatusCode})");
            }

            return response;
        }

        public async Task<string> SendOutAsync(string request)
        {
            StringContent stringContent = new(request, Encoding.UTF8, MediaTypeNames.Application.Json);

            string? requestUri = $"{_client1cConfig.HttpService}/{nameof(DocOut)}";

            HttpResponseMessage httpResponseMessage = await _httpClient1c.PostAsync(requestUri, stringContent);

            string response = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                _logger.LogError("HttpService1c SendOutAsync - {ResponseStatusCode} {@RequestMessage} {@ResponseContent}",
                    httpResponseMessage.StatusCode, httpResponseMessage.RequestMessage, response);
                throw new ApplicationException($"Ошибка запроса к 1С ({httpResponseMessage.StatusCode})");
            }

            return response;
        }
    }
}
