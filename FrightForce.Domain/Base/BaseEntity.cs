namespace FrightForce.Domain.Base;

public abstract class BaseEntity<TKey> : IEntity<TKey>, ISoftDeletable
{
    public TKey Id { get; set; }
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