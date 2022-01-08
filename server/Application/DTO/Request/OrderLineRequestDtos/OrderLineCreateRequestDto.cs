namespace Application.DTO.Request
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces.RequestDtoInterfaces;

    public class OrderLineCreateRequestDto : IOrderLineWithPizzaVariationAndOrder
    {
        [Required]
        public int? PizzaVariationId { get; set; }

        [Required]
        public int? OrderId { get; set; }
    }
}
