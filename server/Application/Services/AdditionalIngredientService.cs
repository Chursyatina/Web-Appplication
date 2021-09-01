namespace Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.AutoMapper;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Domain.Repository;

    public class AdditionalIngredientService : IAdditionalIngredientService
    {
        private IAdditionalIngredientRepository _additionalIngredientRepository;

        public AdditionalIngredientService(IAdditionalIngredientRepository additionalIngredientRepository)
        {
            _additionalIngredientRepository = additionalIngredientRepository;
        }

        public void Delete(int id)
        {
            _additionalIngredientRepository.Delete(id);
        }

        public IEnumerable<AdditionalIngredientDto> GetAll()
        {
            return _additionalIngredientRepository.GetAll().Select(x => x.ToViewModel()).ToList();
        }

        public IEnumerable<int> GetIdentificators()
        {
            return _additionalIngredientRepository.GetIdentificators();
        }

        public AdditionalIngredientDto GetById(int id)
        {
            var existingAdditionalIngredient = _additionalIngredientRepository.GetById(id);

            if (existingAdditionalIngredient != null)
            {
                return existingAdditionalIngredient.ToViewModel();
            }

            return null;
        }

        public AdditionalIngredientDto GetByName(string name)
        {
            var existingAdditionalIngredient = _additionalIngredientRepository.GetByName(name);

            if (existingAdditionalIngredient != null)
            {
                return existingAdditionalIngredient.ToViewModel();
            }

            return null;
        }

        public AdditionalIngredientDto Insert(AdditionalIngredientCreateRequestDto item)
        {
            return _additionalIngredientRepository.Insert(item.ToModel()).ToViewModel();
        }

        public AdditionalIngredientDto Patch(int id, AdditionalIngredientPatchRequestDto additionalIngredient)
        {
            return _additionalIngredientRepository.Patch(id, additionalIngredient.ToModel()).ToViewModel();
        }

        public AdditionalIngredientDto Update(int id, AdditionalIngredientUpdateRequestDto item)
        {
            return _additionalIngredientRepository.Update(id, item.ToModel()).ToViewModel();
        }
    }
}
