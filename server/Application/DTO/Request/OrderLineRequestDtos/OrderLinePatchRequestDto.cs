namespace Application.DTO.Request
{
    using Application.Interfaces.RequestDtoInterfaces;

    public class OrderLinePatchRequestDto : IOrderLineWithPizzaVariationAndOrder
    {
        public string PizzaVariationId { get; set; }

        public string Quantity { get; set; }

        public string OrderId { get; set; }
    }
}
