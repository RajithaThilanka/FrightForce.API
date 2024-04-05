namespace FrightForce.Domain.Identity;

public interface ICurrentUserService
{
    int CurrentCompanyId { get; set; }
    string UserName { get; set; }
}