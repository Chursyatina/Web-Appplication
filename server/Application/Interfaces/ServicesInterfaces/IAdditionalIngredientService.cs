namespace Application.Interfaces
{
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.Interfaces.ServicesInterfaces;

    public interface IAdditionalIngredientService : IService<AdditionalIngredientDto, AdditionalIngredientCreateRequestDto, AdditionalIngredientUpdateRequestDto, AdditionalIngredientPatchRequestDto>, INamedEntityService<AdditionalIngredientDto>
    {
        public IEnumerable<int> GetIdentificators();
    }
}
