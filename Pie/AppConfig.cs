using Microsoft.Identity.Client;

namespace Pie
{
    public class AppConfig
    {
        public string? Tenant { get; private set; }
        public string? DbHost { get; private set; }
        public string? DbConnectionString { get; private set; }
        // "Host=postgres_container;Database=PieDb;Username=PieUser;Password=Pwd4Pie;Persist Security Info=True"
        // "DefaultConnection": "Host=postgres.pickit.com;Database=PieDb;Username=PieUser;Password=Pwd4Pie;Persist Security Info=True"
        public string SeqHost { get; private set; } = default!;
        
        public static AppConfig Configure(WebApplicationBuilder builder)
        {
            var config = new AppConfig();
            
            config.DbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                ?? throw new ApplicationException("ConnectionStrings : DefaultConnection not found");

            config.Tenant = builder.Configuration["TENANT"];
            if (config.Tenant != null)
            {
                config.DbConnectionString = config.DbConnectionString.Replace("PieDb", $"PieDb-{config.Tenant}");
            }

            config.DbHost = builder.Configuration["DB_HOST"];
            if (config.DbHost != null)
            {
                //config.DbConnectionString = config.DbConnectionString.Replace("postgres_container", config.DbHost);
                config.DbConnectionString = config.DbConnectionString.Replace("postgres.pickit.com", config.DbHost);
            }

            config.SeqHost = $"http://{builder.Configuration["SEQ_HOST"] ?? builder.Configuration["SeqHost"]}:5341";

            return config;
        }
    }
}
