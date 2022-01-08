namespace Application.Interfaces.RequestDtoInterfaces
{
    public interface IRequestDtoWithOrder : IRequestDto
    {
        public int? OrderId { get; set; }
    }
}
