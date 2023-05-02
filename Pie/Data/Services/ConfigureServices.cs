namespace Pie.Data.Services
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<WeatherForecastService>();

            return services;
        }
    }
}
