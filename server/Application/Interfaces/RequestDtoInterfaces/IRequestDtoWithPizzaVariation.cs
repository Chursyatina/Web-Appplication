namespace Application.Interfaces.RequestDtoInterfaces
{
    public interface IRequestDtoWithPizzaVariation : IRequestDto
    {
        public string PizzaVariationId { get; set; }
    }
}
