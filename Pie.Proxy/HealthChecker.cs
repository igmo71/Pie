namespace Pie.Proxy
{
    public class HealthChecker : BackgroundService
    {
        private readonly ILogger<HealthChecker> _logger;
        private readonly HubClient1c _hubClient;
        private int _healthCheck;

        public HealthChecker(IConfiguration configuration, ILogger<HealthChecker> logger, HubClient1c hubClient)
        {
            _healthCheck = configuration.GetValue<int>("HealthCheck");
            _logger = logger;
            _hubClient = hubClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("HealthCheck: {State}", _hubClient.State);
                    
                    await _hubClient.SendMessageAsync($"Pie.Proxy HealthCheck: {_hubClient.State}");
                    
                    await Task.Delay(_healthCheck * 1000, stoppingToken);

                }
                catch (Exception ex)
                {
                    _logger.LogError("HealthCheck: {State} {Message} {ex}", _hubClient.State, ex.Message, ex);
                    //throw;
                }
            }
        }
    }
}