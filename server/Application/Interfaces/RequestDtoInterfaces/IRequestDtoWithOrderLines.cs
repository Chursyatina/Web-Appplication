namespace Application.Interfaces.RequestDtoInterfaces
{
    using System.Collections.Generic;

    public interface IRequestDtoWithOrderLines : IRequestDto
    {
        public IEnumerable<int> OrderLinesIds { get; set; }
    }
}
