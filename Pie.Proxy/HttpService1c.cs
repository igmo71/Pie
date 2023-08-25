using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Pie.Proxy
{
    public class HttpService1c
    {
        private readonly HttpClient _httpClient1c;
        //private readonly Client1cConfig _client1cConfig;
        //private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly ILogger<HttpService1c> _logger;

        public HttpService1c(
            HttpClient httpClient,
            IOptions<Client1cConfig> client1cOptions,
            //IOptions<JsonSerializerOptions> jsonSerializerOptions,
            ILogger<HttpService1c> logger)
        {
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //_client1cConfig = client1cOptions.Value;

            _httpClient1c = httpClient;
            //_httpClient1c.BaseAddress = new Uri(_client1cConfig.BaseAddress ?? "Client1c BaseAddress not found");
            //_httpClient1c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            //_httpClient1c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            //                "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_client1cConfig.UserName}:{_client1cConfig.Password}")));
            //_httpClient1c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            //_httpClient1c.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
            //_httpClient1c.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("windows-1251"));

            //_jsonSerializerOptions = jsonSerializerOptions.Value;
            _logger = logger;
        }

        public async Task<string> SendInAsync(string client1cConfig, string request)
        {
            Client1cConfig _client1cConfig = JsonSerializer.Deserialize<Client1cConfig>(client1cConfig)
                ?? throw new ApplicationException("Pie.Proxy SendInAsync Can't Deserialize Client1cConfig");

            ConfigureHttpClient(_client1cConfig);

            string? requestUri = $"{_client1cConfig.HttpService}/DocIn"
                ?? throw new ApplicationException("Pie.Proxy SendInAsync Client1cConfig HttpService uri not found");

            StringContent stringContent = new(request, Encoding.UTF8, MediaTypeNames.Application.Json);

            HttpResponseMessage httpResponseMessage = await _httpClient1c.PostAsync(requestUri, stringContent);

            string response = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                _logger.LogError("HttpService1c SendInAsync - {ResponseStatusCode} {@RequestMessage} {@ResponseContent}",
                    httpResponseMessage.StatusCode, httpResponseMessage.RequestMessage, response);
                throw new ApplicationException($"Pie.Proxy SendInAsync Ошибка запроса к 1С ({httpResponseMessage.StatusCode})");
            }

            return response;
        }

        public async Task<string> SendOutAsync(string client1cConfig, string request)
        {
            Client1cConfig _client1cConfig = JsonSerializer.Deserialize<Client1cConfig>(client1cConfig)
                ?? throw new ApplicationException("Pie.Proxy SendOutAsync Can't Deserialize Client1cConfig");

            ConfigureHttpClient(_client1cConfig);

            string requestUri = $"{_client1cConfig.HttpService}/DocOut"
                ?? throw new ApplicationException("Pie.Proxy SendOutAsync Client1cConfig HttpService uri not found");

            StringContent stringContent = new(request, Encoding.UTF8, MediaTypeNames.Application.Json);

            HttpResponseMessage httpResponseMessage = await _httpClient1c.PostAsync(requestUri, stringContent);

            string response = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                _logger.LogError("HttpService1c SendOutAsync - {ResponseStatusCode} {@RequestMessage} {@ResponseContent}",
                    httpResponseMessage.StatusCode, httpResponseMessage.RequestMessage, response);
                throw new ApplicationException($"Pie.Proxy SendOutAsync Ошибка запроса к 1С ({httpResponseMessage.StatusCode})");
            }

            return response;
        }

        private void ConfigureHttpClient(Client1cConfig _client1cConfig)
        {
            if (_httpClient1c.BaseAddress == null)
                _httpClient1c.BaseAddress = new Uri(_client1cConfig.BaseAddress
                    ?? throw new ApplicationException("Pie.Proxy SendInAsync Client1cConfig BaseAddress not found"));

            _httpClient1c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

            _httpClient1c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_client1cConfig.UserName}:{_client1cConfig.Password}")));
        }
    }
}
