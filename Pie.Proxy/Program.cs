namespace Pie.Proxy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Logging.AddEventLog();

            IConfigurationSection client1cConfig = builder.Configuration.GetSection(nameof(Client1cConfig));
            builder.Services.Configure<Client1cConfig>(client1cConfig);

            builder.Services.AddSingleton<HubClient1c>();

            builder.Services.AddHttpClient<HttpService1c>();

            builder.Services.AddHostedService<HealthChecker>();

            builder.Services.AddWindowsService(options => options.ServiceName = "Pie.Proxy");

            IHost host = builder.Build();
            host.Run();
        }
    }
}