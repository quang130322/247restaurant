using Res247.Data.Infrastructure.Repositories;
using Res247.Models.BaseEntities;
using Res247.Models.Common;
using Res247.Models.Security;
using System.Threading.Tasks;

namespace Res247.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Res247Context _dbContext;
        public Res247Context DataContext => _dbContext;

        public UnitOfWork(Res247Context dbContext)
        {
            _dbContext = dbContext;
        }

        private ICoreRepository<Category> _categoryRepository;
        public ICoreRepository<Category> CategoryRepository => _categoryRepository ?? new CoreRepository<Category>(_dbContext);

        private ICoreRepository<OrderDetail> _orderDetailRepository;
        public ICoreRepository<OrderDetail> OrderDetailRepository => _orderDetailRepository?? new CoreRepository<OrderDetail>(_dbContext);

        private ICoreRepository<Food> _foodRepository;
        public ICoreRepository<Food> FoodRepository => _foodRepository ?? new CoreRepository<Food>(_dbContext);

        private ICoreRepository<Order> _orderRepository;
        public ICoreRepository<Order> OrderRepository => _orderRepository ?? new CoreRepository<Order>(_dbContext);

        private ICoreRepository<CovidInfo> _covidInfoRepository;
        public ICoreRepository<CovidInfo> CovidInfoRepository => _covidInfoRepository ?? new CoreRepository<CovidInfo>(_dbContext);

        private ICoreRepository<Shipper> _shipperRepository;
        public ICoreRepository<Shipper> ShipperRepository => _shipperRepository ?? new CoreRepository<Shipper>(_dbContext);
        private ICoreRepository<CovidShipper> _covidShipperRepository;
        public ICoreRepository<CovidShipper> CovidShipperRepository => _covidShipperRepository ?? new CoreRepository<CovidShipper>(_dbContext);

        #region Method

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public ICoreRepository<T> CoreRepository<T>() where T : BaseEntity
        {
            return new CoreRepository<T>(_dbContext);
        }

        #endregion
    }
}
