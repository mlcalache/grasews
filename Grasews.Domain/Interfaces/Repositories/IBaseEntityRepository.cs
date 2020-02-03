using Grasews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IBaseEntityRepository<TEntity, TKey> : IDisposable where TEntity : BaseEntity<TKey>
    {
        TEntity Get(TKey id, bool @readonly = true);

        IQueryable<TEntity> GetAll(bool @readonly = true);

        void Create(TEntity entity);

        void CreateRange(IEnumerable<TEntity> entities);

        void Remove(TKey id);

        void RemoveRange(IEnumerable<TEntity> entities);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = true);

        int SaveChanges();

        void Update(TEntity entity, params string[] ignoreProperties);
    }

    public interface IBaseEntityRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        //TEntity Get(TKey id, bool @readonly = true);

        IQueryable<TEntity> GetAll(bool @readonly = true);

        void Create(TEntity entity);

        void CreateRange(IEnumerable<TEntity> entities);

        //void Remove(TKey id);

        void RemoveRange(IEnumerable<TEntity> entities);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = true);

        int SaveChanges();

        void Update(TEntity entity, params string[] ignoreProperties);
    }
}