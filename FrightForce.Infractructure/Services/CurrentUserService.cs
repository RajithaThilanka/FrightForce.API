using FrightForce.Domain.Identity;

namespace FrightForce.Infractructure.Services;

public class CurrentUserService : ICurrentUserService
{
    public int CurrentCompanyId { get; set; }
    public string UserName { get; set; }
}