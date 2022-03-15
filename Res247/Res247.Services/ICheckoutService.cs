using Res247.Models.Common;
using System.Collections.Generic;

namespace Res247.Services
{
    public interface ICheckoutService
    {
        void Checkout(Order order, List<OrderDetail> orderDetails);
    }
}
