namespace Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.AutoMapper;
    using Application.DTO.Request.OrderRequestDtos;
    using Application.DTO.Response;
    using Application.Interfaces.ServicesInterfaces;
    using Domain.Repository;

    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Delete(int id)
        {
            _orderRepository.Delete(id);
        }

        public IEnumerable<OrderDto> GetAll()
        {
            return _orderRepository.GetAll().Select(x => x.ToViewModel()).ToList();
        }

        public OrderDto GetById(int id)
        {
            var existingOrder = _orderRepository.GetById(id);

            if (existingOrder != null)
            {
                return existingOrder.ToViewModel();
            }

            return null;
        }

        public OrderDto Insert(OrderCreateRequestDto item)
        {
            return _orderRepository.Insert(item.ToModel(), item.OrderLinesIds).ToViewModel();
        }

        public OrderDto Patch(int id, OrderPatchRequestDto item)
        {
            return _orderRepository.Patch(id, item.ToModel(), item.OrderLinesIds).ToViewModel();
        }

        public OrderDto Update(int id, OrderUpdateRequestDto item)
        {
            return _orderRepository.Update(id, item.ToModel(), item.OrderLinesIds.ToList()).ToViewModel();
        }
    }
}
