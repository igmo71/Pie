using Pie.Connectors.Connector1c;
using Pie.Connectors.Connector1c.Services1c;

namespace Pie.Connectors
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddConnectors(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection client1cConfig = configuration.GetSection(ConnectorsConfig.Section).GetSection(nameof(Client1cConfig));
            services.Configure<Client1cConfig>(client1cConfig);

            services.AddHttpClient<HttpService1c>();
            services.AddHttpClient<ODataService1c>();
            services.AddScoped<HubService1c>();
            services.AddScoped<Service1c>();

            services.AddScoped<DeliveryAreaService1c>();
            services.AddScoped<ProductService1c>();
            services.AddScoped<WarehouseService1c>();

            return services;
        }
    }
}
