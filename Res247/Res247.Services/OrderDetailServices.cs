using Res247.Data.Infrastructure;
using Res247.Models.Common;
using Res247.Services.BaseServices;

namespace Res247.Services
{
    public class OrderDetailServices : BaseServices<OrderDetail>, IOrderDetailServices
    {
        public OrderDetailServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
