namespace Application.Interfaces.ServicesInterfaces
{
    using System.Collections.Generic;
    using Application.DTO.Request.OrderRequestDtos;
    using Application.DTO.Response;

    public interface IOrderService : IService<OrderDto, OrderCreateRequestDto, OrderUpdateRequestDto, OrderPatchRequestDto>
    {
        public IEnumerable<int> GetIdentificators();
    }
}
