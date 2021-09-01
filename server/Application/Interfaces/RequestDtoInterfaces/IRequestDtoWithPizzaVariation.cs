namespace Application.Interfaces.RequestDtoInterfaces
{
    public interface IRequestDtoWithPizzaVariation : IRequestDto
    {
        public int? PizzaVariationId { get; set; }
    }
}
