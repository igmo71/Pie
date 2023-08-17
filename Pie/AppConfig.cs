using Microsoft.Extensions.DependencyInjection;

namespace Pie
{
    public class AppConfig
    {
        public const int SKIP = 0;
        public const int TAKE = 100;

        public string? Tenant { get; private set; }
        public string? DbHost { get; private set; }
        public string? SeqHost { get; private set; }
        public string? DbConnectionString { get; private set; }
        // "Host=postgres_container;Database=PieDb;Username=PieUser;Password=Pwd4Pie;Persist Security Info=True"
        
        public static AppConfig Configure(WebApplicationBuilder builder)
        {
            var config = new AppConfig();
            config.Tenant = builder.Configuration["TENANT"];
            config.DbHost = builder.Configuration["DB_HOST"];
            config.SeqHost = builder.Configuration["SEQ_HOST"];
            
            config.DbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                ?? throw new ApplicationException("ConnectionStrings DefaultConnection no found");

            if (config.DbHost != null)
                config.DbConnectionString = config.DbConnectionString.Replace("postgres_container", config.DbHost);

            if(config.Tenant != null)
                config.DbConnectionString = config.DbConnectionString.Replace("PieDb", $"PieDb-{config.Tenant}");

            builder.Services.AddSingleton<AppConfig>(config);

            return config;
        }
    }
}
