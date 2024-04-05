using FrightForce.Domain.Base;

namespace FrightForce.Domain.Identity;

public class Privilege : BaseAuditableEntity<int>
{
    public string Resource { get; private set; }
    public string Action { get; private set; }

    public ICollection<ApplicationRole> Roles { get; private set; } = new List<ApplicationRole>();

    private Privilege(string resource, string action)
    {
        Resource = resource;
        Action = action;
    }

    public static Privilege Create(string resource, string action)
    {
        return new Privilege(resource, action);
    }

    public override string ToString()
    {
        return $"{Resource}.{Action}";
    }
}