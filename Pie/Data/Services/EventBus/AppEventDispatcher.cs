namespace Pie.Data.Services.EventBus
{
    public class AppEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public AppEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task PublishAsync<TEvent>(TEvent appEvent) where TEvent : IAppEvent
        {
            using var scope = _serviceProvider.CreateScope();
            var handlers = scope.ServiceProvider.GetServices<IAppEventHandler<TEvent>>();
            foreach (var handler in handlers) {
                await handler.HandleAsync(appEvent);
            }
        }
    }
}
