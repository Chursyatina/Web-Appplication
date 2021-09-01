namespace Application.DTO.Request
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces.RequestDtoInterfaces;

    public class OrderLineCreateRequestDto : IRequestDtoWithPizzaVariation
    {
        [Required]
        public int? PizzaVariationId { get; set; }
    }
}
