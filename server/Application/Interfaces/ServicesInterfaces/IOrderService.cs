namespace Application.Interfaces.ServicesInterfaces
{
    using System.Collections.Generic;
    using Application.DTO.Request.OrderRequestDtos;
    using Application.DTO.Response;
    using Domain.Models;

    public interface IOrderService : IService<OrderDto, OrderCreateRequestDto, OrderUpdateRequestDto, OrderPatchRequestDto>
    {
        public IEnumerable<string> GetIdentificators();

        public OrderDto Insert(OrderCreateRequestDto item, User user, List<string> orderLinesIds);
    }
}
