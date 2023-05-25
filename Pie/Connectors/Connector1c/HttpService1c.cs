using Microsoft.Extensions.Options;
using Pie.Data.Models.In;
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
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly ILogger<Service1c> _logger;

        public HttpService1c(
            HttpClient httpClient,
            IOptions<Client1cConfig> client1cOptions,
            IOptions<JsonSerializerOptions> jsonSerializerOptions,
            ILogger<Service1c> logger)
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

            _jsonSerializerOptions = jsonSerializerOptions.Value;
            _logger = logger;
        }

        public async Task<ServiceResult> SendInAsync(DocInDto docDto)
        {
            ServiceResult result = new();
            return result;
        }

        public async Task<ServiceResult> SendOutAsync(DocOutDto docDto)
        {
            ServiceResult result = new();
            _logger.LogDebug("HttpService1c SendOutAsync - Start {DocOutDto.Id} {DocOutDto.Name}", docDto.Id, docDto.Name);

            string content = JsonSerializer.Serialize(docDto);
            StringContent stringContent = new(content, Encoding.UTF8, MediaTypeNames.Application.Json);
            //string? requestUri = $"{_client1cConfig.HttpService}/{nameof(DocOut)}";
            string? requestUri = $"{_client1cConfig.HttpService}/test";
            HttpResponseMessage response = await _httpClient1c.PutAsync(requestUri, stringContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("HttpService1c SendOutAsync - {StatusCode} {@RequestMessage} {@DocOutDto} {@ResponseContent}", 
                    response.StatusCode, response.RequestMessage, docDto, responseContent);
                result.Message = $"Ответ 1С - {response.StatusCode}";
                //return result; // TODO: For Testing - Uncomment for Release !!!
            }

            docDto = JsonSerializer.Deserialize<DocOutDto>(responseContent, _jsonSerializerOptions);
            _logger.LogDebug("HttpService1c SendOutAsync - Ok {DocOutDto.Id} {DocOutDto.Name}", docDto.Id, docDto.Name);
            result.IsSuccess = true;
            return result;
        }        
    }
}
