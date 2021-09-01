namespace Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.AutoMapper;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Domain.Models;
    using Domain.Repository;

    public class PizzaVariationService : IPizzaVariationService
    {
        private IPizzaVariationRepository _pizzaVariationRepository;

        public PizzaVariationService(IPizzaVariationRepository pizzaVariationRepository)
        {
            _pizzaVariationRepository = pizzaVariationRepository;
        }

        public IEnumerable<PizzaVariationDto> GetAll()
        {
            return _pizzaVariationRepository.GetAll().Select(x => x.ToViewModel());
        }

        public PizzaVariationDto GetById(int id)
        {
            var existingPizzaVariation = _pizzaVariationRepository.GetById(id);

            if (existingPizzaVariation != null)
            {
                return existingPizzaVariation.ToViewModel();
            }

            return null;
        }

        public PizzaVariationDto Update(int id, PizzaVariationUpdateRequestDto pizzaVariation)
        {
            int pizzaId = (int)pizzaVariation.PizzaId;
            int doughId = (int)pizzaVariation.DoughId;
            int sizeId = (int)pizzaVariation.SizeId;
            IEnumerable<int> ingredientsIds = pizzaVariation.Ingredients;
            IEnumerable<int> additionalIds = pizzaVariation.AdditionalIngredients;

            PizzaVariation pizzaVariationModel = pizzaVariation.ToModel();

            return _pizzaVariationRepository.Update(id, pizzaVariationModel, pizzaId, doughId, sizeId, ingredientsIds.ToList(), additionalIds.ToList()).ToViewModel();
        }

        public PizzaVariationDto Patch(int id, PizzaVariationPatchRequestDto pizzaVariation)
        {
            int? pizzaId = pizzaVariation.PizzaId;
            int? doughId = pizzaVariation.DoughId;
            int? sizeId = pizzaVariation.SizeId;
            IEnumerable<int> ingredientsIds = pizzaVariation.Ingredients;
            IEnumerable<int> additionalIds = pizzaVariation.AdditionalIngredients;

            PizzaVariation pizzaVariationModel = pizzaVariation.ToModel();

            return _pizzaVariationRepository.Patch(id, pizzaVariationModel, pizzaId, doughId, sizeId, ingredientsIds?.ToList(), additionalIds?.ToList()).ToViewModel();
        }

        public PizzaVariationDto Insert(PizzaVariationCreateRequestDto pizzaVariation)
        {
            return _pizzaVariationRepository.Insert(pizzaVariation.ToModel(), (int)pizzaVariation.PizzaId, (int)pizzaVariation.SizeId, (int)pizzaVariation.DoughId).ToViewModel();
        }

        public void Delete(int id)
        {
            _pizzaVariationRepository.Delete(id);
        }

        public IEnumerable<int> GetIdentificators()
        {
            return _pizzaVariationRepository.GetIdentificators();
        }
    }
}
