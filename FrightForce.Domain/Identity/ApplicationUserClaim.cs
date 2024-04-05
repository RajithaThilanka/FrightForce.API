using Microsoft.AspNetCore.Identity;

namespace FrightForce.Domain.Identity;

public class ApplicationUserClaim : IdentityUserClaim<int>, ICompanyScoped<int>
{
    public int CompanyId { get; set; }
}