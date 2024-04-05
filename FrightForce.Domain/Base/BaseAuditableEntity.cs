namespace FrightForce.Domain.Base;

public class BaseAuditableEntity<TKey> : IAuditableEntity<TKey>, ISoftDeletable
{
    public TKey Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.Now;
    }

    public void Undelete()
    {
        IsDeleted = false;
        DeletedAt = null;
    }
}