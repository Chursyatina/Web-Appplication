namespace Application.Interfaces
{
    public interface IRequestDtoWithDough : IRequestDto
    {
        public int? DoughId { get; set; }
    }
}
