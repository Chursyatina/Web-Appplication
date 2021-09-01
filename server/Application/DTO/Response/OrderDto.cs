namespace Application.DTO.Response
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class OrderDto : IResponseDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public ICollection<OrderLineDto> OrderLines { get; set; }

        [Required]
        public OrderStatusDto OrderStatus { get; set; }
    }
}
