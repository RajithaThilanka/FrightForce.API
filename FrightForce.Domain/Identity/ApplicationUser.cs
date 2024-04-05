using FrightForce.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace FrightForce.Domain.Identity;

public class ApplicationUser : IdentityUser<int>, ISoftDeletable, IAuditable
{
    public string FirstName { get; set; }
    public override string UserName { get; set; }
    public string LastName { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
}