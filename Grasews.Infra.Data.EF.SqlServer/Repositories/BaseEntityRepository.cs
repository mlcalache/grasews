using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using Grasews.Infra.Data.EF.SqlServer.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public abstract class BaseEntityRepository<TEntity, TKey> : IBaseEntityRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>, new()
    {
        protected readonly GrasewsContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        private bool _disposed;

        protected BaseEntityRepository()
        {
            _context = new GrasewsContext();

            _dbSet = _context.Set<TEntity>();
        }

        public virtual void Create(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void CreateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = true)
        {
            return @readonly
                ? _dbSet.AsNoTracking()
                    .Where(predicate)
                : _dbSet
                    .Where(predicate);
        }

        public virtual TEntity Get(TKey id, bool @readonly = true)
        {
            return @readonly
                ? _dbSet.AsNoTracking()
                    .SingleOrDefault(p => p.Id.ToString() == id.ToString())
                : _dbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll(bool @readonly = true)
        {
            return @readonly
                ? _dbSet.AsNoTracking()
                : _dbSet;
        }

        public virtual void Remove(TKey id)
        {
            _dbSet.Remove(_dbSet.Find(id));
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");

                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                throw;
            }
        }

        public virtual void Update(TEntity entity, params string[] ignoreProperties)
        {
            var entry = _context.Entry(entity);

            entry.State = EntityState.Modified;

            _context.IgnoreChanges(entry, ignoreProperties);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }

                _disposed = true;
            }
        }
    }
}