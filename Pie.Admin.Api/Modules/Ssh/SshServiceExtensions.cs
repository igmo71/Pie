namespace Pie.Admin.Api.Modules.Ssh
{
    static class SshServiceExtensions
    {
        internal static IServiceCollection AddSshService(this IServiceCollection services)
        {
            services.AddTransient<ISshService, SshService>();
            return services;
        }
    }
}
