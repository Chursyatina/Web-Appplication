namespace Application.DTO.Request
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces.RequestDtoInterfaces;

    public class OrderLineUpdateRequestDto : IRequestDtoWithPizzaVariation
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        public int? PizzaVariationId { get; set; }
    }
}
