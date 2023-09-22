using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StorifyAPI.Context;
using StorifyAPI.Models.Auth;
using StorifyAPI.Models.Employee;
using System.Text;
using System.Text.Json.Serialization;
using NLog;
using Stories;
using LoggerService;
using Entities;

namespace StorifyAPI
{
    public class Startup
    {
        public readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddDbContext<StorifyContext>(option => option.UseSqlServer(_configuration.GetConnectionString("Storify") ?? throw new InvalidOperationException("Can't found Storify Connection String While Working With Store.")));

            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(_configuration.GetConnectionString("Storify") ?? throw new InvalidOperationException("Can't found Storify Connection String While Working With Repository."), 
                b => b.MigrationsAssembly("StorifyAPI") ) );    

            #region Service Extentions

            services.ConfigCORS(); // CORS Config Policy
            services.ConfigIISIntegration(); // IIS Option
            services.AddScoped<ILoggerManager, LoggerManager>(); // Configure Logger Service

            #endregion

            #region Identity Service

            services.AddDbContext<IdentityContext>(option => option.UseSqlServer(_configuration.GetConnectionString("Storify") ?? throw new InvalidOperationException("Can't found Storify Connection String While Working With Identity.")));

            services.AddIdentity<StoreUser, IdentityRole>(
                option =>
                {
                    option.Password.RequireDigit = true;
                    option.Password.RequireUppercase = true;
                    option.Password.RequireLowercase = true;
                    option.Password.RequireNonAlphanumeric = true;
                    option.Password.RequiredLength = 8;
                    option.User.RequireUniqueEmail = true;
                }
                )
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            #endregion

            #region JWT Config
            var jwtSetting = _configuration.GetSection("JwtSettings");
            var secretKey = _configuration.GetSection("Secret").Value; // better be system environment variable
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSetting.GetSection("validIssuer").Value,
                    ValidAudience = jwtSetting.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });

            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            #endregion

            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        }

        public void Configure (WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else //MoAsh
            {
                app.UseHsts(); // Strict-Transport-Security header
            }

            app.UseHttpsRedirection(); // Enforce Https requests

            #region App Config

            app.UseStaticFiles();
            app.UseCors(ServiceExtensions.corsPolicy);
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            #endregion

            //app.UseRouting();
            app.MapControllers();

            //Authentification and Authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
