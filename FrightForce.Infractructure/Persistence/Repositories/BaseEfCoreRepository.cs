using FrightForce.Domain.Base;
using FrightForce.Infractructure.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FrightForce.Infractructure.Persistence.Repositories;

public abstract class BaseEfCoreRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>, ISoftDeletable
    {
        protected readonly DbContext _context;


        protected BaseEfCoreRepository(DbContext context)
        {
            _context = context;
        }

        public TEntity? FindById(TKey id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public ICollection<TEntity> FindAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            entity.Delete();
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public virtual async Task<TEntity?> FindByIdAsync(TKey id)
        {
            try
            {
                return await _context.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new PersistenceException(ex.Message);
            }
        }

        public async Task<ICollection<TEntity>> FindAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            entity.Delete();
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteByIdAsync(TKey id)
        {
            TEntity entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity is null)
            {
                throw new PersistenceException("Entity Not Found.");
            }

            entity.Delete();
            return await DeleteAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }
    }