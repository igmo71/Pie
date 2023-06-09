namespace Pie.Data.Services.EventBus
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationEventBus(this IServiceCollection services)
        {
            services.AddSingleton<AppEventDispatcher>();

            services.AddScoped<IAppEventHandler<DocOutDtoReceivedEvent>, DocOutEventHandler>();
            services.AddScoped<IAppEventHandler<DocOutCreatedEvent>, DocOutEventHandler>();

            return services;
        }
    }
}
