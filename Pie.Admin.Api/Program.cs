using Microsoft.EntityFrameworkCore;
using Pie.Admin.Api.Data;
using Pie.Admin.Api.Modules.Nginx;
using Pie.Admin.Api.Modules.Ssh;

namespace Pie.Admin.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContextFactory<AppDbContext>(options =>
            {
                //options.UseSqlServer(connectionString);
                options.UseNpgsql(connectionString);
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
                options.LogTo(s => System.Diagnostics.Debug.WriteLine(s));
            });

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpClient<NginxClient>();
            builder.Services.AddNginxService();
            builder.Services.AddSshService();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapSshApi();
            app.MapNginxApi();

            app.Run();
        }
    }
}
