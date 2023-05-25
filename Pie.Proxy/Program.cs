namespace Pie.Proxy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Logging.AddEventLog();

            builder.Services.AddWindowsService(options => options.ServiceName = "Pie.Proxy");

            builder.Services.AddHostedService<HealthChecker>();

            IHost host = builder.Build();
            host.Run();
        }
    }
}