namespace Application.Interfaces.RequestDtoInterfaces
{
    public interface IRequestDtoWithOrderStatus : IRequestDto
    {
        public int? OrderStatusId { get; set; }
    }
}
