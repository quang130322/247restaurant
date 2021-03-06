using Res247.Data.Infrastructure;
using Res247.Models.Common;
using Res247.Services.BaseServices;
using System.Collections.Generic;
using System.Linq;

namespace Res247.Services
{
    public class OrderServices : BaseServices<Order>, IOrderServices
    {
        public OrderServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Order> GetNewest()
        {
            return _unitOfWork.OrderRepository.GetQuery().Where(x=>x.Status == 0).ToList();
        }

        public IEnumerable<Order> GetOrderHistory(string accId)
        {
            return _unitOfWork.OrderRepository.GetQuery().Where(x=>x.AccountId == accId).OrderByDescending(d=>d.OrderDate).ToList();
        }

        public IEnumerable<Order> GetShippedOrderHistory(string accId)
        {
            return _unitOfWork.OrderRepository.GetQuery().Where(x => x.AccountId == accId && x.Status == 2 || x.Status == 1).OrderByDescending(d => d.OrderDate).ToList();
        }

        public IEnumerable<Order> GetShippingHistory(int shipperId)
        {
            return _unitOfWork.OrderRepository.GetQuery().Where(x=>x.ShipperId == shipperId && x.Status == 2 || x.Status == 1).OrderByDescending(d=>d.OrderDate).ToList();
        }

        public IEnumerable<Order> GetShippingOrder()
        {
            return _unitOfWork.OrderRepository.GetQuery().Where(x => x.Status == 1).ToList();
        }


    }
}
