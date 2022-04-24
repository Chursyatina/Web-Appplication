namespace Application.Interfaces.RequestDtoInterfaces
{
    using System.Collections.Generic;
    using Application.DTO.Request;

    public interface IRequestDtoWithOrderLines : IRequestDto
    {
        public IEnumerable<OrderLineCreateRequestDto> OrderLines { get; set; }

        public IEnumerable<string> OrderLinesIds { get; set; }
    }
}
