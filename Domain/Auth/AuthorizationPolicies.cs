using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Auth;

public class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("MustBeCustomer", a =>
                a.RequireAuthenticatedUser().RequireClaim("Type", "customer"));
            options.AddPolicy("MustBeFoodSeller", a =>
                a.RequireAuthenticatedUser().RequireClaim("Type", "foodseller"));
        });
    }
}