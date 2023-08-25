using Microsoft.AspNetCore.SignalR;
using Pie.Data;
using System.Text.Json;

namespace Pie.Connectors.Connector1c
{
    public class HubService1c : IDisposable
    {
        private readonly IHubContext<Hub1c> _hubContext;
        private readonly Client1cConfig _client1cConfig;
        private readonly ILogger<HubService1c> _logger;

        public HubService1c(
            IHubContext<Hub1c> hubContext,
            ApplicationDbContext dbContext,
            ILogger<HubService1c> logger)
        {
            _hubContext = hubContext;

            _client1cConfig = dbContext.Client1CConfig.FirstOrDefault() ?? throw new ApplicationException("HubService1c ctor Client1CConfig not found");

            _logger = logger;

            Hub1c.Connected += ConnectedHandle;
            Hub1c.Disconnected += DisconnectedHandle;
            Hub1c.MessageReceived += MessageReceivedHandle;
        }

        private void ConnectedHandle(object? sender, string? message)
        {
            _logger.LogDebug("HubService1c ConnectedHandle {message}", message);
        }

        private void DisconnectedHandle(object? sender, string? message)
        {
            _logger.LogError("HubService1c  DisconnectedHandle {message}", message);
        }

        private void MessageReceivedHandle(object? sender, string? message)
        {
            _logger.LogInformation("HubService1c MessageReceivedHandle {message}", message);
        }

        public async Task<string> SendInAsync(string request)
        {
            string client1cConfig = JsonSerializer.Serialize(_client1cConfig);

            CancellationTokenSource cts = new(TimeSpan.FromSeconds(10));

            Task<string> sendingIn = _hubContext.Clients.Client(Hub1c.ConnectionId)
                .InvokeAsync<string>(method: "PostDocInDto", arg1: client1cConfig, arg2: request, CancellationToken.None);

            try
            {
                string response = await sendingIn;
                return response;
            }
            catch (Exception)
            {
                if (cts.IsCancellationRequested)
                    throw new ApplicationException("HubService1c SendInAsync. Request to proxy canceled. The client is lost.");
                else throw;
            }
        }

        public async Task<string> SendOutAsync(string request)
        {
            string client1cConfig = JsonSerializer.Serialize(_client1cConfig);

            CancellationTokenSource cts = new(TimeSpan.FromSeconds(10));

            // TODO: Переделать на отправку всем (только для всех InvokeAsync не работает)
            Task<string> sendingOut = _hubContext.Clients.Client(Hub1c.ConnectionId)
                .InvokeAsync<string>(method: "PostDocOutDto", arg1: client1cConfig, arg2: request, cts.Token);

            try
            {
                string response = await sendingOut;
                return response;
            }
            catch (Exception)
            {
                if (cts.IsCancellationRequested)
                    throw new ApplicationException("HubService1c SendOutAsync. Request to proxy canceled. The client is lost.");
                else throw;
            }
        }

        public void Dispose()
        {
            Hub1c.Connected -= ConnectedHandle;
            Hub1c.Disconnected -= DisconnectedHandle;
            Hub1c.MessageReceived -= MessageReceivedHandle;
        }
    }
}
