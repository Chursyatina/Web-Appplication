namespace Application.Interfaces
{
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response;

    public interface IPizzaVariationService : IService<PizzaVariationDto, PizzaVariationCreateRequestDto, PizzaVariationUpdateRequestDto, PizzaVariationPatchRequestDto>
    {
        public IEnumerable<int> GetIdentificators();
    }
}
