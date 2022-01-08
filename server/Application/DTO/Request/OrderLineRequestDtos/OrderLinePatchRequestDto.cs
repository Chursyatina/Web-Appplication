namespace Application.DTO.Request
{
    using Application.Interfaces.RequestDtoInterfaces;

    public class OrderLinePatchRequestDto : IOrderLineWithPizzaVariationAndOrder
    {
        public int? PizzaVariationId { get; set; }

        public int? Quantity { get; set; }

        public int? OrderId { get; set; }
    }
}
