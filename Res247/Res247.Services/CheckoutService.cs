using Res247.Data.Infrastructure;
using Res247.Data.Infrastructure.Repositories;
using Res247.Models.Common;
using System;
using System.Collections.Generic;

namespace Res247.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICoreRepository<Order> _orderRepository;
        private readonly ICoreRepository<OrderDetail> _orderDetailRepository;

        public CheckoutService(IUnitOfWork unitOfWork,
                               ICoreRepository<Order> orderRepository,
                               ICoreRepository<Food> foodRepository,
                               ICoreRepository<OrderDetail> orderDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public void Checkout(Order order, List<OrderDetail> orderDetails)
        {
            order.OrderDate = DateTime.Now;
            _orderRepository.Add(order);

            foreach (var item in orderDetails)
            {
                item.Order = order;
                _orderDetailRepository.Add(item);
            }

            _unitOfWork.SaveChanges();
        }
    }
}
