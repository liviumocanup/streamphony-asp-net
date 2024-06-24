namespace Streamphony.WebAPI.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddPolicies(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("UserPolicy", policy =>
                policy.RequireRole("User", "Admin"))
            .AddPolicy("ArtistPolicy", policy =>
                policy.RequireRole("Artist", "Admin"));
        
        return services;
    }
}
