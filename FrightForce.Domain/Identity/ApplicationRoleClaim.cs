using Microsoft.AspNetCore.Identity;

namespace FrightForce.Domain.Identity;

public class ApplicationRoleClaim : IdentityRoleClaim<int>, ICompanyScoped<int>
{
    public int CompanyId { get; set; }
}