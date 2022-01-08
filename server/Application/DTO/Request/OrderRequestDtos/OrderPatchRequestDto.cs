namespace Application.DTO.Request.OrderRequestDtos
{
    using System.Collections.Generic;
    using Application.Interfaces.RequestDtoInterfaces;

    public class OrderPatchRequestDto : IOrderRequestDtoWithOrderLines
    {
        public int? OrderStatusId { get; set; }

        public IEnumerable<int> OrderLinesIds { get; set; }
    }
}
