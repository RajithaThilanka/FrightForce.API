using FrightForce.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace FrightForce.Domain.Identity;

public class ApplicationRole : IdentityRole<int>, ISoftDeletable, IAuditable, ICompanyScoped<int>
{
    public ICollection<Privilege> Privileges { get; private set; } = new List<Privilege>();
    public int CompanyId { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }


    public void AddPrivilege(Privilege privilege)
    {
        Privileges.Add(privilege);
    }

    public void RemovePrivilege(Privilege privilege)
    {
        Privileges.Remove(privilege);
    }
}