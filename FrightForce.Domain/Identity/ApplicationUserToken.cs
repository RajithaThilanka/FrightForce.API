using Microsoft.AspNetCore.Identity;

namespace FrightForce.Domain.Identity;

public class ApplicationUserToken : IdentityUserToken<int>, ICompanyScoped<int>
{
    public int CompanyId { get; set; }
}