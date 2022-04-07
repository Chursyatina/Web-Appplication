namespace Application.Services
{
    using System.Collections.Generic;
    using Application.DTO.Request.BasketRequestDtos;
    using Application.DTO.Response;
    using Application.Interfaces.ServicesInterfaces;
    using Domain.Models;
    using Domain.Repository;

    public class BasketService : IBasketService
    {
        private IBasketRepository _basketRepository;

        public void Delete(int id)
        {
            _basketRepository.Delete(id);
        }

        public IEnumerable<BasketDto> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public BasketDto GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Basket> GetModels()
        {
            return _basketRepository.GetAll();
        }

        public BasketDto Insert(BasketCreateRequestDto item)
        {
            throw new System.NotImplementedException();
        }

        public BasketDto Patch(int id, BasketPatchRequestDto item)
        {
            throw new System.NotImplementedException();
        }

        public BasketDto Update(int id, BasketUpdateRequestDto item)
        {
            throw new System.NotImplementedException();
        }
    }
}
