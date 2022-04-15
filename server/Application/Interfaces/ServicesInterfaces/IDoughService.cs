namespace Application.Interfaces
{
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.Interfaces.ServicesInterfaces;

    public interface IDoughService : IService<DoughDto, DoughCreateRequestDto, DoughUpdateRequestDto, DoughPatchRequestDto>, INamedEntityService<DoughDto>
    {
        public IEnumerable<string> GetIdentificators();
    }
}