using Mapster;
using Pie.Data.Models;
using Pie.Data.Services.Application;
using System.Reflection;

namespace Pie.Data.Services
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<WeatherForecastService>();
            services.AddScoped<CurrentUserService>();
            services.AddScoped<SearchParameters>();
            services.AddScoped<BaseDocService>();
            services.AddScoped<DocInService>();
            services.AddScoped<DocOutService>();
            services.AddScoped<ProductService>();
            services.AddScoped<WarehouseService>();
            services.AddScoped<StatusInService>();
            services.AddScoped<StatusOutService>();
            services.AddScoped<QueueInService>();
            services.AddScoped<QueueOutService>();

            services.AddMapster();

            return services;
        }

        public static IServiceCollection AddMapster(this IServiceCollection services)
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            Assembly applicationAssembly = typeof(MappedModel).Assembly;
            typeAdapterConfig.Scan(applicationAssembly);

            return services;
        }
    }
}
