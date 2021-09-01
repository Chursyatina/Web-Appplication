namespace Application.Interfaces
{
    public interface IRequestDtoWithPizza : IRequestDto
    {
        public int? PizzaId { get; set; }
    }
}
