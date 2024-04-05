using FrightForce.Domain.Base;

namespace FrightForce.Domain.Identity;

public class ApplicationUserCompanyAccessControl : IAuditable, ISoftDeletable
{
    public string UserName { get; set; }
    public int CompanyId { get; set; }

    public bool IsLocked { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }


    private ApplicationUserCompanyAccessControl()
    {
    }

    public ApplicationUserCompanyAccessControl(string userName, int companyId)
    {
        UserName = userName ?? throw new ArgumentNullException(nameof(userName), "Username Cannot Be Null.");
        CompanyId = companyId;
    }

    public static ApplicationUserCompanyAccessControl Create(string username, int companyId)
    {
        return new ApplicationUserCompanyAccessControl(username, companyId);
    }


    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.Now;
    }

    public void Update(int companyId, string newUsername, bool isLocked)
    {
        CompanyId = companyId;
        UserName = newUsername ?? throw new ArgumentNullException(nameof(newUsername), "Username Cannot Be Null.");
        IsLocked = isLocked;
    }


}