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

            ConnectorsConfig connectorsConfig = connectorsConfigSection.Get<ConnectorsConfig>() ?? throw new ApplicationException("Connectors config not found");

            Client1c client1C = connectorsConfig.Client1c ?? throw new ApplicationException("Client1c config not found");

            services.AddHttpClient(nameof(Client1c), httpClient =>
            {
                httpClient.BaseAddress = new Uri(client1C.BaseAddress ?? throw new ApplicationException("Client1c BaseAddress not found"));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                            "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{client1C.UserName}:{client1C.Password}")));
            });

            services.Configure<Client1c>(nameof(Client1c), connectorsConfigSection.GetSection(nameof(Client1c)));
            services.AddScoped<Service1c>();

            return services;
        }
    }
}
