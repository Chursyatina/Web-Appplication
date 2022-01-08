namespace Application.DTO.Request
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces.RequestDtoInterfaces;

    public class OrderLineUpdateRequestDto : IOrderLineWithPizzaVariationAndOrder
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        public int? PizzaVariationId { get; set; }

        [Required]
        public int? OrderId { get; set; }
    }
}
