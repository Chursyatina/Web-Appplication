namespace Application.Interfaces.RequestDtoInterfaces
{
    public interface IRequestDtoWithOrderStatus : IRequestDto
    {
        public string OrderStatusId { get; set; }
    }
}
