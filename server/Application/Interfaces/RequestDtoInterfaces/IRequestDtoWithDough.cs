namespace Application.Interfaces
{
    public interface IRequestDtoWithDough : IRequestDto
    {
        public string DoughId { get; set; }
    }
}
