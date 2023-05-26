using Microsoft.AspNetCore.SignalR;

namespace Pie.Connectors.Connector1c
{
    public class HubService1c: IDisposable
    {
        private readonly IHubContext<Hub1c> _hubContext;
        private readonly ILogger<HubService1c> _logger;

        public HubService1c(IHubContext<Hub1c> hubContext, ILogger<HubService1c> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
            Hub1c.MessageReceived += MessageReceivedHandle;
        }

        private void MessageReceivedHandle(object? sender, string message)
        {
            _logger.LogInformation($"HubService1c MessageReceivedHandle: {message}");
        }

        public async Task<string> SendInAsync(string request)
        {
            string response = await _hubContext.Clients.Client(Hub1c.ConnectionId)
                .InvokeAsync<string>(method: "PostDocInDto", arg1: request, CancellationToken.None);
            return response;
        }

        public async Task<string> SendOutAsync(string request)
        {
            string response = await _hubContext.Clients.Client(Hub1c.ConnectionId)
                .InvokeAsync<string>(method: "PostDocOutDto", arg1: request, CancellationToken.None);
            return response;
        }

        public void Dispose()
        {
            Hub1c.MessageReceived -= MessageReceivedHandle;
        }
    }
}
