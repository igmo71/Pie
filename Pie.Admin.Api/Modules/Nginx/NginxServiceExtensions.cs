namespace Pie.Admin.Api.Modules.Nginx
{
    static class NginxServiceExtensions
    {
        internal static IServiceCollection AddNginxService(this IServiceCollection services)
        {
            services.AddScoped<INginxService, NginxService>();
            return services;
        }
    }
}
