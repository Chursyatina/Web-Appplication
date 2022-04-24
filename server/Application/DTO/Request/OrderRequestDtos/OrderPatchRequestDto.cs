namespace Application.DTO.Request.OrderRequestDtos
{
    using System.Collections.Generic;
    using Application.Interfaces.RequestDtoInterfaces;

    public class OrderPatchRequestDto : IOrderRequestDtoWithOrderLines
    {
        public string OrderStatusId { get; set; }

        public IEnumerable<string> OrderLinesIds { get; set; }

        public IEnumerable<OrderLineCreateRequestDto> OrderLines { get; set; }
    }
}
