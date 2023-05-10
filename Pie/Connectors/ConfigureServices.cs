using Pie.Connectors.Connector1c;
using System.Net.Http.Headers;
using System.Text;

namespace Pie.Connectors
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddConnectors(this IServiceCollection services, IConfiguration configuration)
        {
            ConnectorsConfig connectorsConfig = configuration.GetSection(ConnectorsConfig.Section).Get<ConnectorsConfig>()
                ?? throw new ApplicationException("ConnectorsConfig not found");

            Client1c client1C = connectorsConfig.Client1c ?? throw new ApplicationException("Client1c config not founf");
            services.AddHttpClient(nameof(Client1c), httpClient =>
            {
                httpClient.BaseAddress = new Uri(client1C.BaseAddress ?? throw new ApplicationException("Client1c.BaseAddress not found"));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                            "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{client1C.UserName}:{client1C.Password}")));
            });

            return services;
        }
    }
}
