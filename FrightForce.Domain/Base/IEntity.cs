namespace FrightForce.Domain.Base;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}