using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Res247.Services.BaseServices
{
    public interface IBaseService<TEntity>
    {
        int Add(TEntity entity);

        Task<int> AddAsync(TEntity entity);

        int AddRange(IEnumerable<TEntity> entities);

        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);

        bool Update(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        bool Delete(int id);

        Task<bool> DeleteAsync(int id);

        bool Delete(TEntity entity);

        Task<bool> DeleteAsync(TEntity entity);

        TEntity GetById(int id);

        Task<TEntity> GetByIdAsync(int id);

        IEnumerable<TEntity> GetAll(bool isIncludeDelete = false);

        Task<IEnumerable<TEntity>> GetAllAsync(bool isIncludeDelete = false);
    }
}
