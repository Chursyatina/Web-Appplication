namespace Application.Interfaces
{
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.Interfaces.ServicesInterfaces;

    public interface IIngredientService : IService<IngredientDto, IngredientCreateRequestDto, IngredientUpdateRequestDto, IngredientPatchRequestDto>, INamedEntityService<IngredientDto>
    {
        public IEnumerable<int> GetIdentificators();
    }
}
