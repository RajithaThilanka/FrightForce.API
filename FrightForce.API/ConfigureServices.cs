using FrightForce.API.Middleware;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FrightForce.API;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddProblemDetails();
        services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();

        return services;
    }

    public static IApplicationBuilder UseApiPipelines(this IApplicationBuilder app)
    {
        //app.UseMiddleware<AccessControlMiddleware>();
        app.UseMiddleware<UserDetailsResolver>();
        return app;
    }
}