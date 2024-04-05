using Microsoft.AspNetCore.Identity;

namespace FrightForce.Domain.Identity;

public class ApplicationUserRole : IdentityUserRole<int>, ICompanyScoped<int>
{
    public int CompanyId { get; set; }
}