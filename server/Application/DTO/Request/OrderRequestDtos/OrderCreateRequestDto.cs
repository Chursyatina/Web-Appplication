namespace Application.DTO.Request.OrderRequestDtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces.RequestDtoInterfaces;

    public class OrderCreateRequestDto : IRequestDtoWithOrderLines
    {
        [Required]
        public IEnumerable<OrderLineCreateRequestDto> OrderLines { get; set; }

        public IEnumerable<string> OrderLinesIds { get; set; }
    }
}
