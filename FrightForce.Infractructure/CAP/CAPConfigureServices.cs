using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using Savorboard.CAP.InMemoryMessageQueue;

namespace FrightForce.Infractructure.CAP;

public static class CAPConfigureServices
{
    public static IServiceCollection AddCapMessaging(this IServiceCollection services)
    {

        services.AddCap(x =>
        {
            x.UseInMemoryStorage();
            x.UseInMemoryMessageQueue();
            x.UseDashboard();
        }).AddSubscribeFilter<EventBusInterceptorFilter>();

        services.AddScoped<EventBusInterceptor, TenantServiceEventBusInterceptor>();

        services.Scan(s =>
            s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(ICapSubscribe)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());


        return services;
    }
}