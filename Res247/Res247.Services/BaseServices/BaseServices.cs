using Res247.Common;
using Res247.Data.Infrastructure;
using Res247.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Res247.Services.BaseServices
{
    public class BaseServices<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual int Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _unitOfWork.CoreRepository<TEntity>().Add(entity);
            return _unitOfWork.SaveChanges();
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _unitOfWork.CoreRepository<TEntity>().Add(entity);
            return await _unitOfWork.SaveChangesAsync();
        }

        public int AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }
                _unitOfWork.CoreRepository<TEntity>().Add(item);
            }
            return _unitOfWork.SaveChanges();
        }

        public async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }
                _unitOfWork.CoreRepository<TEntity>().Add(item);
            }
            return await _unitOfWork.SaveChangesAsync();
        }

        public bool Delete(int id)
        {
            var entity = _unitOfWork.CoreRepository<TEntity>().GetById(id);
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _unitOfWork.CoreRepository<TEntity>().Delete(entity);
            return _unitOfWork.SaveChanges() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = _unitOfWork.CoreRepository<TEntity>().GetById(id);
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _unitOfWork.CoreRepository<TEntity>().Delete(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public bool Delete(TEntity entity)
        {
            _unitOfWork.CoreRepository<TEntity>().Delete(entity);
            return _unitOfWork.SaveChanges() > 0;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _unitOfWork.CoreRepository<TEntity>().Delete(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public virtual IEnumerable<TEntity> GetAll(bool isIncludeDelete = false)
        {
            if (!isIncludeDelete)
            {
                return _unitOfWork.CoreRepository<TEntity>().GetQuery(x => x.IsDeleted == isIncludeDelete).ToList();
            }
            return _unitOfWork.CoreRepository<TEntity>().GetQuery().ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(bool isIncludeDelete = false)
        {
            if (!isIncludeDelete)
            {
                return await _unitOfWork.CoreRepository<TEntity>().GetQuery(x => x.IsDeleted == isIncludeDelete).ToListAsync();
            }
            return await _unitOfWork.CoreRepository<TEntity>().GetQuery().ToListAsync();
        }

        public virtual async Task<Paginated<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int pageIndex = 1, int pageSize = 10)
        {
            var query = _unitOfWork.CoreRepository<TEntity>().Get(filter: filter, orderBy: orderBy,
                includeProperties: includeProperties);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await Paginated<TEntity>.CreateAsync(query.AsNoTracking(), pageIndex, pageSize);
        }

        public virtual TEntity GetById(int id)
        {
            return _unitOfWork.CoreRepository<TEntity>().GetById(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _unitOfWork.CoreRepository<TEntity>().GetByIdAsync(id);
        }

        public virtual bool Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _unitOfWork.CoreRepository<TEntity>().Update(entity);
            return _unitOfWork.SaveChanges() > 0;
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _unitOfWork.CoreRepository<TEntity>().Update(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
