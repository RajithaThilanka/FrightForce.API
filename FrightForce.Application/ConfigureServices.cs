using System.Reflection;
using FrightForce.Application.Common.Behaviours;
using FrightForce.Application.Common.Services;
using FrightForce.Domain.Base;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FrightForce.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR();
        services.AddMapster();

        services.AddCommonServices();

        return services;
    }

    private static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        //services.AddScoped<IIdentityService, DefaultIdentityService>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(EfTxBehaviour<,>));
        //services.AddScoped<IDocumentService,DocumentService>();
            

        return services;
    }


    private static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        // register Mediatr
        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
        );
        return services;
    }

    private static IServiceCollection AddMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();


        return services;
    }
}