using Pie.Data.Services.Identity;
using Pie.Data.Services.In;
using Pie.Data.Services.Out;

namespace Pie.Data.Services
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<WeatherForecastService>();
            services.AddScoped<AppUserService>();
            services.AddScoped<SearchOutParameters>();
            services.AddScoped<BaseDocService>();
            services.AddScoped<DocInService>();
            services.AddScoped<DocOutService>();
            services.AddScoped<DocOutHistoryService>();
            services.AddScoped<DocOutProductHistoryService>();
            services.AddScoped<ProductService>();
            services.AddScoped<WarehouseService>();
            services.AddScoped<StatusInService>();
            services.AddScoped<StatusOutService>();
            services.AddScoped<QueueInService>();
            services.AddScoped<QueueOutService>();
            services.AddScoped<ChangeReasonOutService>();

            services.AddSingleton<EventDispatcher>();

            return services;
        }
    }
}
