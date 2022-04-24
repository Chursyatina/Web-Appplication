namespace Application.DTO.Request
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces.RequestDtoInterfaces;

    public class OrderLineCreateRequestDto : IOrderLineWithPizzaVariationAndOrder
    {
        [Required]
        public string PizzaVariationId { get; set; }

        public string OrderId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
