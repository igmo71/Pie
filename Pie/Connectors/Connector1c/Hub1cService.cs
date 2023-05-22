using Microsoft.AspNetCore.SignalR;
using Pie.Data.Models.Out;

namespace Pie.Connectors.Connector1c
{
    public class Hub1cService: IDisposable
    {
        private readonly IHubContext<Hub1c> _hubContext;
        private readonly ILogger<Hub1cService> _logger;

        public Hub1cService(IHubContext<Hub1c> hubContext, ILogger<Hub1cService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
            Hub1c.MessageReceived += MessageReceivedHandle;
        }

        private void MessageReceivedHandle(object? sender, string message)
        {
            _logger.LogInformation($"Received: {message}");
        }

        public async Task<string> SendDocOutAsync(DocOutDto docOutDto)
        {
            string result = await _hubContext.Clients.Client(Hub1c.ConnectionId)
                .InvokeAsync<string>("PostDocOutDto", docOutDto, CancellationToken.None);

            _logger.LogInformation("SendDocOutAsync: {result}", result);
            return result;
        }

        public void Dispose()
        {
            Hub1c.MessageReceived -= MessageReceivedHandle;
        }
    }
}
