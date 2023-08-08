namespace Pie
{
    public class AppConfig
    {
        public string? DbHost { get; private set; }
        public string? SeqHost { get; private set; }
        public string? Tenant { get; private set; }
        public string? DbConnectionString { get; private set; }
        // "Host=postgres_container;Database=PieDb;Username=PieUser;Password=Pwd4Pie;Persist Security Info=True"
        
        public static AppConfig Configure(WebApplicationBuilder builder)
        {
            var config = new AppConfig();
            config.DbHost = builder.Configuration["ENV_DB_HOST"];
            config.SeqHost = builder.Configuration["ENV_SEQ_HOST"];
            config.Tenant = builder.Configuration["ENV_TENANT"];
            
            config.DbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            if (config.DbHost != null)
                config.DbConnectionString = config.DbConnectionString.Replace("postgres_container", config.DbHost);

            if(config.Tenant != null)
                config.DbConnectionString = config.DbConnectionString.Replace("PieDb", $"PieDb-{config.Tenant}");

            return config;
        }
    }
}
