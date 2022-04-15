namespace Application.DTO.Request
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces.RequestDtoInterfaces;

    public class OrderLineUpdateRequestDto : IOrderLineWithPizzaVariationAndOrder
    {
        [Required]
        public string Quantity { get; set; }

        [Required]
        public string PizzaVariationId { get; set; }

        [Required]
        public string OrderId { get; set; }
    }
}
