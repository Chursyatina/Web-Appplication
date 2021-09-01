namespace Application.Interfaces.ServicesInterfaces
{
    using Application.DTO.Request.OrderRequestDtos;
    using Application.DTO.Response;

    public interface IOrderService : IService<OrderDto, OrderCreateRequestDto, OrderUpdateRequestDto, OrderPatchRequestDto>
    {
    }
}
