using FrightForce.API.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace FrightForce.API;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddProblemDetails();
        services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();
         // Add API Versioning
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        // Add versioned API explorer for Swagger if needed
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    public static IApplicationBuilder UseApiPipelines(this IApplicationBuilder app)
    {
        //app.UseMiddleware<AccessControlMiddleware>();
        app.UseMiddleware<UserDetailsResolver>();
        return app;
    }
}