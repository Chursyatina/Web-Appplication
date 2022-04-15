namespace Application.Interfaces.RequestDtoInterfaces
{
    using System.Collections.Generic;

    public interface IRequestDtoWithOrderLines : IRequestDto
    {
        public IEnumerable<string> OrderLinesIds { get; set; }
    }
}
