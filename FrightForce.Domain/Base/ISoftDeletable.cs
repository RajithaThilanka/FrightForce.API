namespace FrightForce.Domain.Base;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
    DateTime? DeletedAt { get; set; }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }

    public void Undelete()
    {
        IsDeleted = false;
        DeletedAt = null;
    }
}