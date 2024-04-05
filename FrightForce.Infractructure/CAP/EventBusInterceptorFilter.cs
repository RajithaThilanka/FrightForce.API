using DotNetCore.CAP.Filter;
using Microsoft.Extensions.DependencyInjection;

namespace FrightForce.Infractructure.CAP;

public class EventBusInterceptorFilter : ISubscribeFilter
{
    private readonly IEnumerable<EventBusInterceptor> _eventBusInterceptors;

    public EventBusInterceptorFilter(IServiceProvider serviceProvider)
    {
        _eventBusInterceptors = serviceProvider.GetServices<EventBusInterceptor>();
    }

    public async Task OnSubscribeExceptionAsync(ExceptionContext context)
    {
        foreach (EventBusInterceptor eventBusInterceptor in _eventBusInterceptors)
        {
            await eventBusInterceptor.OnSubscribeExceptionAsync(context);
        }
    }

    public async Task OnSubscribeExecutedAsync(ExecutedContext context)
    {
        foreach (EventBusInterceptor eventBusInterceptor in _eventBusInterceptors)
        {
            await eventBusInterceptor.OnSubscribeExecutedAsync(context);
        }
    }

    public async Task OnSubscribeExecutingAsync(ExecutingContext context)
    {
        foreach (EventBusInterceptor eventBusInterceptor in _eventBusInterceptors)
        {
            await eventBusInterceptor.OnSubscribeExecutingAsync(context);
        }
    }
}