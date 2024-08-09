using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAppForDocker.DB;
using WebAppForDocker.Repositories;
using WebAppForDocker.Repositories.Abstraction;
using WebAppForDocker.RSATools;
using WebAppForDocker.Services.Abstractions;

namespace WebAppForDocker.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new RsaSecurityKey(RSAExtensions.GetPublicKey())
                    };
                });
        }

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("db")).LogTo(Console.WriteLine));
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserReadRepository, UserRepository>();
            services.AddScoped<IUserWriteRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
