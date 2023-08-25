using Microsoft.AspNetCore.SignalR;

namespace Pie.Connectors.Connector1c
{
    public class Hub1c : Hub
    {
        private static string connectionId = string.Empty;
        public static string ConnectionId => connectionId;

        private readonly ILogger<Hub1c> _logger;

        public Hub1c(ILogger<Hub1c> logger)
        {
            _logger = logger;
        }

        public static event EventHandler<string>? MessageReceived;
        public static event EventHandler<string?>? Disconnected;
        public static event EventHandler<string?>? Connected;

        public void GetMessage(string message)
        {
            _logger.LogDebug("Hub1c GetMessage {message}", message);
            OnMessageReceived(message);
        }

        public string GetMessageWithResponse(string message)
        {
            _logger.LogDebug("Hub1c GetMessageWithResponse {message}", message);
            OnMessageReceived(message);
            return $"Hub Received: {message}";
        }

        private void OnMessageReceived(string content)
        {
            _logger.LogDebug("Hub1c OnMessageReceived {content}", content);
            MessageReceived?.Invoke(this, content);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync(); // TODO: ???
            connectionId = Context.ConnectionId;

            _logger.LogDebug("Hub1c OnConnectedAsync connectionId: {connectionId}", connectionId);

            Connected?.Invoke(this, connectionId);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception); // TODO: ???

            _logger.LogDebug("Hub1c OnDisconnectedAsync connectionId: {connectionId} {exception}", connectionId, exception);

            Disconnected?.Invoke(this, $"connectionId: {connectionId}; exception.Message: {exception?.Message}");
            connectionId = string.Empty;
        }
    }
}
