namespace Application.Interfaces.ServicesInterfaces
{
    using System.Collections.Generic;
    using Application.DTO.Request.OrderStatusRequestDtos;
    using Application.DTO.Response;

    public interface IOrderStatusService : IService<OrderStatusDto, OrderStatusCreateRequestDto, OrderStatusUpdateRequestDto, OrderStatusPatchRequestDto>, INamedEntityService<OrderStatusDto>
    {
        public IEnumerable<int> GetIdentificators();
    }
}
