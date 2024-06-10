using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Streamphony.Domain.Models.Auth;
using Streamphony.Infrastructure.Options;
using Streamphony.Infrastructure.Persistence.Contexts;

namespace Streamphony.WebAPI.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(nameof(JwtSettings), jwtSettings);

        var jwtSection = configuration.GetSection(nameof(JwtSettings));
        services.Configure<JwtSettings>(jwtSection);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = jwtSettings.GetSymmetricSecurityKey(),

                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudiences = jwtSettings.Audiences,

                    ValidateLifetime = true
                };
                options.ClaimsIssuer = jwtSettings.Issuer;
                options.Audience = jwtSettings.Audiences[0];
            });

        services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<Role>()
            .AddSignInManager()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }
}
