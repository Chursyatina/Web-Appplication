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

        public PizzaVariationDto GetById(string id)
        {
            var existingPizzaVariation = _pizzaVariationRepository.GetById(id);

            if (existingPizzaVariation != null)
            {
                return existingPizzaVariation.ToViewModel();
            }

            return null;
        }

        public PizzaVariationDto Update(string id, PizzaVariationUpdateRequestDto pizzaVariation)
        {
            string pizzaId = pizzaVariation.PizzaId;
            string doughId = pizzaVariation.DoughId;
            string sizeId = pizzaVariation.SizeId;
            IEnumerable<string> ingredientsIds = pizzaVariation.Ingredients;
            IEnumerable<string> additionalIds = pizzaVariation.AdditionalIngredients;

            PizzaVariation pizzaVariationModel = pizzaVariation.ToModel();

            return _pizzaVariationRepository.Update(id, pizzaVariationModel, pizzaId, doughId, sizeId, ingredientsIds.ToList(), additionalIds.ToList()).ToViewModel();
        }

        public PizzaVariationDto Patch(string id, PizzaVariationPatchRequestDto pizzaVariation)
        {
            string pizzaId = pizzaVariation.PizzaId;
            string doughId = pizzaVariation.DoughId;
            string sizeId = pizzaVariation.SizeId;
            IEnumerable<string> ingredientsIds = pizzaVariation.Ingredients;
            IEnumerable<string> additionalIds = pizzaVariation.AdditionalIngredients;

            PizzaVariation pizzaVariationModel = pizzaVariation.ToModel();

            return _pizzaVariationRepository.Patch(id, pizzaVariationModel, pizzaId, doughId, sizeId, ingredientsIds?.ToList(), additionalIds?.ToList()).ToViewModel();
        }

        public PizzaVariationDto Insert(PizzaVariationCreateRequestDto pizzaVariation)
        {
            IEnumerable<string> ingredientsIds = pizzaVariation.Ingredients;
            IEnumerable<string> additionalIds = pizzaVariation.AdditionalIngredients;
            return _pizzaVariationRepository.Insert(pizzaVariation.ToModel(), pizzaVariation.PizzaId, pizzaVariation.SizeId, pizzaVariation.DoughId, ingredientsIds.ToList(), additionalIds.ToList()).ToViewModel();
        }

        public void Delete(string id)
        {
            _pizzaVariationRepository.Delete(id);
        }

        public IEnumerable<string> GetIdentificators()
        {
            return _pizzaVariationRepository.GetIdentificators();
        }

        public PizzaVariationDto FullInsert(PizzaVariationUpdateRequestDto item)
        {
            string pizzaId = item.PizzaId;
            string doughId = item.DoughId;
            string sizeId = item.SizeId;
            IEnumerable<string> ingredientsIds = item.Ingredients;
            IEnumerable<string> additionalIds = item.AdditionalIngredients;

            PizzaVariation pizzaVariationModel = item.ToModel();

            return _pizzaVariationRepository.Insert(pizzaVariationModel, pizzaId, sizeId, doughId, ingredientsIds.ToList(), additionalIds.ToList()).ToViewModel();
        }
    }
}
