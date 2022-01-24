using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Res247.Data.Infrastructure.Repositories
{
    public interface ICoreRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(int id);

        TEntity GetById(int id);

        void Add(TEntity entity);

        void Add(IEnumerable<TEntity> entities);

        void Delete(TEntity entity, bool isHardDelete = false);

        void Delete(IEnumerable<TEntity> entities, bool isHardDelete = false);

        IQueryable<TEntity> GetQuery();

        IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> where);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", bool canLoadDeleted = false);

        void Update(TEntity entity);
    }
}
