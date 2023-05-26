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
        private readonly HttpService1c _httpService1c;
        private readonly ILogger<HubClient> _logger;

        public HubClient(IConfiguration configuration, HttpService1c httpService1c, ILogger<HubClient> logger)
        {
            _logger = logger;

            string hubUrl = configuration.GetValue<string>("HubUri") ?? throw new ApplicationException("Fail to get configuration");
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .WithAutomaticReconnect()
                .Build();

            StartConnection().GetAwaiter().GetResult();

            _hubConnection.Closed += async (ex) =>
            {
                _logger.LogError(ex, "Disconnected");
                await StartConnection();
            };

            _hubConnection.On(
                methodName: "PostDocOutDto",
                parameterTypes: new[] { typeof(string) },
                handler: async (input) =>
                {
                    string? result = await OnPostDocOutDto(input);
                    return result;
                });

            _httpService1c = httpService1c;
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

        private async Task<string?> OnPostDocOutDto(object?[] input)
        {
            string request = input[0] as string ?? throw new ApplicationException("Request is Empty");

                string response = await _httpService1c.SendOutAsync(request);

                return response;
        }
    }
}
