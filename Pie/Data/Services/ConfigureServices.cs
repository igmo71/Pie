﻿using Mapster;
using Pie.Connectors.Connector1c;
using Pie.Data.Models;
using Pie.Data.Services.Application;
using Pie.Data.Services.In;
using Pie.Data.Services.Out;
using System.Reflection;

namespace Pie.Data.Services
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<WeatherForecastService>();
            services.AddScoped<CurrentUserService>();
            services.AddScoped<SearchOutParameters>();
            services.AddScoped<BaseDocService>();
            services.AddScoped<DocInService>();
            services.AddScoped<DocOutService>();
            services.AddScoped<ProductService>();
            services.AddScoped<WarehouseService>();
            services.AddScoped<StatusInService>();
            services.AddScoped<StatusOutService>();
            services.AddScoped<QueueInService>();
            services.AddScoped<QueueOutService>();
            services.AddScoped<ChangeReasonOutService>();
            services.AddScoped<Client1c>();


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
