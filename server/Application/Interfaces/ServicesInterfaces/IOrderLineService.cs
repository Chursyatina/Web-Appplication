namespace Application.Interfaces
{
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response;

    public interface IOrderLineService : IService<OrderLineDto, OrderLineCreateRequestDto, OrderLineUpdateRequestDto, OrderLinePatchRequestDto>
    {
        public IEnumerable<int> GetIdentificators();
    }
}
