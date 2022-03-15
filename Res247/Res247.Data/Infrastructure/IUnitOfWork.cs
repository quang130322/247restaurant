using Res247.Data.Infrastructure.Repositories;
using Res247.Models.BaseEntities;
using Res247.Models.Common;
using System;
using System.Threading.Tasks;

namespace Res247.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        Res247Context DataContext { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        ICoreRepository<T> CoreRepository<T>() where T : BaseEntity;

        #region Master Data

        ICoreRepository<Category> CategoryRepository { get; }

        ICoreRepository<OrderDetail> OrderDetailRepository { get; }

        ICoreRepository<Food> FoodRepository { get; }

        ICoreRepository<Order> OrderRepository { get; }

        ICoreRepository<CovidInfo> CovidInfoRepository { get; }

        ICoreRepository<Shipper> ShipperRepository { get; }
        ICoreRepository<CovidShipper> CovidShipperRepository { get; }

        #endregion

    }
}
