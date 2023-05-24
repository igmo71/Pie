using Microsoft.Extensions.DependencyInjection;
using Pie.Connectors.Connector1c;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace Pie.Connectors
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddConnectors(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection connectorsConfigSection = configuration.GetSection(ConnectorsConfig.Section);

            //ConnectorsConfig connectorsConfig = connectorsConfigSection.Get<ConnectorsConfig>() ?? throw new ApplicationException("Connectors config not found");

            //Client1cConfig client1cConfig = connectorsConfig.Client1cConfig ?? throw new ApplicationException("Client1c config not found");

            services.Configure<Client1cConfig>(connectorsConfigSection.GetSection(nameof(Client1cConfig)));

            //services.AddScoped<HttpService1c>();
            services.AddHttpClient<HttpService1c>();
            services.AddScoped<HubService1c>();
            services.AddScoped<Service1c>();

            return services;
        }
    }
}
