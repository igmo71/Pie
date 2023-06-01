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

            services.AddScoped<BaseDocService>();
            services.AddScoped<DeliveryAreaService>();
            services.AddScoped<PartnerService>();
            services.AddScoped<ProductService>();
            services.AddScoped<WarehouseService>();

            services.AddScoped<ChangeReasonInService>();
            services.AddScoped<DocInHistoryService>();
            services.AddScoped<DocInProductHistoryService>();
            services.AddScoped<DocInService>();
            services.AddScoped<QueueInService>();
            services.AddScoped<SearchInParameters>();
            services.AddScoped<StatusInService>();

            services.AddScoped<ChangeReasonOutService>();
            services.AddScoped<DocOutHistoryService>();
            services.AddScoped<DocOutProductHistoryService>();
            services.AddScoped<DocOutService>();
            services.AddScoped<QueueOutService>();
            services.AddScoped<SearchOutParameters>();
            services.AddScoped<StatusOutService>();

            services.AddSingleton<EventDispatcher>();

            return services;
        }
    }
}
