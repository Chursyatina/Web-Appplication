namespace Application.DTO.Request
{
    using Application.Interfaces.RequestDtoInterfaces;

    public class OrderLinePatchRequestDto : IRequestDtoWithPizzaVariation
    {
        public int? PizzaVariationId { get; set; }

        public int? Quantity { get; set; }
    }
}
