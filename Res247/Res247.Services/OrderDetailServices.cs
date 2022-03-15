using Res247.Data.Infrastructure;
using Res247.Models.Common;
using Res247.Services.BaseServices;
using System.Collections.Generic;
using System.Linq;

namespace Res247.Services
{
    public class OrderDetailServices : BaseServices<OrderDetail>, IOrderDetailServices
    {
        public OrderDetailServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<OrderDetail> GetOrderDetailsByOrder(int orderId)
        {
            return _unitOfWork.OrderDetailRepository.GetQuery().Where(x=>x.OrderId == orderId).ToList();
        }
    }
}
