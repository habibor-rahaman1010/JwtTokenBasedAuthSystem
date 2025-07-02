using CustomAuthSystem.DatabaseContext;
using CustomAuthSystem.DomainEntities;
using CustomAuthSystem.Repositories;
using CustomAuthSystem.RepositoriesInterafce;
using CustomAuthSystem.Services;
using CustomAuthSystem.ServicesInterface;
using CustomAuthSystem.UnitOfWorks;
using CustomAuthSystem.UnitOfWorksInterface;
using CustomAuthSystem.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CustomAuthSystem.ExtensionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, string connectionString, string migrationAssembly)
        {
            services.AddDbContext<AuthenticationDbContext>(options => 
                options.UseSqlServer(connectionString, (m) => m.MigrationsAssembly(migrationAssembly)));

            services.AddDbContext<SegaSoftDbContext>(options => 
                options.UseSqlServer(connectionString, (m) => m.MigrationsAssembly(migrationAssembly)));

            services.AddScoped<IApplicationTime, ApplicationTime>();
            services.AddScoped<IAuthenticationUnitOfWork, AuthenticationUnitOfWork>();
            services.AddScoped<ISegaSoftBbUnitOfWork, SegaSoftBbUnitOfWork>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonManagementService, PersonManagementService>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IAuthenticationManagementService, AuthenticationManagementService>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();

            return services;
        }


        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, string key, string issuer, string audience)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                });

            return services;
        }
    }
}
