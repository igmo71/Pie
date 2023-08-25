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

            string hubUrl = configuration.GetValue<string>("HubUri") ?? throw new ApplicationException("Pie.Proxy SendOutAsync HubClient - Fail to get configuration");
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
                methodName: "PostDocInDto",
                parameterTypes: new[] { typeof(string), typeof(string) },
                handler: async (input) =>
                {
                    string? result = await OnPostDocInDto(input);
                    return result;
                });

            _hubConnection.On(
                methodName: "PostDocOutDto",
                parameterTypes: new[] { typeof(string), typeof(string) },
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
                await Task.Delay(5000);
                await StartConnection();
            }
        }

        public string State => _hubConnection.State.ToString();

        public async Task SendMessageAsync(string message)
        {
            _logger.LogInformation("SendMessageAsync Begin {message}", message);
            if (_hubConnection.State == HubConnectionState.Connected)
            {
                try
                {
                    _logger.LogInformation("SendMessageAsync _hubConnection.SendAsync Begin {message}", message);
                    await _hubConnection.SendAsync("GetMessage", message);
                    //await _hubConnection.InvokeAsync("GetMessage", message);
                    _logger.LogInformation("SendMessageAsync _hubConnection.SendAsync End {message}", message);
                }
                catch (Exception ex)
                {
                    _logger.LogError("SendMessageAsync Exception {ex}", ex);
                }
            }
            _logger.LogInformation("SendMessageAsync End {message}", message);
        }

        public async Task SendMessageWithResponseAsync(string message)
        {
            if (_hubConnection.State == HubConnectionState.Connected)
            {
                var result = await _hubConnection.InvokeAsync<string>("GetMessageWithResponse", message);
                _logger.LogInformation("SendMessageWithResponseAsync {result}", result);
            }
        }

        private async Task<string?> OnPostDocInDto(object?[] input)
        {
            string client1cConfig = input[0] as string ?? throw new ApplicationException("Pie.Proxy HubClient OnPostDocOutDto - Client1cConfig is Empty");
            string request = input[1] as string ?? throw new ApplicationException("Pie.Proxy HubClient OnPostDocInDto - Request is Empty");

            string response = await _httpService1c.SendInAsync(client1cConfig, request);

            return response;
        }

        private async Task<string?> OnPostDocOutDto(object?[] input)
        {
            _logger.LogInformation("OnPostDocOutDto {input}", input);
            string client1cConfig = input[0] as string ?? throw new ApplicationException("Pie.Proxy HubClient OnPostDocOutDto - Client1cConfig is Empty");
            _logger.LogInformation("OnPostDocOutDto {client1cConfig}", client1cConfig);
            string request = input[1] as string ?? throw new ApplicationException("Pie.Proxy HubClient OnPostDocOutDto - Request is Empty");
            _logger.LogInformation("OnPostDocOutDto {request}", request);

            string response = await _httpService1c.SendOutAsync(client1cConfig, request);

            return response;
        }
    }
}
