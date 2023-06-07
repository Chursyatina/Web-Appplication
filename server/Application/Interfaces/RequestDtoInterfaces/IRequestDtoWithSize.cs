namespace Application.Interfaces
{
    public interface IRequestDtoWithSize : IRequestDto
    {
        public string SizeId { get; set; }
    }
}
