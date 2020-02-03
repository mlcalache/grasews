using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using Grasews.Infra.CrossCutting.Helpers;
using Grasews.Infra.Data.EF.Postgres.Contexts;
using Sentry;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
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
            try
            {
                _dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                using (SentrySdk.Init(ConfigurationManagerHelper.SentryUrl))
                {
                    SentrySdk.CaptureException(ex);
                }

                throw;
            }
        }

        public virtual void CreateRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _dbSet.AddRange(entities);
            }
            catch (Exception ex)
            {
                using (SentrySdk.Init("https://7328a5cb5f09459dad988287301493a2@sentry.io/1505243"))
                {
                    SentrySdk.CaptureException(ex);
                }

                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = true)
        {
            try
            {
                return @readonly
                        ? _dbSet.AsNoTracking()
                            .Where(predicate)
                        : _dbSet
                            .Where(predicate);
            }
            catch (Exception ex)
            {
                using (SentrySdk.Init("https://7328a5cb5f09459dad988287301493a2@sentry.io/1505243"))
                {
                    SentrySdk.CaptureException(ex);
                }

                throw;
            }
        }

        public virtual TEntity Get(TKey id, bool @readonly = true)
        {
            try
            {
                return @readonly
                    ? _dbSet.AsNoTracking()
                        .SingleOrDefault(p => p.Id.ToString() == id.ToString())
                    : _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                using (SentrySdk.Init("https://7328a5cb5f09459dad988287301493a2@sentry.io/1505243"))
                {
                    SentrySdk.CaptureException(ex);
                }

                throw;
            }
        }

        public virtual IQueryable<TEntity> GetAll(bool @readonly = true)
        {
            try
            {
                return @readonly
                    ? _dbSet.AsNoTracking()
                    : _dbSet;
            }
            catch (Exception ex)
            {
                using (SentrySdk.Init("https://7328a5cb5f09459dad988287301493a2@sentry.io/1505243"))
                {
                    SentrySdk.CaptureException(ex);
                }

                throw;
            }
        }

        public virtual void Remove(TKey id)
        {
            try
            {
                _dbSet.Remove(_dbSet.Find(id));
            }
            catch (Exception ex)
            {
                using (SentrySdk.Init("https://7328a5cb5f09459dad988287301493a2@sentry.io/1505243"))
                {
                    SentrySdk.CaptureException(ex);
                }

                throw;
            }
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _dbSet.RemoveRange(entities);
            }
            catch (Exception ex)
            {
                using (SentrySdk.Init("https://7328a5cb5f09459dad988287301493a2@sentry.io/1505243"))
                {
                    SentrySdk.CaptureException(ex);
                }

                throw;
            }
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    System.Diagnostics.Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");

                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }

                using (SentrySdk.Init("https://7328a5cb5f09459dad988287301493a2@sentry.io/1505243"))
                {
                    SentrySdk.CaptureException(ex);
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

    public abstract class BaseEntityRepository<TEntity> : IBaseEntityRepository<TEntity> where TEntity : BaseEntity, new()
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
            try
            {
                _dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                using (SentrySdk.Init(ConfigurationManagerHelper.SentryUrl))
                {
                    SentrySdk.CaptureException(ex);
                }

                throw;
            }
        }

        public virtual void CreateRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _dbSet.AddRange(entities);
            }
            catch (Exception ex)
            {
                using (SentrySdk.Init("https://7328a5cb5f09459dad988287301493a2@sentry.io/1505243"))
                {
                    SentrySdk.CaptureException(ex);
                }

                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = true)
        {
            try
            {
                return @readonly
                        ? _dbSet.AsNoTracking()
                            .Where(predicate)
                        : _dbSet
                            .Where(predicate);
            }
            catch (Exception ex)
            {
                using (SentrySdk.Init("https://7328a5cb5f09459dad988287301493a2@sentry.io/1505243"))
                {
                    SentrySdk.CaptureException(ex);
                }

                throw;
            }
        }

        public virtual IQueryable<TEntity> GetAll(bool @readonly = true)
        {
            try
            {
                return @readonly
                    ? _dbSet.AsNoTracking()
                    : _dbSet;
            }
            catch (Exception ex)
            {
                using (SentrySdk.Init("https://7328a5cb5f09459dad988287301493a2@sentry.io/1505243"))
                {
                    SentrySdk.CaptureException(ex);
                }

                throw;
            }
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _dbSet.RemoveRange(entities);
            }
            catch (Exception ex)
            {
                using (SentrySdk.Init("https://7328a5cb5f09459dad988287301493a2@sentry.io/1505243"))
                {
                    SentrySdk.CaptureException(ex);
                }

                throw;
            }
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    System.Diagnostics.Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");

                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }

                using (SentrySdk.Init("https://7328a5cb5f09459dad988287301493a2@sentry.io/1505243"))
                {
                    SentrySdk.CaptureException(ex);
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