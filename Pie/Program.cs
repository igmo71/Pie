using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pie.Areas.Identity;
using Pie.Connectors;
using Pie.Connectors.Connector1c;
using Pie.Data;
using Pie.Data.Models.Identity;
using Pie.Data.Services;
using Pie.Data.Services.EventBus;
using Serilog;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Pie
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var appConfig = AppConfig.Configure(builder);

            //builder.Services.AddLogging(loggingBuilder =>
            //{
            //    loggingBuilder.AddSeq(builder.Configuration.GetSection("Seq"));
            //});

            //builder.Services.AddHttpLogging(logging =>
            //{
            //    //logging.LoggingFields = HttpLoggingFields.All;
            //    logging.LoggingFields =
            //        HttpLoggingFields.RequestPath |
            //        HttpLoggingFields.RequestQuery |
            //        HttpLoggingFields.RequestBody |
            //        HttpLoggingFields.ResponseBody;
            //    logging.MediaTypeOptions.AddText("application/json");
            //    logging.RequestBodyLogLimit = 4096;
            //    logging.ResponseBodyLogLimit = 4096;
            //});
                        
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithProperty("Application", "Pie")
                .Enrich.WithProperty("Tenant", appConfig.Tenant)
                .WriteTo.Console()
                //.WriteTo.Seq($"http://{appConfig.SeqHost ?? "seq_container"}:5341")
                .WriteTo.Seq($"http://{appConfig.SeqHost ?? "192.168.1.137"}:5341")
                .CreateLogger();

            builder.Host.UseSerilog(Log.Logger);


            //builder.Configuration.AddEnvironmentVariables();

            // Add services to the container.
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            var connectionString = appConfig.DbConnectionString;

            builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
            {
                //options.UseSqlServer(connectionString);
                options.UseNpgsql(connectionString);
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
                options.LogTo(s => System.Diagnostics.Debug.WriteLine(s));
            });

            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    //options.UseSqlServer(connectionString);
            //    options.UseNpgsql(connectionString);
            //    options.EnableDetailedErrors();
            //    options.EnableSensitiveDataLogging();
            //    options.LogTo(s => System.Diagnostics.Debug.WriteLine(s));
            //});

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services
                .AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:IssuerSigningKey"]
                            ?? throw new ApplicationException("Issuer Signing Key not found."))),
                        ValidateLifetime = true
                    };
                });

            builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<AppUser>>();

            builder.Services.Configure<JsonSerializerOptions>(options =>
            {
                options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.PropertyNameCaseInsensitive = true;
                options.WriteIndented = true;
                options.Encoder = JavaScriptEncoder.Create(new TextEncoderSettings(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic));
            });

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        new List<string>()
                    }
                });
            });

            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeAreaFolder("Config", "/ChangeReasonsIn");
                options.Conventions.AuthorizeAreaFolder("Config", "/ChangeReasonsOut");
                options.Conventions.AuthorizeAreaFolder("Config", "/QueuesIn");
                options.Conventions.AuthorizeAreaFolder("Config", "/QueuesOut");
                options.Conventions.AuthorizeAreaFolder("Config", "/StatusesIn");
                options.Conventions.AuthorizeAreaFolder("Config", "/StatusesOut");

                options.Conventions.AuthorizeAreaFolder("History", "/DocsIn");
                options.Conventions.AuthorizeAreaFolder("History", "/DocsOut");
            })
                .AddRazorRuntimeCompilation();

            builder.Services.AddServerSideBlazor();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddApplicationServices();
            builder.Services.AddApplicationEventBus();
            builder.Services.AddConnectors(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();

                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
                        
            //app.UseHttpLogging();
            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
            app.MapHub<Hub1c>("/Hub1c");

            app.Run();
        }
    }
}