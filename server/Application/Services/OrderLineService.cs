namespace Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.AutoMapper;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Domain.Models;
    using Domain.Repository;

    public class OrderLineService : IOrderLineService
    {
        private IOrderLineRepository _orderLineRepository;

        public OrderLineService(IOrderLineRepository orderLineRepository)
        {
            _orderLineRepository = orderLineRepository;
        }

        public void Delete(string id)
        {
            _orderLineRepository.Delete(id);
        }

        public IEnumerable<OrderLineDto> GetAll()
        {
            return _orderLineRepository.GetAll().Select(x => x.ToViewModel());
        }

        public OrderLineDto GetById(string id)
        {
            var existingOrderLine = _orderLineRepository.GetById(id);

            if (existingOrderLine != null)
            {
                return existingOrderLine.ToViewModel();
            }

            return null;
        }

        public OrderLine GetModelById(string id)
        {
            var existingOrderLine = _orderLineRepository.GetById(id);

            if (existingOrderLine != null)
            {
                return existingOrderLine;
            }

            return null;
        }

        public IEnumerable<string> GetIdentificators()
        {
            return _orderLineRepository.GetIdentificators();
        }

        public OrderLineDto Insert(OrderLineCreateRequestDto item)
        {
            string pizzaVariationId = item.PizzaVariationId;
            string orderId = item.OrderId;

            return _orderLineRepository.Insert(item.ToModel(), pizzaVariationId, orderId).ToViewModel();
        }

        public OrderLineDto InsertToBasket(OrderLineCreateRequestDto item)
        {
            string pizzaVariationId = item.PizzaVariationId;
            string basketId = item.OrderId;

            return _orderLineRepository.InsertToBasket(item.ToModel(), pizzaVariationId, basketId).ToViewModel();
        }

        public OrderLineDto Patch(string id, OrderLinePatchRequestDto item)
        {
            string pizzaVariationId = item.PizzaVariationId;

            return _orderLineRepository.Patch(id, item.ToModel(), pizzaVariationId).ToViewModel();
        }

        public OrderLineDto Update(string id, OrderLineUpdateRequestDto item)
        {
            string pizzaVariationId = item.PizzaVariationId;

            return _orderLineRepository.Update(id, item.ToModel(), pizzaVariationId).ToViewModel();
        }
    }
}
