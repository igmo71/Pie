using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace Pie.Proxy
{
    public class HubClient
    {
        private readonly HubConnection _hubConnection;
        private readonly Client1cConfig _client1cConfig;
        private readonly HttpClient _httpClient1c;
        private readonly ILogger<HubClient> _logger;


        public HubClient(IConfiguration configuration, IOptions<Client1cConfig> client1cOptions, HttpClient httpClient, ILogger<HubClient> logger)
        {
            _logger = logger;

            string hubUrl = configuration.GetValue<string>("HubUri") ?? throw new ApplicationException("Fail to get configuration");
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .WithAutomaticReconnect()
                .Build();

            StartConnection().GetAwaiter().GetResult();

            _client1cConfig = client1cOptions.Value;

            _httpClient1c = httpClient;
            _httpClient1c.BaseAddress = new Uri(_client1cConfig.BaseAddress ?? "Client1c BaseAddress not found");
            _httpClient1c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            //_httpClient1c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            //_httpClient1c.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
            //_httpClient1c.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("windows-1251"));
            _httpClient1c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                            "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_client1cConfig.UserName}:{_client1cConfig.Password}")));
        }

        private async Task StartConnection()
        {
            _logger.LogInformation("Trying to connect");
            try
            {
                await _hubConnection.StartAsync();
                _logger.LogInformation("Connected");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                await Task.Delay(10000);
                await StartConnection();
            }
        }

        public string State => _hubConnection.State.ToString();

        public async Task SendMessageAsync(string message)
        {
            var result = await _hubConnection.InvokeAsync<string>("GetMessage", message);
            _logger.LogInformation(result);
        }
    }
}
