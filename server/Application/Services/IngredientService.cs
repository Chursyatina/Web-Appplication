namespace Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.AutoMapper;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Domain.Repository;

    public class IngredientService : IIngredientService
    {
        private IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public void Delete(string id)
        {
            _ingredientRepository.Delete(id);
        }

        public IngredientDto GetById(string id)
        {
            var existingIngredient = _ingredientRepository.GetById(id);

            if (existingIngredient != null)
            {
                return existingIngredient.ToViewModel();
            }

            return null;
        }

        public IngredientDto GetByName(string name)
        {
            var existingIngredient = _ingredientRepository.GetByName(name);

            if (existingIngredient != null)
            {
                return existingIngredient.ToViewModel();
            }

            return null;
        }

        public IEnumerable<IngredientDto> GetAll()
        {
            return _ingredientRepository.GetAll().Select(x => x.ToViewModel()).ToList();
        }

        public IEnumerable<string> GetIdentificators()
        {
            return _ingredientRepository.GetIdentificators();
        }

        public IngredientDto Insert(IngredientCreateRequestDto ingredient)
        {
            return _ingredientRepository.Insert(ingredient.ToModel()).ToViewModel();
        }

        public IngredientDto Update(string id, IngredientUpdateRequestDto ingredient)
        {
            return _ingredientRepository.Update(id, ingredient.ToModel()).ToViewModel();
        }

        public IngredientDto Patch(string id, IngredientPatchRequestDto ingredient)
        {
            return _ingredientRepository.Patch(id, ingredient.ToModel()).ToViewModel();
        }
    }
}
