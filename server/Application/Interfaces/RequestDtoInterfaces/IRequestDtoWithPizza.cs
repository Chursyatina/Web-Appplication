namespace Application.Interfaces
{
    public interface IRequestDtoWithPizza : IRequestDto
    {
        public string PizzaId { get; set; }
    }
}
