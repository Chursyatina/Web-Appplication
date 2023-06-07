namespace Application.Interfaces.RequestDtoInterfaces
{
    public interface IRequestDtoWithOrder : IRequestDto
    {
        public string OrderId { get; set; }
    }
}
