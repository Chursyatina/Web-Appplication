﻿namespace Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.AutoMapper;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Domain.Models;
    using Domain.Repository;

    public class PizzaService : IPizzaService
    {
        private IPizzaRepository _pizzaRepository;

        public PizzaService(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        public IEnumerable<PizzaDto> GetAll()
        {
            return _pizzaRepository.GetAll().Select(x => x.ToViewModel());
        }

        public PizzaDto GetById(int id)
        {
            var existingPizza = _pizzaRepository.GetById(id);

            if (existingPizza != null)
            {
                return existingPizza.ToViewModel();
            }

            return null;
        }

        public PizzaDto GetByName(string name)
        {
            var existingPizza = _pizzaRepository.GetByName(name);

            if (existingPizza != null)
            {
                return existingPizza.ToViewModel();
            }

            return null;
        }

        public PizzaDto Update(int id, PizzaUpdateRequestDto pizza)
        {
            IEnumerable<int> ingredientsIds = pizza.Ingredients;
            Pizza pizzaModel = pizza.ToModel();

            return _pizzaRepository.Update(id, pizzaModel, ingredientsIds.ToList()).ToViewModel();
        }

        public PizzaDto Patch(int id, PizzaPatchRequestDto pizza)
        {
            IEnumerable<int> ingredientsIds = pizza.Ingredients;
            Pizza pizzaModel = pizza.ToModel();

            if (pizza.Ingredients == null)
            {
                pizzaModel.Ingredients = null;
            }

            if (ingredientsIds == null)
            {
                return _pizzaRepository.Patch(id, pizzaModel, null).ToViewModel();
            }

            return _pizzaRepository.Patch(id, pizzaModel, ingredientsIds.ToList()).ToViewModel();
        }

        public PizzaDto Insert(PizzaCreateRequestDto pizza)
        {
            return _pizzaRepository.Insert(pizza.ToModel()).ToViewModel();
        }

        public void Delete(int id)
        {
            _pizzaRepository.Delete(id);
        }

        public IEnumerable<int> GetIdentificators()
        {
            return _pizzaRepository.GetIdentificators();
        }
    }
}