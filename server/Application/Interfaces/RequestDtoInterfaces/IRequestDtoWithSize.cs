namespace Application.Interfaces
{
    public interface IRequestDtoWithSize : IRequestDto
    {
        public int? SizeId { get; set; }
    }
}
