namespace Application.Interfaces
{
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Domain.Models;

    public interface IOrderLineService : IService<OrderLineDto, OrderLineCreateRequestDto, OrderLineUpdateRequestDto, OrderLinePatchRequestDto>
    {
        public IEnumerable<int> GetIdentificators();

        public OrderLineDto InsertToBasket(OrderLineCreateRequestDto item);

        public OrderLine GetModelById(int id);
    }
}
