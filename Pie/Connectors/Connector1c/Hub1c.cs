using Microsoft.AspNetCore.SignalR;

namespace Pie.Connectors.Connector1c
{
    public class Hub1c : Hub
    {
        private static string connectionId = string.Empty;
        public static string ConnectionId => connectionId;

        public static event EventHandler<string>? MessageReceived;
        public static event EventHandler<string?>? Disconnected;

        public void GetMessage(string message)
        {
            OnMessageReceived(message);
        }

        public string GetMessageWithResponse(string message)
        {
            OnMessageReceived(message);
            return $"Hub Received: {message}";
        }

        private void OnMessageReceived(string content)
        {
            MessageReceived?.Invoke(this, content);
        }

        public override async Task OnConnectedAsync()
        {
            connectionId = Context.ConnectionId;
            await base.OnConnectedAsync(); // TODO: ???
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            connectionId = string.Empty;
            await base.OnDisconnectedAsync(exception); // TODO: ???
            Disconnected?.Invoke(this, exception?.Message);
        }
    }
}
