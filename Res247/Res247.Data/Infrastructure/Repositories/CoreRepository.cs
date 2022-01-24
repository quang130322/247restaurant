using Res247.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Res247.Data.Infrastructure.Repositories
{
    public class CoreRepository<TEntity> : ICoreRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected readonly Res247Context _context;
        private readonly DbSet<TEntity> DbSet;

        public CoreRepository(Res247Context context)
        {
            _context = context;
            //Find property with typeof(T) on dataContext
            var typeOfDbSet = typeof(DbSet<TEntity>);
            foreach (var prop in context.GetType().GetProperties())
            {
                if (typeOfDbSet == prop.PropertyType)
                {
                    DbSet = prop.GetValue(context, null) as DbSet<TEntity>;
                    break;
                }
            }

            if (DbSet == null)
            {
                DbSet = context.Set<TEntity>();
            }
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public void Delete(TEntity entity, bool isHardDelete = false)
        {
            if (isHardDelete)
            {
                DbSet.Remove(entity);
            }
            else
            {
                entity.IsDeleted = true;
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(IEnumerable<TEntity> entities, bool isHardDelete = false)
        {
            if (isHardDelete)
            {
                DbSet.RemoveRange(entities);
            }
            else
            {
                foreach (var entity in entities)
                {
                    entity.IsDeleted = true;
                }
            }
        }

        public void Delete(Expression<Func<TEntity, bool>> where, bool isHardDelete = false)
        {
            var entities = GetQuery(where).AsEnumerable();

            //use this overload instead of using foreach to improve performance
            Delete(entities, isHardDelete);
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", bool canLoadDeleted = false)
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (canLoadDeleted == false)
            {
                query = query.Where(x => x.IsDeleted == canLoadDeleted);
            }

            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy != null ? orderBy(query) : query;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual TEntity GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetQuery()
        {
            return DbSet;
        }

        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.Where(where);
        }

        public void Update(TEntity entity)
        {
            //_dbSet.AddOrUpdate(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task<IEnumerable<TEntity>> GetByPageAsync(Expression<Func<TEntity, bool>> condition, int size, int page)
        {
            return await DbSet.Where(condition).Skip(size * (page - 1)).Take(size).ToListAsync();
        }
    }
}
