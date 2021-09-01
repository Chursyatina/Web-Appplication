namespace Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.AutoMapper;
    using Application.DTO.Request.OrderStatusRequestDtos;
    using Application.DTO.Response;
    using Application.Interfaces.ServicesInterfaces;
    using Domain.Repository;

    public class OrderStatusService : IOrderStatusService
    {
        private IOrderStatusRepository _orderStatusRepository;

        public OrderStatusService(IOrderStatusRepository orderStatusRepository)
        {
            _orderStatusRepository = orderStatusRepository;
        }

        public void Delete(int id)
        {
            _orderStatusRepository.Delete(id);
        }

        public IEnumerable<OrderStatusDto> GetAll()
        {
            return _orderStatusRepository.GetAll().Select(x => x.ToViewModel());
        }

        public OrderStatusDto GetById(int id)
        {
            var existingOrderStatus = _orderStatusRepository.GetById(id);

            if (existingOrderStatus != null)
            {
                return existingOrderStatus.ToViewModel();
            }

            return null;
        }

        public OrderStatusDto GetByName(string name)
        {
            var existingOrderStatus = _orderStatusRepository.GetByName(name);

            if (existingOrderStatus != null)
            {
                return existingOrderStatus.ToViewModel();
            }

            return null;
        }

        public IEnumerable<int> GetIdentificators()
        {
            return _orderStatusRepository.GetIdentificators();
        }

        public OrderStatusDto Insert(OrderStatusCreateRequestDto item)
        {
            return _orderStatusRepository.Insert(item.ToModel()).ToViewModel();
        }

        public OrderStatusDto Patch(int id, OrderStatusPatchRequestDto item)
        {
            return _orderStatusRepository.Patch(id, item.ToModel()).ToViewModel();
        }

        public OrderStatusDto Update(int id, OrderStatusUpdateRequestDto item)
        {
            return _orderStatusRepository.Update(id, item.ToModel()).ToViewModel();
        }
    }
}
