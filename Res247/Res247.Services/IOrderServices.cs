using Res247.Models.Common;
using Res247.Services.BaseServices;
using System.Collections.Generic;

namespace Res247.Services
{
    public interface IOrderServices : IBaseService<Order>
    {
        IEnumerable<Order> GetNewest();
        IEnumerable<Order> GetShippingOrder();
        IEnumerable<Order> GetOrderHistory(string accId);
        IEnumerable<Order> GetShippingOrderOfShipper(string accId);
    }
}
