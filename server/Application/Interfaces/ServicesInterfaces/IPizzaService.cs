namespace Application.Interfaces
{
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.Interfaces.ServicesInterfaces;

    public interface IPizzaService : IService<PizzaDto, PizzaCreateRequestDto, PizzaUpdateRequestDto, PizzaPatchRequestDto>, INamedEntityService<PizzaDto>
    {
        public IEnumerable<int> GetIdentificators();
    }
}
