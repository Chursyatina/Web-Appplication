namespace Application.DTO.Request.OrderRequestDtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces.RequestDtoInterfaces;

    public class OrderUpdateRequestDto : IOrderRequestDtoWithOrderLines
    {
        [Required]
        public string OrderStatusId { get; set; }

        [Required]
        public IEnumerable<string> OrderLinesIds { get; set; }
    }
}
