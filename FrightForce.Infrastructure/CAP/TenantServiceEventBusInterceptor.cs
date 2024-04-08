using DotNetCore.CAP.Filter;
using FrightForce.Domain.Identity;
using FrightForce.Infractructure.Persistence;
using FrightForce.Infrastructure.Persistence;

namespace FrightForce.Infractructure.CAP;

public class TenantServiceEventBusInterceptor : EventBusInterceptor
{
    private readonly FrightForceDbContext _dbContext;
    public readonly ICurrentUserService _currentUserService;

    public TenantServiceEventBusInterceptor(FrightForceDbContext dbContext, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
    }

    public override async Task OnSubscribeExecutingAsync(ExecutingContext context)
    {
        string? companyId = context.DeliverMessage.Headers["CompanyId"];
        _dbContext.CompanyId = int.Parse(companyId);
    }
}