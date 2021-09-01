namespace Application.DTO.Request.OrderRequestDtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces.RequestDtoInterfaces;

    public class OrderCreateRequestDto : IOrderRequestDto
    {
        [Required]
        public int? OrderStatusId { get; set; }

        [Required]
        public IEnumerable<int> OrderLinesIds { get; set; }
    }
}
