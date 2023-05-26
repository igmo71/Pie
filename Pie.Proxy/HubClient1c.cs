using Microsoft.AspNetCore.SignalR.Client;

namespace Pie.Proxy
{
    public class HubClient1c
    {
        private readonly HubConnection _hubConnection;
        private readonly HttpService1c _httpService1c;
        private readonly ILogger<HubClient1c> _logger;

        public HubClient1c(IConfiguration configuration, HttpService1c httpService1c, ILogger<HubClient1c> logger)
        {
            _logger = logger;

            string hubUrl = configuration.GetValue<string>("HubUri") ?? throw new ApplicationException("HubClient - Fail to get configuration");
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .WithAutomaticReconnect()
                .Build();

            StartConnection().GetAwaiter().GetResult();

            _hubConnection.Closed += async (ex) =>
            {
                _logger.LogError(ex, "HubClient - Disconnected");
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
            _logger.LogInformation("HubClient - Trying to connect");
            try
            {
                await _hubConnection.StartAsync();
                _logger.LogInformation("HubClient - Connected");
            }
            catch (Exception ex)
            {
                _logger.LogWarning("HubClient - {Message}", ex.Message);
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
            string request = input[0] as string ?? throw new ApplicationException("HubClient OnPostDocOutDto - Request is Empty");

                string response = await _httpService1c.SendOutAsync(request);

                return response;
        }
    }
}
