using Microsoft.AspNetCore.Identity;

namespace FrightForce.Domain.Identity;

public class ApplicationUserLogin : IdentityUserLogin<int>, ICompanyScoped<int>
{
    public int CompanyId { get; set; }
}