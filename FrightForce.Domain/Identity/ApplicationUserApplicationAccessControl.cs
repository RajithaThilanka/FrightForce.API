using FrightForce.Domain.Base;

namespace FrightForce.Domain.Identity;

public class ApplicationUserApplicationAccessControl : BaseAuditableEntity<int>
{
    public string Username { get; private set; }

    private ApplicationUserApplicationAccessControl(string username)
    {
        Username = username;
    }

    public static ApplicationUserApplicationAccessControl Create(string username)
    {
        return new ApplicationUserApplicationAccessControl(username);
    }
    public void Update(string newUsername)
    {
        Username = newUsername ?? throw new ArgumentNullException(nameof(newUsername), "Username Cannot Be Null.");
    }

}