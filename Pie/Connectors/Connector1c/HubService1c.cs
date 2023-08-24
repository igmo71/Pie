using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
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

            Hub1c.MessageReceived += MessageReceivedHandle;
        }

        private void MessageReceivedHandle(object? sender, string message)
        {
            _logger.LogInformation($"HubService1c MessageReceivedHandle: {message}");
        }

        public async Task<string> SendInAsync(string request)
        {
            string client1cConfig = JsonSerializer.Serialize(_client1cConfig);

            string response = await _hubContext.Clients.Client(Hub1c.ConnectionId)
                .InvokeAsync<string>(method: "PostDocInDto", arg1: client1cConfig, arg2: request, CancellationToken.None);

            return response;
        }

        public async Task<string> SendOutAsync(string request)
        {
            string client1cConfig = JsonSerializer.Serialize(_client1cConfig);

            string response = await _hubContext.Clients.Client(Hub1c.ConnectionId)
                .InvokeAsync<string>(method: "PostDocOutDto", arg1: client1cConfig, arg2: request, CancellationToken.None);

            return response;
        }

        public void Dispose()
        {
            Hub1c.MessageReceived -= MessageReceivedHandle;
        }
    }
}
