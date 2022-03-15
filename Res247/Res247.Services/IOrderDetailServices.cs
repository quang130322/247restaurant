using Res247.Models.Common;
using Res247.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res247.Services
{
    public interface IOrderDetailServices : IBaseService<OrderDetail>
    {
        IEnumerable<OrderDetail> GetOrderDetailsByOrder(int orderId);
    }
}
