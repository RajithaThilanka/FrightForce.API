namespace FrightForce.Domain.Base;

public interface IRepository<TEntity, in TKey> where TEntity : IEntity<TKey>
{
    TEntity? FindById(TKey id);
    ICollection<TEntity> FindAll();
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Delete(TEntity entity);

    Task<TEntity?> FindByIdAsync(TKey id);
    Task<ICollection<TEntity>> FindAllAsync();
    Task<TEntity> AddAsync(TEntity entity);
    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
    Task<TEntity> DeleteByIdAsync(TKey id);
}