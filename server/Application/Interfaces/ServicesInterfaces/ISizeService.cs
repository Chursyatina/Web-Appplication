namespace Application.Interfaces
{
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.Interfaces.ServicesInterfaces;

    public interface ISizeService : IService<SizeDto, SizeCreateRequestDto, SizeUpdateRequestDto, SizePatchRequestDto>, INamedEntityService<SizeDto>
    {
        public IEnumerable<int> GetIdentificators();
    }
}
