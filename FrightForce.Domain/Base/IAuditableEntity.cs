namespace FrightForce.Domain.Base;

public interface IAuditableEntity<TKey> : IEntity<TKey>, IAuditable
{

}